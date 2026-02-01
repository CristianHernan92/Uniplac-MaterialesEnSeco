namespace Pinturería.Forms
{
    partial class DataVentaEntryForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.ComboBox cmbProducto;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.ComboBox cmbCantidad;
        private System.Windows.Forms.Label lblFormaPago;
        private System.Windows.Forms.ComboBox cmbFormaPago;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.lblCategoria = new System.Windows.Forms.Label();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.lblProducto = new System.Windows.Forms.Label();
            this.cmbProducto = new System.Windows.Forms.ComboBox();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.cmbCantidad = new System.Windows.Forms.ComboBox();
            this.lblFormaPago = new System.Windows.Forms.Label();
            this.cmbFormaPago = new System.Windows.Forms.ComboBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // lblCategoria
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Location = new Point(12, 15);
            this.lblCategoria.Text = "Categoría:";

            // cmbCategoria
            this.cmbCategoria.Location = new Point(110, 12);
            this.cmbCategoria.Size = new Size(220, 23);
            this.cmbCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // lblProducto
            this.lblProducto.AutoSize = true;
            this.lblProducto.Location = new Point(12, 55);
            this.lblProducto.Text = "Producto:";

            // cmbProducto
            this.cmbProducto.Location = new Point(110, 52);
            this.cmbProducto.Size = new Size(220, 23);
            this.cmbProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProducto.Enabled = false;

            // lblCantidad
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Location = new Point(12, 95);
            this.lblCantidad.Text = "Cantidad:";

            // cmbCantidad
            this.cmbCantidad.Location = new Point(110, 92);
            this.cmbCantidad.Size = new Size(100, 23);
            this.cmbCantidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCantidad.Enabled = false;

            // lblFormaPago
            this.lblFormaPago.AutoSize = true;
            this.lblFormaPago.Location = new Point(12, 135);
            this.lblFormaPago.Text = "Forma de pago:";

            // cmbFormaPago
            this.cmbFormaPago.Location = new Point(110, 132);
            this.cmbFormaPago.Size = new Size(220, 23);
            this.cmbFormaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormaPago.Enabled = false;

            // btnAceptar
            this.btnAceptar.Location = new Point(110, 175);
            this.btnAceptar.Size = new Size(100, 30);
            this.btnAceptar.Text = "Aceptar";

            // btnCancelar
            this.btnCancelar.Location = new Point(230, 175);
            this.btnCancelar.Size = new Size(100, 30);
            this.btnCancelar.Text = "Cancelar";

            // Form
            this.ClientSize = new Size(350, 220);
            this.Controls.Add(this.lblCategoria);
            this.Controls.Add(this.cmbCategoria);
            this.Controls.Add(this.lblProducto);
            this.Controls.Add(this.cmbProducto);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.cmbCantidad);
            this.Controls.Add(this.lblFormaPago);
            this.Controls.Add(this.cmbFormaPago);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnCancelar);
            this.Text = "Agregar línea de venta";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
