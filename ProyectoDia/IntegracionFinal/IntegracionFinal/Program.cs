using System;
using Gtk;

namespace IntegracionFinal
{
	class MainClass
	{
		public static void Main(string[] args)
		{
            ////Nelson
            //Application.Init();
            //ViewInformes Grafi = new ViewInformes();
            //Application.Run();

            ////Marco
            //Application.Init();
            //MainWindowViewFiltroPublicaciones filtro = new MainWindowViewFiltroPublicaciones();
            //filtro.ShowAll();
            //Application.Run();
            ////Application.Quit();

            ////PANCHI
            //Gtk.Application.Init();
            //var win = new MainWindow();
            //win.ShowAll();
            //Gtk.Application.Run();

            ////Immator
            //Gtk.Application.Init();
            //var win2 = new MainWindowIMT();
            //win2.ShowAll();
            //Gtk.Application.Run();

            ////   TOTAL ANIHILATION VIEW   ////
            Application.Init();
            ViewIntegrada Grafi = new ViewIntegrada();
            Application.Run();

            return;
		}
	}
}
