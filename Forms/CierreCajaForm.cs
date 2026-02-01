using Pinturería.Data;
using Pinturería.Models;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;

namespace Pinturería.Forms
{
    public partial class CierreCajaForm : Form
    {
        public CierreCajaForm()
        {
            InitializeComponent();

            btnExportar.Click += btnExportar_Click;

            ConfigurarCalendario();
            CargarCierreCaja(DateTime.Today);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            lblFiltroFecha.Location = new Point(30, lblTitulo.Bottom + 20);
            calendarFiltro.Location = new Point(30, lblFiltroFecha.Bottom + 5);
            btnExportar.Location = new Point(30, calendarFiltro.Bottom + 20);
            lblNota.Location = new Point(30, btnExportar.Bottom + 10);
            dgvCierreCaja.Location = new Point(30, lblNota.Bottom + 10);

            //Altura y anchura del dgvCierreCaja
            dgvCierreCaja.Width = ((int)(this.ClientSize.Width * 0.50) * 2) - 30;
            dgvCierreCaja.Height = (int)(this.ClientSize.Height * 1.5);

            lblSeparador.Location = new Point(30, dgvCierreCaja.Bottom + 30);
        }

        private void ConfigurarCalendario()
        {
            using (var db = new AppDbContext())
            {
                var primeraFecha = db.Ventas.Min(v => (DateTime?)v.Fecha);

                if (primeraFecha.HasValue)
                {
                    calendarFiltro.MinDate = primeraFecha.Value.Date;
                    calendarFiltro.MaxDate = DateTime.Today;
                    calendarFiltro.SetDate(DateTime.Today);
                }
                else
                {
                    // No hay ventas, bloquear calendario en hoy
                    calendarFiltro.MinDate = DateTime.Today;
                    calendarFiltro.MaxDate = DateTime.Today;
                    calendarFiltro.SetDate(DateTime.Today);
                }
            }
        }

        private void calendarFiltro_DateChanged(object sender, DateRangeEventArgs e)
        {
            CargarCierreCaja(e.Start);
        }

        private void CargarCierreCaja(DateTime fecha)
        {
            dgvCierreCaja.Columns.Clear();

            // Crear columnas si no existen
            if (dgvCierreCaja.Columns.Count == 0)
            {
                dgvCierreCaja.Columns.Add("Fecha", "Fecha");
                dgvCierreCaja.Columns.Add("TotalEfectivo", "Efectivo");
                dgvCierreCaja.Columns.Add("TotalDebito", "Débito");
                dgvCierreCaja.Columns.Add("TotalCredito", "Crédito");
                dgvCierreCaja.Columns.Add("TotalMercadoPago", "Mercado Pago");
                dgvCierreCaja.Columns.Add("TotalCuentaCorriente", "C.Corriente");
                dgvCierreCaja.Columns.Add("TotalTransferencias", "Transferencias");
                dgvCierreCaja.Columns.Add("TotalGeneral", "Total");
            }

            using (var db = new AppDbContext())
            {
                var ventasQuery = from v in db.Ventas
                                where v.Fecha.Date <= fecha.Date
                                join dv in db.DataVentas on v.Id equals dv.Venta_Id
                                select new { v.Fecha, dv.FormaPago, dv.Cantidad, dv.Precio };

                var ventas = ventasQuery.ToList();

                if (ventas.Any())
                {
                    var ventasPorDia = ventas
                        .GroupBy(v => v.Fecha.Date)
                        .OrderBy(g => g.Key);

                    foreach (var grupo in ventasPorDia)
                    {
                        decimal totalEfectivo = grupo.Where(x => x.FormaPago == "Efectivo").Sum(x => x.Precio * x.Cantidad);
                        decimal totalDebito = grupo.Where(x => x.FormaPago == "Débito").Sum(x => x.Precio * x.Cantidad);
                        decimal totalCredito = grupo.Where(x => x.FormaPago == "Crédito").Sum(x => x.Precio * x.Cantidad);
                        decimal totalMercadoPago = grupo.Where(x => x.FormaPago == "Mercado Pago").Sum(x => x.Precio * x.Cantidad);
                        decimal totalCuentaCorriente = grupo.Where(x => x.FormaPago == "Cuenta corriente").Sum(x => x.Precio * x.Cantidad);
                        decimal totalTransferencias = grupo.Where(x => x.FormaPago == "Transferencia").Sum(x => x.Precio * x.Cantidad);
                        decimal totalGeneral = grupo.Sum(x => x.Precio * x.Cantidad);

                        dgvCierreCaja.Rows.Add(
                            grupo.Key.ToShortDateString(),
                            totalEfectivo,
                            totalDebito,
                            totalCredito,
                            totalMercadoPago,
                            totalCuentaCorriente,
                            totalTransferencias,
                            totalGeneral
                        );
                    }
                }
                else
                {
                    dgvCierreCaja.Rows.Add(fecha.ToShortDateString(), 0, 0, 0, 0, 0, 0, 0);
                }
            }
        }


        private void btnExportar_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog
            {
                Filter = "Archivos PDF|*.pdf",
                Title = "Guardar reporte como PDF",
                FileName = $"CierreCaja_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    GenerarPDF_MigraDoc(sfd.FileName, "Reporte de cierre de caja", dgvCierreCaja);
                    MessageBox.Show("Reporte exportado correctamente.",
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

            // 🔎 Nota aclaratoria
            var nota = seccion.AddParagraph(
                "*Nota: Los días que no aparecen en la tabla es porque no se registraron ventas en ese día (o se eliminaron todas las ventas de ese día)."
            );
            nota.Format.Font.Size = 9;
            nota.Format.Font.Italic = true;
            nota.Format.SpaceBefore = "0.8cm";
            nota.Format.SpaceAfter = "0.6cm";
            nota.Format.Alignment = ParagraphAlignment.Left;

            //
            //TABLA
            //

            // Crear tabla
            var tabla = seccion.AddTable();
            tabla.Borders.Width = 0.75;
            tabla.Format.Font.Size = 9;

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
            anchos = [PorcentajeDe28_5(12.5), PorcentajeDe28_5(12.5), PorcentajeDe28_5(12.5), PorcentajeDe28_5(12.5), PorcentajeDe28_5(12.5), PorcentajeDe28_5(12.5), PorcentajeDe28_5(12.5), PorcentajeDe28_5(12.5)];

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
            ConfigurarCalendario();
            CargarCierreCaja(DateTime.Today);
        }
    }
}