using Pinturería.Data;
using Pinturería.Models;
using System.Text.RegularExpressions;
using System.Globalization;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;

namespace Pinturería.Forms
{
    public partial class ProductosForm : Form
    {
        public ProductosForm()
        {
            InitializeComponent();

            btnAgregarCategoria.Click += btnAgregarCategoria_Click;
            dgvCategorias.CellContentClick += dgvCategorias_CellContentClick;
            btnAgregarProducto.Click += btnAgregarProducto_Click;
            dgvProductos.CellContentClick += dgvProductos_CellContentClick;
            
            cmbFiltroCategoria.SelectedIndexChanged += cmbFiltroCategoria_SelectedIndexChanged;

            btnExportarCategorías.Click += btnExportarCategorías_Click;
            btnExportarProductos.Click += btnExportarProductos_Click;

            // Cargar combos y grillas
            CargarCategoriasCombo();
            CargarCategorias();
            CargarProductos();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            lblTitulo.Location = new Point(30, 20);
            txtNombreCategoria.Location = new Point(30, lblTitulo.Bottom + 20);
            btnAgregarCategoria.Location = new Point(30, txtNombreCategoria.Bottom + 10);
            lblCategorias.Location = new Point(30, btnAgregarCategoria.Bottom + 20);
            btnExportarCategorías.Location = new Point(30, lblCategorias.Bottom + 20);
            dgvCategorias.Location = new Point(30, btnExportarCategorías.Bottom + 10);
            //Altura y anchura del dgvCategorias
            dgvCategorias.Width = (int)(this.ClientSize.Width * 0.80);
            dgvCategorias.Height =  (int)(this.ClientSize.Height * 0.90);

            var dgvCategoriasLocationXMoreWidth = dgvCategorias.Width + dgvCategorias.Location.X + 20;
            var lblCategoriasLocationY = lblCategorias.Location.Y;

            lblTituloProductos.Location = new Point(dgvCategoriasLocationXMoreWidth, lblCategoriasLocationY);
            lblNombreProducto.Location = new Point(dgvCategoriasLocationXMoreWidth, lblTituloProductos.Bottom + 20);
            txtNombreProducto.Location = new Point(lblNombreProducto.Right + 25, lblNombreProducto.Location.Y);
            lblCategoriaProducto.Location = new Point(dgvCategoriasLocationXMoreWidth, txtNombreProducto.Bottom + 10);
            cmbCategoria.Location = new Point(lblNombreProducto.Right + 25, lblCategoriaProducto.Location.Y);
            lblPrecio.Location = new Point(dgvCategoriasLocationXMoreWidth, cmbCategoria.Bottom + 10);
            txtPrecio.Location = new Point(lblNombreProducto.Right + 25, lblPrecio.Location.Y);
            lblStock.Location = new Point(dgvCategoriasLocationXMoreWidth, txtPrecio.Bottom + 10);
            txtStock.Location = new Point(lblNombreProducto.Right + 25, lblStock.Location.Y);
            btnAgregarProducto.Location = new Point(dgvCategoriasLocationXMoreWidth, txtStock.Bottom + 10);
            lblProductos.Location = new Point(dgvCategoriasLocationXMoreWidth, btnAgregarProducto.Bottom + 20);
            lblFiltrar.Location = new Point(dgvCategoriasLocationXMoreWidth, lblProductos.Bottom + 10);
            cmbFiltroCategoria.Location = new Point(dgvCategoriasLocationXMoreWidth, lblFiltrar.Bottom + 5);
            cmbFiltroCategoria.Width = (int)(this.ClientSize.Width * 0.30);

            lblSeparador.Location = new Point(30, dgvCategorias.Bottom + 20);
            lblSeparador.Width = ((int)(this.ClientSize.Width * 0.50) * 2) - 30;

            btnExportarProductos.Location = new Point(30, lblSeparador.Bottom + 20);
            dgvProductos.Location = new Point(30, btnExportarProductos.Bottom + 10);
            //Altura y anchura del dgvProductos
            dgvProductos.Width = lblSeparador.Width;
            dgvProductos.Height =  (int)(dgvCategorias.Height * 1.5);

            lblSeparadorDos.Location = new Point(30, dgvProductos.Bottom + 30);
        }


        // ----------------------
        // Categorías
        // ----------------------
        private async void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreCategoria.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Ingrese un nombre de categoría", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnAgregarCategoria.Enabled = false;
            btnAgregarCategoria.Text = "⏳ Guardando...";

