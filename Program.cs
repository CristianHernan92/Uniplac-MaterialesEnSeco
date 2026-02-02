using System;
using System.Windows.Forms;

namespace MaterialesEnSeco
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // ✅ Habilitar visual styles
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ✅ Escalado DPI correcto (Windows 10+)
            Application.SetHighDpiMode(HighDpiMode.SystemAware);

            AppContext.SetSwitch("MySql.EnableLegacyDateTime", true);

            // Ejecutar el formulario principal
            Application.Run(new MainForm());
        }
    }
}
