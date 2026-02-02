using MaterialesEnSeco.Forms;
using System.Windows.Forms;

namespace MaterialesEnSeco
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            // ✅ Escalado automático según DPI del monitor
            this.AutoScaleMode = AutoScaleMode.Dpi;

            InitializeComponent();

            // ✅ Configurar anclaje/dock de controles importantes
            panelContenedor.Dock = DockStyle.Fill;

            // ✅ Ajustar MenuStrip para escalar correctamente
            AjustarMenuStrip(menuStrip1);
        }

        // Método para ajustar el MenuStrip según DPI
        private void AjustarMenuStrip(MenuStrip menu)
        {
            if (menu == null)
                return;

            menu.AutoSize = true;
            menu.Padding = new Padding(5, 2, 5, 2);

            // Escalar fuente según DPI
            using (Graphics g = menu.CreateGraphics())
            {
                float dpi = g.DpiX;
                float scaleFactor = dpi / 96f; // 96 DPI estándar
                menu.Font = new Font(menu.Font.FontFamily, menu.Font.Size * scaleFactor, menu.Font.Style);
            }

            // Opcional: si los items son muchos, se puede habilitar el overflow
            menu.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
        }

        // Método reutilizable para abrir forms dentro del panel
        private void AbrirFormEnPanel(Form formHijo)
        {
            // limpiar lo que haya antes
            panelContenedor.Controls.Clear();

            formHijo.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None;
            formHijo.Dock = DockStyle.Fill;

            panelContenedor.Controls.Add(formHijo);
            formHijo.Show();
        }

        private void ProductosMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new ProductosForm());
        }

        private void VentasMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new VentasForm());
        }

        private void CierreCajaMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new CierreCajaForm());
        }

        private void PresupuestoMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new PresupuestoForm());
        }

        private void RefrescarMenuItem_Click(object sender, EventArgs e)
        {
            if (panelContenedor.Controls.Count == 0)
            {
                MessageBox.Show("No hay ningún formulario abierto para refrescar.",
                                "Información",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            Form formActual = panelContenedor.Controls[0] as Form;

            if (formActual == null)
                return;

            // Intentamos llamar a un método RefreshData() si existe en ese form
            var metodo = formActual.GetType().GetMethod("RefreshData");
            if (metodo != null)
            {
                metodo.Invoke(formActual, null);
                MessageBox.Show("Información refrescada",
                                "Refrescar",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("El formulario actual no tiene soporte de refresco.",
                                "Aviso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }
    }
}
