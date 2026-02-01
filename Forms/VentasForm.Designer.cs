using System.Windows.Forms;

namespace Pinturería.Forms
{
    partial class VentasForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private Button btnRegistrarVenta;
        private Label lblVentas;
        private Button btnExportarVentas;
        private DataGridView dgvVentas;
        private MonthCalendar mcFiltroFecha;
        private Label lblFiltroFecha;
        private Label lblSeparador;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            btnRegistrarVenta = new Button();
            lblVentas = new Label();
            btnExportarVentas = new Button();
            dgvVentas = new DataGridView();
            lblFiltroFecha = new Label();
            mcFiltroFecha = new MonthCalendar();
            lblSeparador = new Label();

            ((System.ComponentModel.ISupportInitialize)dgvVentas).BeginInit();
            SuspendLayout();

            // lblTitulo
            lblTitulo.Name = "lblTitulo";
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Registrar Venta";
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);

            // btnRegistrarVenta
            btnRegistrarVenta.Name = "btnRegistrarVenta";
            btnRegistrarVenta.TabIndex = 1;
            btnRegistrarVenta.Text = "Registrar venta";
            btnRegistrarVenta.UseVisualStyleBackColor = true;
            btnRegistrarVenta.AutoSize = true;
            btnRegistrarVenta.Font = new Font("Segoe UI", 12.5F);
            btnRegistrarVenta.TextAlign = ContentAlignment.MiddleCenter;
            btnRegistrarVenta.Padding = new Padding(0, 0, 0, 4);

            // lblVentas
            lblVentas.Name = "lblVentas";
            lblVentas.TabIndex = 3;
            lblVentas.Text = "Ventas";
            lblVentas.AutoSize = true;
            lblVentas.Font = new Font("Segoe UI", 20F, FontStyle.Bold);

            // lblFiltroFecha
            lblFiltroFecha.Name = "lblFiltroFecha";
            lblFiltroFecha.TabIndex = 5;
            lblFiltroFecha.Text = "Filtrar por fecha:";
            lblFiltroFecha.AutoSize = true;
            lblFiltroFecha.Font = new Font("Segoe UI", 12.5F);

            // mcFiltroFecha
            mcFiltroFecha.MaxSelectionCount = 1;
            mcFiltroFecha.Name = "mcFiltroFecha";
            mcFiltroFecha.TabIndex = 6;

            // btnExportarVentas
            btnExportarVentas.ImageAlign = ContentAlignment.MiddleLeft;
            btnExportarVentas.Name = "btnExportarVentas";
            btnExportarVentas.TabIndex = 4;
            btnExportarVentas.Text = "Exportar";
            btnExportarVentas.UseVisualStyleBackColor = true;
            btnExportarVentas.AutoSize = true;
            btnExportarVentas.Font = new Font("Segoe UI", 12.5F);
            btnExportarVentas.TextAlign = ContentAlignment.MiddleCenter;
            btnExportarVentas.Padding = new Padding(0, 0, 0, 4);

            // dgvVentas
            dgvVentas.AllowUserToAddRows = false;
            dgvVentas.AllowUserToDeleteRows = false;
            dgvVentas.AllowUserToResizeColumns = false;
            dgvVentas.AllowUserToResizeRows = false;
            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvVentas.MultiSelect = false;
            dgvVentas.Name = "dgvVentas";
            dgvVentas.RowHeadersVisible = false;
            dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVentas.RowTemplate.Resizable = DataGridViewTriState.False;
            dgvVentas.TabIndex = 4;
            dgvVentas.ReadOnly = true;
            dgvVentas.ColumnHeadersHeight = 27;
            dgvVentas.RowTemplate.Height = 27;
            dgvVentas.Font = new Font("Segoe UI", 10F);

            // lblSeparador
            lblSeparador.BorderStyle = BorderStyle.None;
            lblSeparador.Name = "lblSeparador";
            lblSeparador.Size = new Size(750, 2);
            lblSeparador.TabIndex = 6;

            // VentasForm
            AutoScroll = true;
            ClientSize = new Size(944, 420);
            Controls.Add(lblTitulo);
            Controls.Add(btnRegistrarVenta);
            Controls.Add(lblVentas);
            Controls.Add(btnExportarVentas);
            Controls.Add(dgvVentas);
            Controls.Add(lblFiltroFecha);
            Controls.Add(mcFiltroFecha);
            Controls.Add(lblSeparador);
            Name = "VentasForm";
            Text = "Ventas";

            ((System.ComponentModel.ISupportInitialize)dgvVentas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}