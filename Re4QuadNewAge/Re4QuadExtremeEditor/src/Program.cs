using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Re4QuadExtremeEditor
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Configurar manipuladores globais de exceção
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch {}
            // Não tem como capturar a System.ObjectDisposedException
            // Essa Exception é gerada quando o openGL é de uma versão não suportada pelo programa
            // O try catch impede do windows gerar um "CrashDumps"
        }

        // Manipulador para exceções de thread UI
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception, "Error in graphical interface");
        }

        // Manipulador para exceções não tratadas de threads não-UI
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                HandleException(ex, "General error");
            }
        }

        // Método para lidar com exceções
        private static void HandleException(Exception ex, string context)
        {
            MessageBox.Show($"{context}: {ex.Message}\nAn unexpected error occurred, the program may not work correctly from now on.", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
