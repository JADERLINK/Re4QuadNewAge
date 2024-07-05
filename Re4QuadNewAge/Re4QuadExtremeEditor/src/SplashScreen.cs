using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Re4QuadExtremeEditor.src.Forms;

namespace Re4QuadExtremeEditor.src
{
    public static class SplashScreen
    {
        public static SplashScreenConteiner Conteiner { get; set; }
        private static void SplashScreenShow()
        {
            Application.Run(new SplashScreenForm(Conteiner));
        }

        public static void StartSplashScreen()
        {
            Conteiner = new SplashScreenConteiner();
            System.Threading.Thread threadSplashScreen = new System.Threading.Thread(SplashScreenShow);
            threadSplashScreen.SetApartmentState(System.Threading.ApartmentState.STA);
            threadSplashScreen.Start();
        }
    }

    public class SplashScreenConteiner
    {
        public Action Close { get; set; }
        public Action ReleasedToClose { get; set; }
        public bool FormIsClosed { get; set; }

        public SplashScreenConteiner() 
        {
            FormIsClosed = false;
        }
    }
}
