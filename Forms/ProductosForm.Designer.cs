namespace MaterialesEnSeco.Forms
{
    partial class ProductosForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitulo;
        private TextBox txtNombreCategoria;
        private Button btnAgregarCategoria;
        private Label lblCategorias;
        private Button btnExportarCategorías;
        private DataGridView dgvCategorias;
        private Label lblSeparador;
        private Label lblTituloProductos;
        private Label lblNombreProducto;
        private TextBox txtNombreProducto;
        private Label lblCategoriaProducto;
        private ComboBox cmbCategoria;
        private Label lblPrecio;
        private TextBox txtPrecio;
        private Label lblStock;
        private TextBox txtStock;
        private Button btnAgregarProducto;
        private Label lblProductos;
        private Label lblFiltrar;
        private ComboBox cmbFiltroCategoria;
        private Button btnExportarProductos;
        private DataGridView dgvProductos;
        private Label lblSeparadorDos;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            txtNombreCategoria = new TextBox();
            btnAgregarCategoria = new Button();
            lblCategorias = new Label();
            btnExportarCategorías = new Button();
            dgvCategorias = new DataGridView();
            lblTituloProductos = new Label();
            lblNombreProducto = new Label();
            txtNombreProducto = new TextBox();
            lblCategoriaProducto = new Label();
            cmbCategoria = new ComboBox();
            lblPrecio = new Label();
            txtPrecio = new TextBox();
            lblStock = new Label();
            txtStock = new TextBox();
            btnAgregarProducto = new Button();
            lblProductos = new Label();
            lblFiltrar = new Label();
            cmbFiltroCategoria = new ComboBox();
            lblSeparador = new Label();
            btnExportarProductos = new Button();
            dgvProductos = new DataGridView();
            lblSeparadorDos = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvCategorias).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            SuspendLayout();

            // lblTitulo
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Agregar Categoría";
            lblTitulo.AutoSize = true;

            // txtNombreCategoria
            txtNombreCategoria.Name = "txtNombreCategoria";
            txtNombreCategoria.PlaceholderText = "Nombre";
            txtNombreCategoria.Size = new Size(250, 23);
            txtNombreCategoria.TabIndex = 2;
            txtNombreCategoria.Font = new Font("Segoe UI", 12.5F);

            // btnAgregarCategoria
            btnAgregarCategoria.Name = "btnAgregarCategoria";
            btnAgregarCategoria.TabIndex = 3;
            btnAgregarCategoria.Text = "Agregar categoría";
            btnAgregarCategoria.AutoSize = true;
            btnAgregarCategoria.Font = new Font("Segoe UI", 12.5F);
            btnAgregarCategoria.TextAlign = ContentAlignment.MiddleCenter;
            btnAgregarCategoria.Padding = new Padding(0, 0, 0, 4);

            // lblCategorias
            lblCategorias.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblCategorias.Name = "lblCategorias";
            lblCategorias.TabIndex = 4;
            lblCategorias.Text = "Categorías";
            lblCategorias.AutoSize = true;

            // btnExportarCategorías
            btnExportarCategorías.ImageAlign = ContentAlignment.MiddleLeft;
            btnExportarCategorías.Name = "btnExportarCategorías";
            btnExportarCategorías.TabIndex = 4;
            btnExportarCategorías.Text = "Exportar";
            btnExportarCategorías.UseVisualStyleBackColor = true;
            btnExportarCategorías.AutoSize = true;
            btnExportarCategorías.Font = new Font("Segoe UI", 12.5F);
            btnExportarCategorías.TextAlign = ContentAlignment.MiddleCenter;
            btnExportarCategorías.Padding = new Padding(0, 0, 0, 4);

            // dgvCategorias
            dgvCategorias.AllowUserToAddRows = false;
            dgvCategorias.AllowUserToDeleteRows = false;
            dgvCategorias.AllowUserToResizeColumns = false;
            dgvCategorias.AllowUserToResizeRows = false;
            dgvCategorias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCategorias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvCategorias.MultiSelect = false;
            dgvCategorias.Name = "dgvCategorias";
            dgvCategorias.RowHeadersVisible = false;
            dgvCategorias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategorias.RowTemplate.Resizable = DataGridViewTriState.False;
            dgvCategorias.TabIndex = 5;
            dgvCategorias.ReadOnly = true;
            dgvCategorias.ColumnHeadersHeight = 27;
            dgvCategorias.RowTemplate.Height = 27;
            dgvCategorias.Font = new Font("Segoe UI", 10F);

            // lblTituloProductos
            lblTituloProductos.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTituloProductos.Name = "lblTituloProductos";
            lblTituloProductos.TabIndex = 7;
            lblTituloProductos.Text = "Agregar Producto";
            lblTituloProductos.AutoSize = true;

            // lblNombreProducto
            lblNombreProducto.Name = "lblNombreProducto";
            lblNombreProducto.TabIndex = 9;
            lblNombreProducto.Text = "Nombre:";
            lblNombreProducto.AutoSize = true;
            lblNombreProducto.Font = new Font("Segoe UI", 12.5F);

            // txtNombreProducto
            txtNombreProducto.Name = "txtNombreProducto";
            txtNombreProducto.Size = new Size(200, 23);
            txtNombreProducto.TabIndex = 10;
            txtNombreProducto.Font = new Font("Segoe UI", 12.5F);

            // lblCategoriaProducto
            lblCategoriaProducto.Name = "lblCategoriaProducto";
            lblCategoriaProducto.TabIndex = 11;
            lblCategoriaProducto.Text = "Categoría:";
            lblCategoriaProducto.AutoSize = true;
            lblCategoriaProducto.Font = new Font("Segoe UI", 12.5F);

            // cmbCategoria
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(200, 23);
            cmbCategoria.TabIndex = 12;
            cmbCategoria.Font = new Font("Segoe UI", 12.5F);

            // lblPrecio
            lblPrecio.Name = "lblPrecio";
            lblPrecio.TabIndex = 13;
            lblPrecio.Text = "Precio:";
            lblPrecio.AutoSize = true;
            lblPrecio.Font = new Font("Segoe UI", 12.5F);

            // txtPrecio
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(200, 23);
            txtPrecio.TabIndex = 14;
            txtPrecio.Font = new Font("Segoe UI", 12.5F);

            // lblStock
            lblStock.Name = "lblStock";
            lblStock.TabIndex = 15;
            lblStock.Text = "Stock:";
            lblStock.AutoSize = true;
            lblStock.Font = new Font("Segoe UI", 12.5F);

            // txtStock
            txtStock.Name = "txtStock";
            txtStock.Size = new Size(200, 23);
            txtStock.TabIndex = 16;
            txtStock.Font = new Font("Segoe UI", 12.5F);

            // btnAgregarProducto
            btnAgregarProducto.Name = "btnAgregarProducto";
            btnAgregarProducto.TabIndex = 17;
            btnAgregarProducto.Text = "Agregar producto";
            btnAgregarProducto.AutoSize = true;
            btnAgregarProducto.Font = new Font("Segoe UI", 12.5F);
            btnAgregarProducto.TextAlign = ContentAlignment.MiddleCenter;
            btnAgregarProducto.Padding = new Padding(0, 0, 0, 4);

            // lblProductos
            lblProductos.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblProductos.Name = "lblProductos";
            lblProductos.TabIndex = 18;
            lblProductos.Text = "Productos";
            lblProductos.AutoSize = true;

            // lblFiltrar
            lblFiltrar.Name = "lblFiltrar";
            lblFiltrar.TabIndex = 20;
            lblFiltrar.Text = "Filtrar por categoría:";
            lblFiltrar.AutoSize = true;
            lblFiltrar.Font = new Font("Segoe UI", 12.5F);

            // cmbFiltroCategoria
            cmbFiltroCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroCategoria.Name = "cmbFiltroCategoria";
            cmbFiltroCategoria.Size = new Size(150, 23);
            cmbFiltroCategoria.TabIndex = 21;
            cmbFiltroCategoria.Font = new Font("Segoe UI", 12.5F);

            // lblSeparador
            lblSeparador.BorderStyle = BorderStyle.Fixed3D;
            lblSeparador.Name = "lblSeparador";
            lblSeparador.Size = new Size(750, 2);
            lblSeparador.TabIndex = 6;

            // btnExportarProductos
            btnExportarProductos.ImageAlign = ContentAlignment.MiddleLeft;
            btnExportarProductos.Name = "btnExportarProductos";
            btnExportarProductos.TabIndex = 4;
            btnExportarProductos.Text = "Exportar";
            btnExportarProductos.UseVisualStyleBackColor = true;
            btnExportarProductos.AutoSize = true;
            btnExportarProductos.Font = new Font("Segoe UI", 12.5F);
            btnExportarProductos.TextAlign = ContentAlignment.MiddleCenter;
            btnExportarProductos.Padding = new Padding(0, 0, 0, 4);

            // dgvProductos
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.AllowUserToResizeColumns = false;
            dgvProductos.AllowUserToResizeRows = false;
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvProductos.MultiSelect = false;
            dgvProductos.Name = "dgvProductos";
            dgvProductos.RowHeadersVisible = false;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.RowTemplate.Resizable = DataGridViewTriState.False;
            dgvProductos.TabIndex = 19;
            dgvProductos.ReadOnly = true;
            dgvProductos.ColumnHeadersHeight = 27;
            dgvProductos.RowTemplate.Height = 27;
            dgvProductos.Font = new Font("Segoe UI", 10F);

            // lblSeparadorDos
            lblSeparadorDos.BorderStyle = BorderStyle.None;
            lblSeparadorDos.Name = "lblSeparadorDos";
            lblSeparadorDos.Size = new Size(750, 2);
            lblSeparadorDos.TabIndex = 6;

            // ProductosForm
            AutoScroll = true;
            ClientSize = new Size(914, 508);
            Controls.Add(lblTitulo);
            Controls.Add(txtNombreCategoria);
            Controls.Add(btnAgregarCategoria);
            Controls.Add(lblCategorias);
            Controls.Add(btnExportarCategorías);
            Controls.Add(dgvCategorias);
            Controls.Add(lblSeparador);
            Controls.Add(lblTituloProductos);
            Controls.Add(lblNombreProducto);
            Controls.Add(txtNombreProducto);
            Controls.Add(lblCategoriaProducto);
            Controls.Add(cmbCategoria);
            Controls.Add(lblPrecio);
            Controls.Add(txtPrecio);
            Controls.Add(lblStock);
            Controls.Add(txtStock);
            Controls.Add(btnAgregarProducto);
            Controls.Add(lblProductos);
            Controls.Add(lblFiltrar);
            Controls.Add(cmbFiltroCategoria);
            Controls.Add(btnExportarProductos);
            Controls.Add(dgvProductos);
            Controls.Add(lblSeparadorDos);
            Name = "ProductosForm";
            Text = "Productos";
            ((System.ComponentModel.ISupportInitialize)dgvCategorias).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
