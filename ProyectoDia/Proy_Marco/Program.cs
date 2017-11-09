using System;

namespace Proy_Marco
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Testing");

			Gtk.Application.Init();

			controllerFiltroPublicacion control = new controllerFiltroPublicacion();
			control.generarVistaFormularioFiltro();

			Gtk.Application.Run();

			Gtk.Application.Quit();

			var test = Console.Read();

		}
	}
}
