using Gtk;

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
			//VERTICAL BOXs>
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

			//AÑADIR A BOX VERTICAL (MainView)
			vbox.PackStart(barraMenu, false, false, 0); //MENU

			//VISUALIZAR
			// - AÑADIR PARA MOSTRAR -
			Add(vbox);

			//MOSTRAR TODOS
			ShowAll(); //Muestra todos en la ventana

		}
		void OnDelete(object obj, DeleteEventArgs args) { Application.Quit(); }
	}
}