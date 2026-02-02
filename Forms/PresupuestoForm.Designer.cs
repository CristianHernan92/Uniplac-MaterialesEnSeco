namespace MaterialesEnSeco.Forms
{
    partial class PresupuestoForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private Button btnAgregarProductoAlPresupuesto;
        private DataGridView dgvPresupuesto;
        private Label lblFinalCrédito;
        private Label lblFinalEfectivo;
        private Button btnExportarPresupuesto;
        private Label lblSeparador;
        private ComboBox cmbPorcentajeEfectivo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblFinalCrédito = new Label();
            lblFinalEfectivo = new Label();
            btnAgregarProductoAlPresupuesto = new Button();
            dgvPresupuesto = new DataGridView();
            btnExportarPresupuesto = new Button();
            lblSeparador = new Label();
            cmbPorcentajeEfectivo = new ComboBox();

            ((System.ComponentModel.ISupportInitialize)dgvPresupuesto).BeginInit();
            SuspendLayout();

            // lblTitulo
            lblTitulo.Name = "lblTitulo";
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Presupuesto";
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);

            // btnAgregarProductoAlPresupuesto
            btnAgregarProductoAlPresupuesto.Name = "btnAgregarProductoAlPresupuesto";
            btnAgregarProductoAlPresupuesto.TabIndex = 1;
            btnAgregarProductoAlPresupuesto.Text = "Agregar pruducto";
            btnAgregarProductoAlPresupuesto.UseVisualStyleBackColor = true;
            btnAgregarProductoAlPresupuesto.AutoSize = true;
            btnAgregarProductoAlPresupuesto.Font = new Font("Segoe UI", 12.5F);
            btnAgregarProductoAlPresupuesto.TextAlign = ContentAlignment.MiddleCenter;
            btnAgregarProductoAlPresupuesto.Padding = new Padding(0, 0, 0, 4);

            // btnExportarPresupuesto
            btnExportarPresupuesto.ImageAlign = ContentAlignment.MiddleLeft;
            btnExportarPresupuesto.Name = "btnExportarPresupuesto";
            btnExportarPresupuesto.TabIndex = 4;
            btnExportarPresupuesto.Text = "Exportar";
            btnExportarPresupuesto.UseVisualStyleBackColor = true;
            btnExportarPresupuesto.AutoSize = true;
            btnExportarPresupuesto.Font = new Font("Segoe UI", 12.5F);
            btnExportarPresupuesto.TextAlign = ContentAlignment.MiddleCenter;
            btnExportarPresupuesto.Padding = new Padding(0, 0, 0, 4);

            // dgvPresupuesto
            dgvPresupuesto.AllowUserToAddRows = false;
            dgvPresupuesto.AllowUserToDeleteRows = false;
            dgvPresupuesto.AllowUserToResizeColumns = false;
            dgvPresupuesto.AllowUserToResizeRows = false;
            dgvPresupuesto.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvPresupuesto.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvPresupuesto.MultiSelect = false;
            dgvPresupuesto.Name = "dgvPresupuesto";
            dgvPresupuesto.ReadOnly = true;
            dgvPresupuesto.RowHeadersVisible = false;
            dgvPresupuesto.RowTemplate.Resizable = DataGridViewTriState.False;
            dgvPresupuesto.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPresupuesto.TabIndex = 4;
            dgvPresupuesto.ColumnHeadersHeight = 27;
            dgvPresupuesto.RowTemplate.Height = 27;
            dgvPresupuesto.Font = new Font("Segoe UI", 10F);
            dgvPresupuesto.ScrollBars = ScrollBars.Vertical;

            // lblFinalCrédito
            lblFinalCrédito.Name = "lblFinalCrédito";
            lblFinalCrédito.TabIndex = 0;
            lblFinalCrédito.Text = "Final Crédito:";
            lblFinalCrédito.AutoSize = true;
            lblFinalCrédito.Font = new Font("Segoe UI", 15F, FontStyle.Bold);

            // lblFinalEfectivo
            lblFinalEfectivo.Name = "lblFinalEfectivo";
            lblFinalEfectivo.TabIndex = 0;
            lblFinalEfectivo.Text = "Final Efectivo:";
            lblFinalEfectivo.AutoSize = true;
            lblFinalEfectivo.Font = new Font("Segoe UI", 15F, FontStyle.Bold);

            // cmbPorcentajeEfectivo
            cmbPorcentajeEfectivo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPorcentajeEfectivo.Name = "cmbPorcentajeEfectivo";
            cmbPorcentajeEfectivo.Size = new Size(150, 23);
            cmbPorcentajeEfectivo.TabIndex = 21;
            cmbPorcentajeEfectivo.Font = new Font("Segoe UI", 12.5F);

            // lblSeparador
            lblSeparador.BorderStyle = BorderStyle.None;
            lblSeparador.Name = "lblSeparador";
            lblSeparador.Size = new Size(750, 2);
            lblSeparador.TabIndex = 6;

            // PresupuestoForm
            AutoScroll = true;
            ClientSize = new Size(944, 420);
            Controls.Add(lblTitulo);
            Controls.Add(lblFinalCrédito);
            Controls.Add(lblFinalEfectivo);
            Controls.Add(cmbPorcentajeEfectivo);
            Controls.Add(btnAgregarProductoAlPresupuesto);
            Controls.Add(btnExportarPresupuesto);
            Controls.Add(dgvPresupuesto);
            Controls.Add(lblSeparador);
            Name = "PresupuestoForm";
            Text = "Presupuesto";

            ((System.ComponentModel.ISupportInitialize)dgvPresupuesto).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}