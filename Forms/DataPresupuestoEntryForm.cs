using Pinturería.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pinturería.Forms
{
    public partial class DataPresupuestoEntryForm : Form
    {
        public string SelectedCategoria { get; private set; }
        public string SelectedProducto { get; private set; }
        public int SelectedCantidad { get; private set; }
        public decimal SelectedPrecio { get; private set; } // precio unitario
        public List<string> ProductosSeleccionados { get; set; } = new List<string>();

        public DataPresupuestoEntryForm(List<string> _ProductosSeleccionados)
        {
            InitializeComponent();

            //por cada instancia de este "DataPresupuestoEntryForm" que se habre desde "PresupuestoForm" le pasamos los productos que ya se registraron en el presupuesto para que no se vuelvan a mostrar para elegir en el dropdowon de Productos
            this.ProductosSeleccionados = _ProductosSeleccionados;

            btnAceptar.Click += BtnAceptar_Click;
            btnCancelar.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            cmbCategoria.SelectedIndexChanged += CmbCategoria_SelectedIndexChanged;
            cmbProducto.SelectedIndexChanged += CmbProducto_SelectedIndexChanged;
            cmbCantidad.SelectedIndexChanged += CmbCantidad_SelectedIndexChanged;

            LoadCombos();
        }

        private void LoadCombos()
        {
            using (var db = new AppDbContext())
            {
                // Cargar categorías
                var categorias = db.Categorias.OrderBy(c => c.Nombre).ToList();
                cmbCategoria.Items.Clear();
                cmbCategoria.Items.Add("Seleccionar opción");
                foreach (var c in categorias) cmbCategoria.Items.Add(c.Nombre);
                cmbCategoria.SelectedIndex = 0;

                // Producto y cantidad empiezan vacíos y deshabilitados
                cmbProducto.Items.Clear();
                cmbProducto.Items.Add("Seleccionar opción");
                cmbProducto.SelectedIndex = 0;
                cmbProducto.Enabled = false;

                cmbCantidad.Items.Clear();
                cmbCantidad.Items.Add("Seleccionar opción");
                cmbCantidad.SelectedIndex = 0;
                cmbCantidad.Enabled = false;
            }
        }

        private void CmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategoria.SelectedIndex <= 0)
            {
                cmbProducto.Enabled = false;
                cmbProducto.Items.Clear();
                cmbProducto.Items.Add("Seleccionar opción");
                cmbProducto.SelectedIndex = 0;

                cmbCantidad.Enabled = false;
                cmbCantidad.Items.Clear();
                cmbCantidad.Items.Add("Seleccionar opción");
                cmbCantidad.SelectedIndex = 0;

                return;
            }

            string categoriaNombre = cmbCategoria.SelectedItem.ToString();
            using (var db = new AppDbContext())
            {
                var productos = db.Productos
                                .Where(p => p.Category_ID == db.Categorias.First(c => c.Nombre == categoriaNombre).Id)
                                .OrderBy(p => p.Nombre)
                                .ToList();

                // Filtrar productos que ya se registraron en el presupuesto
                productos = productos
                            .Where(p => !ProductosSeleccionados.Contains(p.Nombre))
                            .ToList();

                cmbProducto.Enabled = true;
                cmbProducto.Items.Clear();
                cmbProducto.Items.Add("Seleccionar opción");
                foreach (var p in productos) cmbProducto.Items.Add(p.Nombre);
                cmbProducto.SelectedIndex = 0;

                cmbCantidad.Enabled = false;
                cmbCantidad.Items.Clear();
                cmbCantidad.Items.Add("Seleccionar opción");
                cmbCantidad.SelectedIndex = 0;

            }
        }

        private void CmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedIndex <= 0)
            {
                cmbCantidad.Enabled = false;
                cmbCantidad.Items.Clear();
                cmbCantidad.Items.Add("Seleccionar opción");
                cmbCantidad.SelectedIndex = 0;
                return;
            }

            string productoNombre = cmbProducto.SelectedItem.ToString();

            using (var db = new AppDbContext())
            {
                var producto = db.Productos.FirstOrDefault(p => p.Nombre == productoNombre);
                if (producto == null)
                {
                    MessageBox.Show("Producto no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbProducto.SelectedIndex = 0;
                    return;
                }

                // Habilitar combo
                cmbCantidad.Enabled = true;
                cmbCantidad.Items.Clear();
                cmbCantidad.Items.Add("Seleccionar opción");
                for (int i = 0; i <= 1000; i++)
                    cmbCantidad.Items.Add(i.ToString());
                cmbCantidad.SelectedIndex = 0;
            }
        }

        private void CmbCantidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCantidad.SelectedIndex <= 0 || cmbCantidad.SelectedItem.ToString() == "0")
            {
                btnAceptar.Enabled = false;
                return;
            }
            btnAceptar.Enabled = true;
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            if (cmbCategoria.SelectedIndex <= 0)
            {
                MessageBox.Show("Seleccione una categoría.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbProducto.SelectedIndex <= 0)
            {
                MessageBox.Show("Seleccione un producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbCantidad.SelectedIndex <= 0 || cmbCantidad.SelectedItem.ToString() == "0")
            {
                MessageBox.Show("Seleccione una cantidad mayor que cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SelectedCategoria = cmbCategoria.SelectedItem.ToString();
            SelectedProducto = cmbProducto.SelectedItem.ToString();
            ProductosSeleccionados.Add(SelectedProducto);
            SelectedCantidad = int.Parse(cmbCantidad.SelectedItem.ToString());

            using (var db = new AppDbContext())
            {
                var producto = db.Productos.FirstOrDefault(p => p.Nombre == SelectedProducto);
                SelectedPrecio = producto?.Precio ?? 0m;
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}
