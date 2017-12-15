using System;
using System.Collections.Generic;
using Gtk;

namespace IntegracionFinal
{
    public partial class ViewIntegrada
    {
        // GLOBAL
        static string MIEMBROS = "TestMiembros.xml";
        static string PUBLICACIONES = "Test.xml";
        //Accion al destruir la ventana
        void OnDelete(object obj, DeleteEventArgs args) { Application.Quit(); }
        //Accion al hacer clic en el botón "Salir" del menú
        void OnMenuSalirActivated(object sender, EventArgs e)
        {
            this.Destroy();
            Application.Quit();
        }
        //Accion al hacer clic en el botón "Acerca de" del menú
        void OnMenuAboutActivated(object sender, EventArgs e)
        {
            AboutDialog about = new AboutDialog();
            about.SetPosition(WindowPosition.Center);
            about.SetIconFromFile(".\\imagenes\\icon.png");
            about.ProgramName = "Integracion";
            about.Version = "1.0.0";
            about.Copyright = "(c) Universidade de Vigo";
            about.Comments = @"Integracion is a simple solution proyect for DIA @ ESEI";
            about.Website = "integracion.fake.web";
            try
            {
                //DRM FREE image found @ https://pixabay.com/es/portapapeles-de-papel-clip-negocio-2899533/ (Modified with happy pony)
                about.Logo = new Gdk.Pixbuf(".\\imagenes\\about.png", 200, 300);
            }
            catch
            {
                throw new Exception("Imagen 'about.png' no encontrada");
            }
            about.Run();
            about.Destroy();
        }
    }
}
