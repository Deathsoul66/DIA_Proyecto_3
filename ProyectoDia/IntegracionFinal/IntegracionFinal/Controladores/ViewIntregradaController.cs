using System;
using System.Collections.Generic;
using Gtk;

namespace IntegracionFinal
{
    public partial class ViewIntegrada
    {
        static string MIEMBROS = "TestMiembros.xml";
        static string PUBLICACIONES = "Test.xml";


        void OnDelete(object obj, DeleteEventArgs args) { Application.Quit(); }

        void OnMenuSalirActivated(object sender, EventArgs e)
        {
            this.Destroy();
            Application.Quit();
        }

        void OnMenuAboutActivated(object sender, EventArgs e)
        {
            AboutDialog about = new AboutDialog();
            about.SetPosition(WindowPosition.Center);
            about.SetIconFromFile(".\\imagenes\\icon.png");
            about.ProgramName = "Integracion";
            about.Version = "0.1.1";
            about.Copyright = "(c) Universidade de Vigo";
            about.Comments = @"Integracion is a simple solution proyect for DIA @ ESEI";
            about.Website = "integracion.fake.web";
            try
            {
                //DRM FREE image found @ https://pixabay.com/es/portapapeles-de-papel-clip-negocio-2899533/
                about.Logo = new Gdk.Pixbuf(".\\imagenes\\about.png", 200, 300);
            }
            catch
            {
                throw new Exception("Imagen 'about.png' no encontrada");
            }
            about.Run();
            about.Destroy();
        }

        //void OnMenuTestActivated(object sender, EventArgs e) { }
        //void OnMenuInfMerMiembroMensualActivated(object o, ButtonPressEventArgs args) { }
        //void onBtnConsultarMerMiembroMesesClicked(object sender, EventArgs e) { }
        //void onInfAnualDepActivated(object o, ButtonPressEventArgs args) { }
        //void onBtnConsultMerDepClicked(object sender, EventArgs e) { }
        //void oninfAnualMerActivated(object o, ButtonPressEventArgs args) { }
        //void btnConsultarMerAnioClicked(object sender, EventArgs e) { }
    }
}
