using MaterialesEnSeco.Data;
using MaterialesEnSeco.Models;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;

namespace MaterialesEnSeco.Forms
{
    public partial class VentasForm : Form
    {
        public List<string> _ProductosSeleccionados { get; set; } = new List<string>();

        public VentasForm()
        {
            InitializeComponent();

            btnRegistrarVenta.Click += BtnRegistrarVenta_Click;
            mcFiltroFecha.DateChanged += McFiltroFecha_DateChanged;
            lblFiltroFecha.Click += lblFiltroFecha_Click;
            btnExportarVentas.Click += btnExportarVentas_Click;
            dgvVentas.CellContentClick += dgvVentas_CellContentClick;

            CargarRangoFechas();
            CargarVentas(DateTime.Today);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            lblTitulo.Location = new Point(30, 20);
            btnRegistrarVenta.Location = new Point(30, lblTitulo.Bottom + 15);
            lblVentas.Location = new Point(30, btnRegistrarVenta.Bottom + 20);
            lblFiltroFecha.Location = new Point(30, lblVentas.Bottom + 10);
            mcFiltroFecha.Location = new Point(30, lblFiltroFecha.Bottom + 5);
            btnExportarVentas.Location = new Point(30, mcFiltroFecha.Bottom + 20);
            dgvVentas.Location = new Point(30, btnExportarVentas.Bottom + 10);

            //Altura y anchura del dgvVentas
            dgvVentas.Width = ((int)(this.ClientSize.Width * 0.50) * 2) - 30;
            dgvVentas.Height = (int)(this.ClientSize.Height * 1.5);

            lblSeparador.Location = new Point(30, dgvVentas.Bottom + 30);
        }

        private void dgvVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvVentas.Columns[e.ColumnIndex].Name != "Eliminar")
                return;

            var idCell = dgvVentas.Rows[e.RowIndex].Cells["Id"].Value;
            if (idCell == null || idCell.ToString() == "")
                return; // seguridad extra

            int ventaId = Convert.ToInt32(idCell);

            var confirm = MessageBox.Show(
                $"¿Está seguro que desea eliminar la venta N° {ventaId}?\n\n" +
                "Se eliminarán todos los productos asociados.",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm != DialogResult.Yes)
                return;

            using (var db = new AppDbContext())
            {
                var venta = db.Ventas.FirstOrDefault(v => v.Id == ventaId);
                if (venta == null) return;

                var detalles = db.DataVentas.Where(dv => dv.Venta_Id == ventaId).ToList();

                // ✅ Opcional: devolver stock
                foreach (var d in detalles)
                {
                    var producto = db.Productos.FirstOrDefault(p => p.Nombre == d.Producto);
                    if (producto != null)
                        producto.Stock += d.Cantidad;
                }

                db.DataVentas.RemoveRange(detalles);
                db.Ventas.Remove(venta);
                db.SaveChanges();
            }

            CargarVentas(mcFiltroFecha.SelectionStart);

