using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using Pinturería.Data;
using Pinturería.Models;
using System.Windows.Forms;

namespace Pinturería.Forms
{
    public partial class PresupuestoForm : Form
    {
        public List<string> _ProductosSeleccionados { get; set; } = new List<string>();
        public decimal totalPresupuesto = 0;

        private class DataPresupuestoTemp
        {
            public string Categoria { get; set; }
            public string Producto { get; set; }
            public int Cantidad { get; set; }
            public decimal Precio { get; set; }
        }

        private List<DataPresupuestoTemp> ObtenerLineasDesdeGrilla()
{
            var lista = new List<DataPresupuestoTemp>();

            foreach (DataGridViewRow row in dgvPresupuesto.Rows)
            {
                if (row.IsNewRow) continue;

                lista.Add(new DataPresupuestoTemp
                {
                    Cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value),
                    Producto = row.Cells["Producto"].Value.ToString(),
                    Precio = Convert.ToDecimal(row.Cells["Precio"].Value),
                    // La categoría no está en la grilla, la dejamos vacía:
                    Categoria = ""
                });
            }

            return lista;
        }
        
        public PresupuestoForm()
        {
            InitializeComponent();

            btnAgregarProductoAlPresupuesto.Click += BtnAgregarProductoAlPresupuesto;
            btnExportarPresupuesto.Click += btnExportarPresupuesto_Click;
            cmbPorcentajeEfectivo.SelectedIndexChanged += cmbPorcentajeEfectivo_SelectedIndexChanged;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            lblTitulo.Location = new Point(30, 20);
            btnAgregarProductoAlPresupuesto.Location = new Point(30, lblTitulo.Bottom + 20);
            btnExportarPresupuesto.Location = new Point(30, btnAgregarProductoAlPresupuesto.Bottom + 25);
            dgvPresupuesto.Location = new Point(30, btnExportarPresupuesto.Bottom + 10);

            //Altura y anchura del dgvPresupuesto
            dgvPresupuesto.Width = ((int)(this.ClientSize.Width * 0.57) * 2);
            dgvPresupuesto.Height = (int)(this.ClientSize.Height * 0.5);

            //Agregar columnas a la tabla
            dgvPresupuesto.Columns.Clear();
            dgvPresupuesto.Columns.Add("Cantidad", "Cantidad");
            dgvPresupuesto.Columns.Add("Producto", "Producto");
            dgvPresupuesto.Columns.Add("Precio", "Precio");
            dgvPresupuesto.Columns.Add("Total", "Total");
            DataGridViewImageColumn eliminarCol = new DataGridViewImageColumn
            {
                Name = "Eliminar",
                HeaderText = "",
                Image = Properties.Resources.iconEliminar,
                Width = 30,
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };
            dgvPresupuesto.Columns.Add(eliminarCol);

            int anchoTotal = dgvPresupuesto.Width;
            dgvPresupuesto.Columns["Cantidad"].Width = (int)(anchoTotal * 0.07);
            dgvPresupuesto.Columns["Producto"].Width = (int)(anchoTotal * 0.68);
            dgvPresupuesto.Columns["Precio"].Width = (int)(anchoTotal * 0.10);
            dgvPresupuesto.Columns["Total"].Width = (int)(anchoTotal * 0.10);
            dgvPresupuesto.Columns["Eliminar"].Width = (int)(anchoTotal * 0.05);

            dgvPresupuesto.CellClick += dgvPresupuesto_CellClick;

            lblFinalCrédito.Location = new Point(30, dgvPresupuesto.Bottom + 20);
            lblFinalEfectivo.Location = new Point(30, lblFinalCrédito.Bottom + 10);

            cmbPorcentajeEfectivo.Location = new Point(lblFinalEfectivo.Location.X, lblFinalEfectivo.Bottom + 3);
            cmbPorcentajeEfectivo.DisplayMember = "Texto";
            cmbPorcentajeEfectivo.ValueMember = "Valor";
            cmbPorcentajeEfectivo.DataSource = Enumerable.Range(0, 101)
                .Select(i => new { Valor = i, Texto = $"{i}%" })
                .ToList();
            cmbPorcentajeEfectivo.SelectedValue = 100;

            lblSeparador.Location = new Point(30, cmbPorcentajeEfectivo.Bottom + 30);

        }

        private void dgvPresupuesto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Ignorar header

