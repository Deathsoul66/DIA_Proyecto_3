using Gtk;
using System;

namespace Proy_Nelson
{
	public class ViewInformes : Window
	{
		public ViewInformes() : base("Informer - DIA Individual Nelson")
		{
			//CONFIG WINDOW
			SetDefaultSize(860, 640);
			SetPosition(WindowPosition.Center);
			//Free DRM icon.png : source @ http://findicons.com/icon/558115/free_bsd#
			SetIconFromFile("icon.png");
			//SYSTEM EVENTS
			DeleteEvent += OnDelete;
			//VERTICAL BOXs
			Box vbox = new VBox(false, 2);
			//ELEMENTS
			// = MENU =
			MenuBar barraMenu = new MenuBar();
			MenuItem infMensual = new MenuItem("Mensual Miembros");
			MenuItem infAnualDep = new MenuItem("Anual Departamento");
			MenuItem infAnualMer = new MenuItem("Anual Meritos");
			MenuItem about = new MenuItem("Acerca de");
			MenuItem salir = new MenuItem("Salir");

			barraMenu.Append(infMensual);
			barraMenu.Append(infAnualDep);
			barraMenu.Append(infAnualMer);
			barraMenu.Append(about);
			barraMenu.Append(salir);

			//ADD TO VERTICAL BOX
			vbox.PackStart(barraMenu, false, false, 0); //MENU

			//ADD TO SHOW
			Add(vbox);

			//SHOW
			ShowAll();

			//EVENTS
			about.ButtonPressEvent += OnMenuAboutActivated;
			salir.ButtonPressEvent += OnMenuSalirActivated;

		}

		void OnDelete(object obj, DeleteEventArgs args) { Application.Quit(); }

		void OnMenuSalirActivated(object sender, EventArgs e)
		{
			this.Destroy();
			Application.Quit();		}

		void OnMenuAboutActivated(object sender, EventArgs e)
		{
			AboutDialog about = new AboutDialog();
			about.SetIconFromFile("icon.png");
			about.ProgramName = "Informer";
			about.Version = "0.0.1";
			about.Copyright = "(c) Nelson Martinez";
			about.Comments = @"Informer is a simple solution proyect for DIA @ ESEI";
			about.Website = "informer.fake.web";
			try
			{
				//DRM FREE image found @ https://pixabay.com/es/portapapeles-de-papel-clip-negocio-2899533/
				about.Logo = new Gdk.Pixbuf("about.png", 200, 300);
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