            try
            {
                using (var db = new AppDbContext())
                {
                    bool existe = await Task.Run(() => db.Categorias.Any(c => c.Nombre == nombre));
                    if (existe)
                        MessageBox.Show("Ya existe dicha categoría", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        db.Categorias.Add(new Categoria { Nombre = nombre });
                        await db.SaveChangesAsync();
                        MessageBox.Show("Categoría agregada correctamente",
                                           "Mensaje",
                                           MessageBoxButtons.OK,
                                           MessageBoxIcon.Information);
                        txtNombreCategoria.Clear();
                        CargarCategorias();
                        CargarCategoriasCombo();
                        CargarProductos();
                    }
                }
            }
            finally
            {
                btnAgregarCategoria.Enabled = true;
                btnAgregarCategoria.Text = "Agregar categoría";
            }
        }

        private void CargarCategoriasCombo()
        {
            using (var db = new AppDbContext())
            {
                // Lista separada para registrar productos (cmbCategoria)
                var categoriasRegistro = db.Categorias
                                        .OrderBy(c => c.Nombre)
                                        .ToList();
                // Insertamos "Seleccione una categoría" al inicio
                categoriasRegistro.Insert(0, new Categoria { Id = 0, Nombre = "Seleccionar" });
                cmbCategoria.DataSource = categoriasRegistro;
                cmbCategoria.DisplayMember = "Nombre";
                cmbCategoria.ValueMember = "Id";

                // Lista separada para filtrar productos (cmbFiltroCategoria)
                var categoriasFiltro = db.Categorias
                                        .OrderBy(c => c.Nombre)
                                        .ToList();
                // Insertamos "Todas" al inicio
                categoriasFiltro.Insert(0, new Categoria { Id = 0, Nombre = "Todas" });

                // Desconectamos el evento mientras asignamos el DataSource
                cmbFiltroCategoria.SelectedIndexChanged -= cmbFiltroCategoria_SelectedIndexChanged;
                cmbFiltroCategoria.DataSource = categoriasFiltro;
                cmbFiltroCategoria.DisplayMember = "Nombre";
                cmbFiltroCategoria.ValueMember = "Id";
                cmbFiltroCategoria.SelectedValue = 0;
                // Volvemos a conectar el evento
                cmbFiltroCategoria.SelectedIndexChanged += cmbFiltroCategoria_SelectedIndexChanged;
            }
        }

        private void CargarCategorias()
        {
            using (var db = new AppDbContext())
            {
                var categorias = db.Categorias
                    .OrderBy(c => c.Nombre)
                    .Select(c => new { c.Id, c.Nombre })
                    .ToList();

                dgvCategorias.Columns.Clear();
                dgvCategorias.DataSource = categorias;

                // Iconos editar/eliminar
                DataGridViewImageColumn editarCol = new DataGridViewImageColumn
                {
                    Name = "Editar",
                    HeaderText = "",
                    Image = Properties.Resources.iconEditar,
                    Width = 30,
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };
                dgvCategorias.Columns.Add(editarCol);

                DataGridViewImageColumn eliminarCol = new DataGridViewImageColumn
                {
                    Name = "Eliminar",
                    HeaderText = "",
                    Image = Properties.Resources.iconEliminar,
                    Width = 30,
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };
                dgvCategorias.Columns.Add(eliminarCol);
            }
        }

        private void dgvCategorias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int categoriaId = Convert.ToInt32(dgvCategorias.Rows[e.RowIndex].Cells["Id"].Value);
            string colName = dgvCategorias.Columns[e.ColumnIndex].Name;