            MessageBox.Show("Venta eliminada correctamente.",
                            "Eliminado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void McFiltroFecha_DateChanged(object sender, DateRangeEventArgs e)
        {
            CargarVentas(e.Start);
        }

        private void CargarVentas(DateTime fecha)
        {
            using (var db = new AppDbContext())
            {
                dgvVentas.Columns.Clear();

                // Crear columnas si no existen
                if (dgvVentas.Columns.Count == 0)
                {
                    dgvVentas.Columns.Add("Id", "ID");
                    dgvVentas.Columns.Add("Fecha", "Fecha");
                    dgvVentas.Columns.Add("Categoria", "Categoría");
                    dgvVentas.Columns.Add("Producto", "Producto");
                    dgvVentas.Columns.Add("Precio", "Precio");
                    dgvVentas.Columns.Add("Cantidad", "Cantidad");
                    dgvVentas.Columns.Add("FormaPago", "Forma de pago");
                    dgvVentas.Columns.Add("Total", "Total");

                    // ✅ Columna eliminar
                    var eliminarCol = new DataGridViewImageColumn
                    {
                        Name = "Eliminar",
                        HeaderText = "",
                        Width = 30,
                        ImageLayout = DataGridViewImageCellLayout.Zoom,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    };

                    dgvVentas.Columns.Add(eliminarCol);
                }

                // Traemos ventas del día
                var ventas = db.Ventas
                               .Where(v => v.Fecha.Date == fecha.Date)
                               .OrderBy(v => v.Fecha.Date)
                               .ToList();

                foreach (var venta in ventas)
                {
                    // Traemos los DataVentas de esta venta
                    var detalles = db.DataVentas
                                     .Where(dv => dv.Venta_Id == venta.Id)
                                     .ToList();

                    bool primeraFila = true;
                    foreach (var det in detalles)
                    {
                        if (primeraFila)
                        {
                            int rowIndex = dgvVentas.Rows.Add(
                                venta.Id,
                                venta.Fecha.ToString("g"),
                                det.Categoria,
                                det.Producto,
                                det.Precio,
                                det.Cantidad,
                                det.FormaPago,
                                venta.Total,
                                Properties.Resources.blanco
                            );

                            // ✅ ahora sí, solo esta celda tiene imagen
                            dgvVentas.Rows[rowIndex].Cells["Eliminar"].Value = Properties.Resources.iconEliminar;

                            primeraFila = false;
                        }
                        else
                        {
                            dgvVentas.Rows.Add(
                                "", "",
                                det.Categoria,
                                det.Producto,
                                det.Precio,
                                det.Cantidad,
                                det.FormaPago,
                                "",
                                Properties.Resources.blanco
                            );
                        }
                    }

                    if (!detalles.Any())
                    {
                        int rowIndex = dgvVentas.Rows.Add(
                            venta.Id,
                            venta.Fecha.ToString("g"),
                            "", "", "", "", "",
                            venta.Total,
                            Properties.Resources.blanco
                        );

                        dgvVentas.Rows[rowIndex].Cells["Eliminar"].Value = Properties.Resources.iconEliminar;
                    }
                }
            }
        }


        private void CargarRangoFechas()
        {
            using (var db = new AppDbContext())
            {
                var primeraFecha = db.Ventas.Min(v => (DateTime?)v.Fecha);

                if (primeraFecha.HasValue)
                {
                    mcFiltroFecha.MinDate = primeraFecha.Value.Date; // primera venta
                    mcFiltroFecha.MaxDate = DateTime.Today; // hasta hoy
                    mcFiltroFecha.SetDate(DateTime.Today); // por defecto hoy
                }
                else
                {
                    // si no hay ventas, bloqueamos el calendario en hoy
                    mcFiltroFecha.MinDate = DateTime.Today;
                    mcFiltroFecha.MaxDate = DateTime.Today;
                    mcFiltroFecha.SetDate(DateTime.Today);
                }
            }
        }

        private async void BtnRegistrarVenta_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Ingrese la cantidad de productos (sin contar cuánto de cada uno) se vendieron en esta nueva venta",
                "Cantidad de productos",
                "1");

            if (string.IsNullOrWhiteSpace(input)) return;

