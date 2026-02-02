namespace MaterialesEnSeco
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem productosMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ventasMenuItem;
        private System.Windows.Forms.ToolStripMenuItem presupuestoMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cierreCajaMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refrescarMenuItem;
        private System.Windows.Forms.Panel panelContenedor;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            productosMenuItem = new ToolStripMenuItem();
            ventasMenuItem = new ToolStripMenuItem();
            cierreCajaMenuItem = new ToolStripMenuItem();
            presupuestoMenuItem = new ToolStripMenuItem();
            refrescarMenuItem = new ToolStripMenuItem();
            panelContenedor = new Panel();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Font = new Font("Segoe UI", 10F);
            menuStrip1.Items.AddRange(new ToolStripItem[] { productosMenuItem, ventasMenuItem, cierreCajaMenuItem, presupuestoMenuItem, refrescarMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1284, 27);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // productosMenuItem
            // 
            productosMenuItem.Name = "productosMenuItem";
            productosMenuItem.Size = new Size(83, 23);
            productosMenuItem.Text = "Productos";
            productosMenuItem.Click += ProductosMenuItem_Click;
            // 
            // ventasMenuItem
            // 
            ventasMenuItem.Name = "ventasMenuItem";
            ventasMenuItem.Size = new Size(62, 23);
            ventasMenuItem.Text = "Ventas";
            ventasMenuItem.Click += VentasMenuItem_Click;
            // 
            // cierreCajaMenuItem
            // 
            cierreCajaMenuItem.Name = "cierreCajaMenuItem";
            cierreCajaMenuItem.Size = new Size(103, 23);
            cierreCajaMenuItem.Text = "Cierre de caja";
            cierreCajaMenuItem.Click += CierreCajaMenuItem_Click;
            // 
            // presupuestoMenuItem
            // 
            presupuestoMenuItem.Name = "presupuestoMenuItem";
            presupuestoMenuItem.Size = new Size(97, 23);
            presupuestoMenuItem.Text = "Presupuesto";
            presupuestoMenuItem.Click += PresupuestoMenuItem_Click;
            // 
            // refrescarMenuItem
            // 
            refrescarMenuItem.Name = "refrescarMenuItem";
            refrescarMenuItem.Size = new Size(76, 23);
            refrescarMenuItem.Text = "Refrescar";
            refrescarMenuItem.Click += RefrescarMenuItem_Click;
            // 
            // panelContenedor
            // 
            panelContenedor.Dock = DockStyle.Fill;
            panelContenedor.Location = new Point(0, 27);
            panelContenedor.Name = "panelContenedor";
            panelContenedor.Size = new Size(1284, 534);
            panelContenedor.TabIndex = 1;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1284, 561);
            Controls.Add(panelContenedor);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "Sistema de gestión";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
