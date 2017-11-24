using System;
using System.Collections.Generic;
using System.Linq;
namespace Proy_ImaTor
{
    using Proy_ImaTor.View;
    class MainClass
    {
		public static void Main(string[] args)
		{
			Gtk.Application.Init();
			var win = new MainWindow();
			win.ShowAll();
			Gtk.Application.Run();
		}
	}
}