            if (!int.TryParse(input, out int cantidadLineas) || cantidadLineas < 0)
            {
                MessageBox.Show("Ingrese un número entero no negativo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cantidadLineas == 0)
            {
                MessageBox.Show("No se registró ninguna línea (cantidad 0).", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var entradas = new List<DataVentaTemp>();

            for (int i = 0; i < cantidadLineas; i++)
            {
                using (var entryForm = new DataVentaEntryForm(_ProductosSeleccionados))
                {
                    entryForm.Text = $"Línea {i + 1} de {cantidadLineas}";
                    var dr = entryForm.ShowDialog();

                    if (dr != DialogResult.OK)
                    {
                         _ProductosSeleccionados.Clear();
                        MessageBox.Show("Ingreso cancelado, no se registró la venta.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    decimal precioFinal = entryForm.SelectedPrecio;

                    // ⚠️ Si es venta en EFECTIVO, permitir descuento o recargo
                    if (entryForm.SelectedFormaPago.Equals("Efectivo", StringComparison.OrdinalIgnoreCase))
                    {
                        // Preguntar si desea aplicar descuento o recargo
                        var opcion = MessageBox.Show(
                            "Sí = Descontar\n" +
                            "No = Aumentar",
                            "Efectivo - Descontar/Aumentar porcentaje",
                            MessageBoxButtons.YesNoCancel,
                            MessageBoxIcon.Question
                        );

                        if (opcion == DialogResult.Cancel)
                        {
                            _ProductosSeleccionados.Clear();
                            MessageBox.Show("Operación cancelada. No se registrará la venta.", "Cancelado",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        bool esDescuento = (opcion == DialogResult.Yes);

                        while (true)
                        {
                            string inputPorcentaje = Microsoft.VisualBasic.Interaction.InputBox(
                                $"Ingrese el porcentaje que desea {(esDescuento ? "descontar" : "aumentar")}:\n" +
                                "- Valores de 0 a 100 (sin %)\n" +
                                "- Use coma ',' o punto '.' para decimales",
                                esDescuento ? "Descuento" : "Recargo",
                                "0"
                            );

                            // Cancelar → aborta toda la venta
                            if (inputPorcentaje == "")
                            {
                                _ProductosSeleccionados.Clear();
                                MessageBox.Show("Operación cancelada. No se registrará la venta.", "Cancelado",
                                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            inputPorcentaje = inputPorcentaje.Replace('.', ',');

                            if (decimal.TryParse(inputPorcentaje, out decimal porc) &&
                                porc >= 0 && porc <= 100)
                            {
                                if (esDescuento)
                                    precioFinal -= precioFinal * (porc / 100m);
                                else
                                    precioFinal += precioFinal * (porc / 100m);

                                precioFinal = Math.Round(precioFinal, 2);
                                break;
                            }

                            MessageBox.Show("Ingrese un valor válido entre 0 y 100.", "Valor inválido",
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                    var linea = new DataVentaTemp
                    {
                        Categoria = entryForm.SelectedCategoria,
                        Producto = entryForm.SelectedProducto,
                        Cantidad = entryForm.SelectedCantidad,
                        FormaPago = entryForm.SelectedFormaPago,
                        Precio = precioFinal
                    };

                    if (linea.Cantidad <= 0)
                    {
                        MessageBox.Show("No se permiten cantidades 0. Venta cancelada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    entradas.Add(linea);
                }
            }

            // --- Persistencia en base de datos ---
            using (var db = new AppDbContext())
            {
                var venta = new Venta
                {
                    Fecha = DateTime.Now,
                    Total = 0m
                };

                db.Ventas.Add(venta);
                db.SaveChanges();

                decimal total = 0m;

                foreach (var linea in entradas)
                {
                    var dv = new DataVenta
                    {
                        Venta_Id = venta.Id,
                        Categoria = linea.Categoria,
                        Producto = linea.Producto,
                        Precio = linea.Precio,
                        Cantidad = linea.Cantidad,
                        FormaPago = linea.FormaPago
                    };

                    db.DataVentas.Add(dv);

                    var productoEntity = db.Productos.FirstOrDefault(p => p.Nombre == linea.Producto);
                    if (productoEntity != null)
                        productoEntity.Stock = Math.Max(0, productoEntity.Stock - linea.Cantidad);

                    total += linea.Precio * linea.Cantidad;
                }

                db.SaveChanges();

                venta.Total = total;
                db.SaveChanges();

                CargarVentas(DateTime.Today);
                _ProductosSeleccionados.Clear();

                MessageBox.Show($"Venta registrada correctamente. Total: {total:C2}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        // DTO temporal para recolección
        private class DataVentaTemp
        {
            public string Categoria { get; set; }
            public string Producto { get; set; }
            public int Cantidad { get; set; }
            public string FormaPago { get; set; }
            public decimal Precio { get; set; }
        }

        private void lblFiltroFecha_Click(object sender, EventArgs e)
        {

        }

        private void btnExportarVentas_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog
            {
                Filter = "Archivos PDF|*.pdf",
                Title = "Guardar reporte de ventas como PDF",
                FileName = $"Ventas_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    // Llamamos a GenerarPDF_MigraDoc, pasando el DataGridView de ventas
                    GenerarPDF_MigraDoc(sfd.FileName, "Reporte de Ventas", dgvVentas);

                    MessageBox.Show("Reporte de ventas exportado correctamente.",
                                    "Éxito",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
        }

        private void GenerarPDF_MigraDoc(string rutaArchivo, string titulo, DataGridView dgv)
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
            tabla.Rows.LeftIndent = 0;

            //
            //COLUMNAS
            //

            // Crear columnas según las visibles del DataGridView (excepto imágenes)
            var columnas = dgv.Columns
                            .Cast<DataGridViewColumn>()
                            .Where(c => c.Visible && c.GetType() != typeof(DataGridViewImageColumn))
                            .ToList();

            double PorcentajeDe28_5(double porcentaje)
            {
                return (28.5 * porcentaje) / 100.0;
            }

            double[] anchos = [];
            int index = 0;
            anchos = [PorcentajeDe28_5(4), PorcentajeDe28_5(8), PorcentajeDe28_5(20), PorcentajeDe28_5(37), PorcentajeDe28_5(8), PorcentajeDe28_5(6), PorcentajeDe28_5(9), PorcentajeDe28_5(8)];

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
            CargarRangoFechas();
            CargarVentas(DateTime.Today);
        }
    }
}
