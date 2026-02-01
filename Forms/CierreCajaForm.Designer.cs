using System.Windows.Forms;

namespace Pinturería.Forms
{
    partial class CierreCajaForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitulo;
        private Label lblNota;
        private DataGridView dgvCierreCaja;
        private Label lblFiltroFecha;
        private MonthCalendar calendarFiltro;
        private Button btnExportar;
        private Label lblSeparador;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblNota = new Label();
            dgvCierreCaja = new DataGridView();
            lblFiltroFecha = new Label();
            calendarFiltro = new MonthCalendar();
            btnExportar = new Button();
            lblSeparador = new Label();

            ((System.ComponentModel.ISupportInitialize)dgvCierreCaja).BeginInit();
            SuspendLayout();

            // lblTitulo
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.Location = new Point(30, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Cierre de caja";
            lblTitulo.AutoSize = true;

            // lblFiltroFecha
            lblFiltroFecha.Name = "lblFiltroFecha";
            lblFiltroFecha.TabIndex = 2;
            lblFiltroFecha.Text = "Filtrar por fecha:";
            lblFiltroFecha.AutoSize = true;
            lblFiltroFecha.Font = new Font("Segoe UI", 12.5F);

            // calendarFiltro
            calendarFiltro.MaxSelectionCount = 1;
            calendarFiltro.Name = "calendarFiltro";
            calendarFiltro.TabIndex = 3;
            calendarFiltro.DateChanged += calendarFiltro_DateChanged;

            // btnExportar
            btnExportar.ImageAlign = ContentAlignment.MiddleLeft;
            btnExportar.Name = "btnExportar";
            btnExportar.TabIndex = 4;
            btnExportar.Text = "Exportar";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.AutoSize = true;
            btnExportar.Font = new Font("Segoe UI", 12.5F);
            btnExportar.TextAlign = ContentAlignment.MiddleCenter;
            btnExportar.Padding = new Padding(0, 0, 0, 4);

            // lblNota
            lblNota.Font = new Font("Segoe UI", 9F);
            lblNota.Location = new Point(30, 20);
            lblNota.Name = "lblNota";
            lblNota.TabIndex = 0;
            lblNota.Text = "*Nota: Los días que no aparecen en la tabla es porque no se registraron ventas en ese día (o se eliminaron todas las ventas de ese día).";
            lblNota.AutoSize = true;

            // dgvCierreCaja
            dgvCierreCaja.AllowUserToAddRows = false;
            dgvCierreCaja.AllowUserToDeleteRows = false;
            dgvCierreCaja.AllowUserToResizeColumns = false;
            dgvCierreCaja.AllowUserToResizeRows = false;
            dgvCierreCaja.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCierreCaja.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvCierreCaja.MultiSelect = false;
            dgvCierreCaja.Name = "dgvCierreCaja";
            dgvCierreCaja.ReadOnly = true;
            dgvCierreCaja.RowHeadersVisible = false;
            dgvCierreCaja.RowTemplate.Resizable = DataGridViewTriState.False;
            dgvCierreCaja.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCierreCaja.TabIndex = 1;
            dgvCierreCaja.ColumnHeadersHeight = 27;
            dgvCierreCaja.RowTemplate.Height = 27;
            dgvCierreCaja.Font = new Font("Segoe UI", 10F);

             // lblSeparador
            lblSeparador.BorderStyle = BorderStyle.None;
            lblSeparador.Name = "lblSeparador";
            lblSeparador.Size = new Size(750, 2);
            lblSeparador.TabIndex = 6;

            // CierreCajaForm
            AutoScroll = true;
            ClientSize = new Size(934, 561);
            Controls.Add(lblTitulo);
            Controls.Add(lblFiltroFecha);
            Controls.Add(calendarFiltro);
            Controls.Add(btnExportar);
            Controls.Add(lblNota);
            Controls.Add(dgvCierreCaja);
            Controls.Add(lblSeparador);
            Name = "CierreCajaForm";
            Text = "Cierre de caja";

            ((System.ComponentModel.ISupportInitialize)dgvCierreCaja).EndInit();
            ResumeLayout(false);
        }
    }
}