            // ELIMINAR
            if (dgvPresupuesto.Columns[e.ColumnIndex].Name == "Eliminar")
            {
                EliminarFila(e.RowIndex);
                return;
            }
        }

        private void EliminarFila(int rowIndex)
        {
            if (MessageBox.Show(
                    "¿Desea eliminar este producto del presupuesto?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                ) == DialogResult.Yes)
            {
                dgvPresupuesto.Rows.RemoveAt(rowIndex);
                RecalcularTotales();
            }
        }

        private void RecalcularTotales()
        {
            totalPresupuesto = 0;

            foreach (DataGridViewRow row in dgvPresupuesto.Rows)
            {
                if (row.IsNewRow) continue;

                decimal totalFila = Convert.ToDecimal(row.Cells["Total"].Value);
                totalPresupuesto += totalFila;
            }

            lblFinalCrédito.Text = "Final Crédito : $" + totalPresupuesto.ToString("N2");

            decimal porc = Convert.ToDecimal(cmbPorcentajeEfectivo.SelectedValue);
            decimal totalEfectivo = totalPresupuesto - (totalPresupuesto * porc / 100);

            lblFinalEfectivo.Text = "Final Efectivo : $" + totalEfectivo.ToString("N2");
        }

        private void cmbPorcentajeEfectivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ACTUALIZAMOS EL LABEL DE "FINAL EFECTIVO"
            this.lblFinalEfectivo.Text = "Final Efectivo : $" + (totalPresupuesto - (totalPresupuesto * Convert.ToDecimal(cmbPorcentajeEfectivo.SelectedValue) / 100)).ToString();
        }

        private async void BtnAgregarProductoAlPresupuesto(object sender, EventArgs e)
        {
            var entrada = new DataPresupuestoTemp();

            //
            //COMIENZO FORMULARIO PRODUCTO
            //

            using (var entryForm = new DataPresupuestoEntryForm(_ProductosSeleccionados))
            {
                entryForm.Text = $"Agregar Producto";
                var dr = entryForm.ShowDialog();

                if (dr != DialogResult.OK)
                {
                    _ProductosSeleccionados.Clear();
                    MessageBox.Show("Ingreso cancelado.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (entryForm.SelectedCantidad <= 0)
                {
                    MessageBox.Show("No se permiten cantidades 0. Cancelado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //DATOS INGRESADOS POR EL USUARIO
                entrada = new DataPresupuestoTemp
                {
                    Categoria = entryForm.SelectedCategoria,
                    Producto = entryForm.SelectedProducto,
                    Cantidad = entryForm.SelectedCantidad,
                    Precio = entryForm.SelectedPrecio
                };

                // VERIFICAR SI EL PRODUCTO YA EXISTE EN EL DGV
                foreach (DataGridViewRow row in dgvPresupuesto.Rows)
                {
                    if (row.Cells["Producto"].Value != null &&
                        row.Cells["Producto"].Value.ToString() == entrada.Producto)
                    {
                        MessageBox.Show(
                            "El producto ya existe en el presupuesto",
                            "Producto repetido",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        return; // Cancelar el agregado
                    }
                }

                //INSERTAMOS LOS DATOS EN LA TABLA DGV
                dgvPresupuesto.Rows.Add(
                    entrada.Cantidad,
                    entrada.Producto,            
                    entrada.Precio,            
                    entrada.Cantidad * entrada.Precio     
                );

                //SUMAMOS AL TOTAL
                totalPresupuesto += entrada.Cantidad * entrada.Precio;

                //ACTUALIZAMOS EL LABEL DE "FINAL CRÉDITO"
                this.lblFinalCrédito.Text = "Final Crédito : $" + totalPresupuesto.ToString();
                //ACTUALIZAMOS EL LABEL DE "FINAL EFECTIVO"
                this.lblFinalEfectivo.Text = "Final Efectivo : $" + (totalPresupuesto - (totalPresupuesto * Convert.ToDecimal(cmbPorcentajeEfectivo.SelectedValue) / 100)).ToString();

            }

            _ProductosSeleccionados.Clear();
        }

        private void btnExportarPresupuesto_Click(object sender, EventArgs e)
        {
            var lineas = ObtenerLineasDesdeGrilla();
            exportarPresupuesto(
                 lineas,
                 totalPresupuesto,
                 totalPresupuesto - (totalPresupuesto * Convert.ToDecimal(cmbPorcentajeEfectivo.SelectedValue) / 100),
                 true,
                 Convert.ToDecimal(cmbPorcentajeEfectivo.SelectedValue)
            );
        }

        private void exportarPresupuesto(
                List<DataPresupuestoTemp> entrada,
                decimal totalConTarjeta,
                decimal totalConEfectivo,
                bool esDescuento,
                decimal porc)
        {
            using (var sfd = new SaveFileDialog
            {
                Filter = "Archivos PDF|*.pdf",
                Title = "Guardar presupuesto como PDF",
                FileName = $"Presupuesto_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string destinatarioIngresado = Microsoft.VisualBasic.Interaction.InputBox(
                        "Ingrese el nombre del destinatario:",
                        "Señor/es",
                        ""
                        );
                    while (destinatarioIngresado.Trim() == "")
                    {
                        MessageBox.Show("Debe ingresar un destinatario.");
                        destinatarioIngresado = Microsoft.VisualBasic.Interaction.InputBox(
                        "Ingrese el nombre del destinatario:",
                        "Señor/es",
                        ""
                        );
                    }

                    // Llamamos a GenerarPDF_MigraDoc
                    GenerarPDF_MigraDoc(
                        sfd.FileName,
                        "Presupuesto",
                        entrada,
                        totalConTarjeta,                         
                        totalConEfectivo,      
                        esDescuento,
                        porc,                          
                        destinatarioIngresado
                    );

                    MessageBox.Show("Presupuesto exportado correctamente.",
                                    "Éxito",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
        }

        private void GenerarPDF_MigraDoc(
                        string rutaArchivo, 
                        string titulo,
                        List<DataPresupuestoTemp> lineas,
                        decimal totalTarjeta,
                        decimal totalEfectivo,
                        bool esDescuento,
                        decimal porcEfectivo,
                        string destinatario)
        {
            var doc = new Document();
            var seccion = doc.AddSection();

            seccion.PageSetup.PageFormat = PageFormat.A4;
            seccion.PageSetup.Orientation = MigraDoc.DocumentObjectModel.Orientation.Portrait;

            seccion.PageSetup.LeftMargin = Unit.FromCentimeter(1.0);
            seccion.PageSetup.RightMargin = Unit.FromCentimeter(1.0);
            seccion.PageSetup.TopMargin = Unit.FromCentimeter(1.0);
            seccion.PageSetup.BottomMargin = Unit.FromCentimeter(1.0);

            // ----------------------------------------------------------
            // ENCABEZADO: LOGO (IZQ) + UNIPLAC / DIRECCIÓN (DER)
            // ----------------------------------------------------------
            var tablaHeader = seccion.AddTable();
            tablaHeader.Borders.Width = 0;

            tablaHeader.AddColumn(Unit.FromCentimeter(5));   // Logo
            tablaHeader.AddColumn(Unit.FromCentimeter(14.3));  // Uniplac + dirección

            var filaHeader = tablaHeader.AddRow();
            filaHeader.Cells[0].VerticalAlignment = VerticalAlignment.Top;
            filaHeader.Cells[1].VerticalAlignment = VerticalAlignment.Top;
            filaHeader.HeightRule = RowHeightRule.AtLeast;
            filaHeader.Height = Unit.FromCentimeter(1.8);

            // --- LOGO ---
            string rutaLogo = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                @"Utils\logo",
                "logo-uniplac.jpg"
            );

            if (File.Exists(rutaLogo))
            {
                var logo = filaHeader.Cells[0].AddImage(rutaLogo);
                logo.LockAspectRatio = true;
                logo.Width = Unit.FromCentimeter(3.5);
            }

            // --- UNIPLAC / DIRECCIÓN ---
            var parUniplac = filaHeader.Cells[1].AddParagraph(
                "ENSENADA UNIPLAC SRL\n" +
                "Direccion: ALBERDI 205 ENSENADA --- 1900\n" +
                "Tel.: 2216441333\n" +
                "CUIT: 30-71656796-2"
            );

            parUniplac.Format.Alignment = ParagraphAlignment.Right;
            parUniplac.Format.Font.Size = 10;
            parUniplac.Format.Font.Bold = true;

            // ---- FECHA ----
            var fechaPar = seccion.AddParagraph(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            fechaPar.Format.Font.Size = 9;
            fechaPar.Format.Alignment = ParagraphAlignment.Right;
            fechaPar.Format.SpaceAfter = Unit.FromCentimeter(0.4);
            fechaPar.Format.SpaceBefore = Unit.FromCentimeter(1);

            // ----------------------------------------------------------
            // SEÑOR/ES + DESTINATARIO
            // ----------------------------------------------------------

            var senores = seccion.AddParagraph("Señor/es:");
            senores.Format.Font.Size = 12;
            senores.Format.Font.Bold = true;

            var destinatarioPar = seccion.AddParagraph(destinatario);
            destinatarioPar.Format.Font.Size = 12;
            destinatarioPar.Format.SpaceAfter = Unit.FromCentimeter(0.5);

            // ----------------------------------------------------------
            // TÍTULO PRINCIPAL
            // ----------------------------------------------------------

            var tituloPar = seccion.AddParagraph("PRESUPUESTO");
            tituloPar.Format.Font.Size = 18;
            tituloPar.Format.Font.Bold = true;
            tituloPar.Format.Alignment = ParagraphAlignment.Center;
            tituloPar.Format.SpaceAfter = Unit.FromCentimeter(0.8);

            // ----------------------------------------------------------
            // TABLA DE PRODUCTOS
            // ----------------------------------------------------------

            var table = seccion.AddTable();
            table.Borders.Width = 0.5;
            table.Format.Font.Size = 12;

            table.AddColumn(Unit.FromCentimeter(2));
            table.AddColumn(Unit.FromCentimeter(10));
            table.AddColumn(Unit.FromCentimeter(3));
            table.AddColumn(Unit.FromCentimeter(3));

            var header = table.AddRow();
            header.Shading.Color = Colors.LightGray;
            header.Cells[0].AddParagraph("Cantidad");
            header.Cells[1].AddParagraph("Producto");
            header.Cells[2].AddParagraph("Precio");
            header.Cells[3].AddParagraph("Total");

            foreach (var l in lineas)
            {
                var row = table.AddRow();
                row.Cells[0].AddParagraph(l.Cantidad.ToString());
                row.Cells[1].AddParagraph(l.Producto);
                row.Cells[2].AddParagraph(l.Precio.ToString("N2"));
                row.Cells[3].AddParagraph((l.Precio * l.Cantidad).ToString("N2"));
            }

            seccion.AddParagraph("").Format.SpaceAfter = Unit.FromCentimeter(0.3);

            // ----------------------------------------------------------
            // TOTALES
            // ----------------------------------------------------------

            var totalEfec = seccion.AddParagraph($"Total con tarjeta de crédito $: {totalTarjeta:N2}");
            totalEfec.Format.Font.Size = 12;
            totalEfec.Format.Font.Bold = true;
            totalEfec.Format.Alignment = ParagraphAlignment.Right;
            totalEfec.Format.SpaceAfter = Unit.FromCentimeter(0.2);

            string signo = esDescuento ? "-" : "+";

            var totalTar = seccion.AddParagraph(
                $"Total en efectivo ({signo}{porcEfectivo}%) $: {totalEfectivo:N2}"
            );
            totalTar.Format.Font.Size = 12;
            totalTar.Format.Font.Bold = true;
            totalTar.Format.Alignment = ParagraphAlignment.Right;
            totalTar.Format.SpaceAfter = Unit.FromCentimeter(1.0);

            // ----------------------------------------------------------
            // CONDICIONES
            // ----------------------------------------------------------

            var condiciones_primero = seccion.AddParagraph(
                "Importes al día de la fecha. Cualquier variación impositiva afectará la liquidación final"
            );
            condiciones_primero.Format.Font.Size = 12;
            condiciones_primero.Format.Font.Italic = true;
            condiciones_primero.Format.SpaceAfter = Unit.FromCentimeter(0.25);

            var condiciones_segundo = seccion.AddParagraph(
                "PLAZO Y LUGAR DE ENTREGA:\n" +
                "CONDICIONES DE PAGO:\n" +
                "VIGENCIA DEL PRESUPUESTO:"
            );
            condiciones_segundo.Format.Font.Size = 12;
            condiciones_segundo.Format.Font.Bold = true;
            condiciones_segundo.Format.SpaceAfter = Unit.FromCentimeter(1.0);

            // ----------------------------------------------------------
            // ESPACIO ANTES DEL PIE (para empujarlo hacia abajo)
            // ----------------------------------------------------------

            var espacio = seccion.AddParagraph();
            espacio.Format.SpaceBefore = Unit.FromCentimeter(10);

            // ----------------------------------------------------------
            // PIE DE PÁGINA (invisible, porque el contenido pasó arriba)
            // ----------------------------------------------------------

            var renderer = new PdfDocumentRenderer(true)
            {
                Document = doc
            };
            renderer.RenderDocument();
            renderer.PdfDocument.Save(rutaArchivo);
        }

    }
}