            if (colName == "Eliminar")
            {
                if (MessageBox.Show("Se eliminará la categoría y los productos asociados a ella. ¿Desea continuar?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    using (var db = new AppDbContext())
                    {
                        var cat = db.Categorias.FirstOrDefault(c => c.Id == categoriaId);
                        if (cat != null)
                        {
                            var productos = db.Productos.Where(p => p.Category_ID == categoriaId).ToList();
                            db.Productos.RemoveRange(productos);
                            db.Categorias.Remove(cat);
                            db.SaveChanges();
                            CargarCategorias();
                            CargarCategoriasCombo();
                            CargarProductos();
                        }
                    }
                }
            }
            else if (colName == "Editar")
            {
                string nombreActual = dgvCategorias.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                string nuevoNombre = nombreActual;

                while (true)
                {
                    nuevoNombre = Microsoft.VisualBasic.Interaction.InputBox(
                        "Ingrese nuevo nombre de categoría:",
                        "Editar categoría",
                        nuevoNombre // deja precargado lo último escrito
                    );

                    // Usuario canceló o no ingresó nada
                    if (string.IsNullOrWhiteSpace(nuevoNombre) || nuevoNombre == nombreActual)
                        break;

                    using (var db = new AppDbContext())
                    {
                        bool existe = db.Categorias.Any(c => c.Nombre == nuevoNombre);
                        if (existe)
                        {
                            MessageBox.Show("Ya existe dicha categoría",
                                            "Error",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                            continue; // vuelve a mostrar el InputBox
                        }
                        else
                        {
                            var cat = db.Categorias.FirstOrDefault(c => c.Id == categoriaId);
                            cat.Nombre = nuevoNombre;
                            db.SaveChanges();
                            CargarCategorias();
                            CargarCategoriasCombo();
                            CargarProductos();
                            MessageBox.Show("Se cambió el nombre de la categoría correctamente",
                                            "Mensaje",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                            break; // termina el bucle al guardar con éxito
                        }
                    }
                }
            }

        }

        // ----------------------
        // Productos
        // ----------------------
        private async void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreProducto.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Ingrese un nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int categoriaId = cmbCategoria.SelectedValue != null ? (int)cmbCategoria.SelectedValue : 0;
            if (categoriaId == 0)
            {
                MessageBox.Show("Eliga una categoría", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string input = txtPrecio.Text.Trim();
            if (!Regex.IsMatch(input, @"^\d+([.,]\d{1,2})?$")) { MessageBox.Show("Precio inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            decimal precio = decimal.Parse(input.Replace(',', '.'), CultureInfo.InvariantCulture);
            
            if (!int.TryParse(txtStock.Text.Trim(), out int stock)) { MessageBox.Show("Stock inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (stock < 0){MessageBox.Show("El stock no puede ser negativo.","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);return;}

            btnAgregarProducto.Enabled = false;
            btnAgregarProducto.Text = "⏳ Guardando...";

            try
            {
                using (var db = new AppDbContext())
                {
                    bool existe = await Task.Run(() => db.Productos.Any(p => p.Nombre == nombre));
                    if (existe)
                        MessageBox.Show("Ya existe dicho producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        db.Productos.Add(new Producto
                        {
                            Nombre = nombre,
                            Category_ID = categoriaId,
                            Precio = precio,
                            Stock = stock
                        });
                        await db.SaveChangesAsync();
                        MessageBox.Show("Producto agregado correctamente",
                                            "Mensaje",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                        txtNombreProducto.Clear();
                        cmbCategoria.SelectedIndex = 0;
                        txtPrecio.Clear();
                        txtStock.Clear();
                        CargarProductos();
                    }
                }
            }
            finally
            {
                btnAgregarProducto.Enabled = true;
                btnAgregarProducto.Text = "Agregar producto";
            }
        }

        private void CargarProductos()
        {
            using (var db = new AppDbContext())
            {
                var productos = db.Productos.AsQueryable();

                if (cmbFiltroCategoria.SelectedValue is int catId && catId > 0)
                    productos = productos.Where(p => p.Category_ID == catId);

                dgvProductos.Columns.Clear();
                dgvProductos.DataSource = productos
                    .OrderBy(p => p.Nombre)
                    .Select(p => new { p.Id, p.Nombre, Categoria = p.Categoria.Nombre, p.Precio, p.Stock })
                    .ToList();

                // Iconos
                DataGridViewImageColumn editarCol = new DataGridViewImageColumn
                {
                    Name = "Editar",
                    HeaderText = "",
                    Image = Properties.Resources.iconEditar,
                    Width = 30,
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };
                dgvProductos.Columns.Add(editarCol);

                DataGridViewImageColumn eliminarCol = new DataGridViewImageColumn
                {
                    Name = "Eliminar",
                    HeaderText = "",
                    Image = Properties.Resources.iconEliminar,
                    Width = 30,
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };
                dgvProductos.Columns.Add(eliminarCol);
            }
        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int productoId = Convert.ToInt32(dgvProductos.Rows[e.RowIndex].Cells["Id"].Value);
            string colName = dgvProductos.Columns[e.ColumnIndex].Name;

            if (colName == "Eliminar")
            {
                if (MessageBox.Show("Se eliminará el producto. ¿Desea continuar?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    using (var db = new AppDbContext())
                    {
                        var prod = db.Productos.FirstOrDefault(p => p.Id == productoId);
                        if (prod != null)
                        {
                            db.Productos.Remove(prod);
                            db.SaveChanges();
                            CargarProductos();
                        }
                    }
                }
            }
            else if (colName == "Editar")
            {
                using (var db = new AppDbContext())
                {
                    var prod = db.Productos.FirstOrDefault(p => p.Id == productoId);
                    if (prod == null) return;

                    // -------------------
                    // Nombre
                    // -------------------
                    string nuevoNombre = Microsoft.VisualBasic.Interaction.InputBox("Nombre:", "Editar producto", prod.Nombre);

                    if (string.IsNullOrEmpty(nuevoNombre)) return;

                    if (nuevoNombre != prod.Nombre)
                    {
                        // Validar duplicados
                        if (db.Productos.Any(p => p.Nombre == nuevoNombre))
                        {
                            MessageBox.Show("Ya existe dicho producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; // abortar edición completa
                        }
                        prod.Nombre = nuevoNombre;
                        // -------------------
                        // Guardar cambios
                        // -------------------
                        db.SaveChanges();
                        CargarProductos();
                        CargarCategorias();
                        MessageBox.Show("Nombre actualizado correctamente.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // -------------------
                    // Precio
                    // -------------------
                    string nuevoPrecioStr = Microsoft.VisualBasic.Interaction.InputBox("Precio:", "Editar producto", prod.Precio.ToString());

                    if (string.IsNullOrEmpty(nuevoPrecioStr)) return; // CANCELAR → cortar todo

                    // Validación regex (máximo 2 decimales, coma o punto)
                    if (!Regex.IsMatch(nuevoPrecioStr, @"^\d+([.,]\d{1,2})?$"))
                    {
                        MessageBox.Show("Precio inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Normalizar coma a punto
                    nuevoPrecioStr = nuevoPrecioStr.Replace(',', '.');

                    if (decimal.TryParse(nuevoPrecioStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal nuevoPrecio))
                    {
                        if (nuevoPrecio != prod.Precio)
                        {
                            prod.Precio = nuevoPrecio;
                            // -------------------
                            // Guardar cambios
                            // -------------------
                            db.SaveChanges();
                            CargarProductos();
                            CargarCategorias();
                            MessageBox.Show("Precio actualizado correctamente.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Precio inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // -------------------
                    // Stock
                    // -------------------
                    string nuevoStockStr = Microsoft.VisualBasic.Interaction.InputBox("Stock:", "Editar producto", prod.Stock.ToString());

                    if (string.IsNullOrEmpty(nuevoStockStr)) return; // CANCELAR → cortar todo

                    if (!int.TryParse(nuevoStockStr, out int nuevoStock))
                    {
                        MessageBox.Show("Stock inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (nuevoStock < 0)
                    {
                        MessageBox.Show("El stock no puede ser negativo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (nuevoStock != prod.Stock)
                    {
                        prod.Stock = nuevoStock;
                        // -------------------
                        // Guardar cambios
                        // -------------------
                        db.SaveChanges();
                        CargarProductos();
                        CargarCategorias();
                        MessageBox.Show("Stock actualizado correctamente.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }


            }
        }

        // ----------------------
        // Filtros
        // ----------------------
        private void cmbFiltroCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFiltroCategoria.SelectedValue != null)
            {
                if (cmbFiltroCategoria.SelectedItem is Categoria categoriaSeleccionada)
                {
                    int catId = categoriaSeleccionada.Id;
                    using (var db = new AppDbContext())
                    {
                        var productos = db.Productos.Where(p => p.Category_ID == catId).OrderBy(p => p.Nombre).ToList();
                    }
                    CargarProductos();
                }
            }
        }

        private void btnExportarCategorías_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Archivo PDF (*.pdf)|*.pdf";
                sfd.FileName = $"Categorias_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    GenerarPDF_MigraDoc(sfd.FileName, "Categorías", dgvCategorias, "Categorías");
                    MessageBox.Show("PDF de categorías generado correctamente.",
                                    "Exportación completada",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
        }

        private void btnExportarProductos_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Archivo PDF (*.pdf)|*.pdf";
                sfd.FileName = $"Productos_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    GenerarPDF_MigraDoc(sfd.FileName, "Productos", dgvProductos, "Productos");
                    MessageBox.Show("PDF de productos generado correctamente.",
                                    "Exportación completada",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
        }

        private void GenerarPDF_MigraDoc(string rutaArchivo, string titulo, DataGridView dgv, string tablaPara)
        {
            // Crear documento
            var doc = new Document();
            var seccion = doc.AddSection();

            seccion.PageSetup.PageFormat = PageFormat.A4;
            seccion.PageSetup.Orientation = MigraDoc.DocumentObjectModel.Orientation.Landscape;

            // Márgenes de 0.7 cm en todos los lados
            seccion.PageSetup.LeftMargin = Unit.FromCentimeter(0.7);
            seccion.PageSetup.RightMargin = Unit.FromCentimeter(0.7);
            seccion.PageSetup.TopMargin = Unit.FromCentimeter(0.7);
            seccion.PageSetup.BottomMargin = Unit.FromCentimeter(0.7);

            // Título
            var pTitulo = seccion.AddParagraph(titulo);
            pTitulo.Format.Font.Size = 18;
            pTitulo.Format.Font.Bold = true;
            pTitulo.Format.SpaceAfter = "0.3cm";
            pTitulo.Format.Alignment = ParagraphAlignment.Center;

            // 📅 Fecha de generación
            var fecha = seccion.AddParagraph($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}");
            fecha.Format.Font.Size = 9;
            fecha.Format.Font.Italic = true;
            fecha.Format.SpaceAfter = "0.5cm";
            fecha.Format.Alignment = ParagraphAlignment.Right;

            //
            //TABLA
            //

            // Crear tabla
            var tabla = seccion.AddTable();
            tabla.Borders.Width = 0.75;
            tabla.Format.Alignment = ParagraphAlignment.Center;

            //
            //COLUMNAS
            //

            // Crear columnas según las visibles del DataGridView (excepto imágenes)
            var columnas = dgv.Columns
                            .Cast<DataGridViewColumn>()
                            .Where(c => c.Visible && c.GetType() != typeof(DataGridViewImageColumn))
                            .ToList();

            // Crear columnas 

            double PorcentajeDe28_5(double porcentaje)
            {
                return (28.5 * porcentaje) / 100.0;
            }

            double[] anchos = [];
            int index = 0;

            if (tablaPara == "Categorías")
            {
                anchos = [PorcentajeDe28_5(4), PorcentajeDe28_5(96)];
            }
            else if (tablaPara == "Productos")
            {
                anchos = [PorcentajeDe28_5(4), PorcentajeDe28_5(53), PorcentajeDe28_5(27), PorcentajeDe28_5(9), PorcentajeDe28_5(4)];
            }

            foreach (var col in columnas)
            {
                var column = tabla.AddColumn(Unit.FromCentimeter(anchos[index++]));
                column.Format.Alignment = ParagraphAlignment.Left;
            }

            // Encabezado
            var filaHeader = tabla.AddRow();
            filaHeader.Shading.Color = Colors.LightGray;
            filaHeader.Format.Font.Bold = true;
            filaHeader.Format.Alignment = ParagraphAlignment.Left;

            for (int i = 0; i < columnas.Count; i++)
                filaHeader.Cells[i].AddParagraph(columnas[i].HeaderText);

            //
            //FILAS
            //

            // Filas
            foreach (DataGridViewRow fila in dgv.Rows)
            {
                if (fila.IsNewRow) continue;

                var row = tabla.AddRow();
                row.Format.Font.Size = 10;

                for (int i = 0; i < columnas.Count; i++)
                {
                    var valor = fila.Cells[columnas[i].Name].Value?.ToString() ?? "";
                    row.Cells[i].AddParagraph(valor);
                }
            }

            // Renderizar PDF
            var renderer = new PdfDocumentRenderer(true)
            {
                Document = doc
            };
            renderer.RenderDocument();
            renderer.PdfDocument.Save(rutaArchivo);
        }

        public void RefreshData()
        {
            //vuelve a cargar todos los datos que vienen de la base de datos
            CargarCategoriasCombo();
            CargarCategorias();
            CargarProductos();
        }
    }
}
