using System;
using Gtk;

namespace Proy_Marco
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Testing");

			Application.Init();

			controllerFiltroPublicacion control = new controllerFiltroPublicacion();
			control.generarVistaTablaBuscador();

			Application.Run();
			Application.Quit();

			var test = Console.Read();
		}
	}
}
