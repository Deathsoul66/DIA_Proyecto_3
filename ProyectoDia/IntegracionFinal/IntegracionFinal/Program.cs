using System;
using Gtk;

namespace IntegracionFinal
{
	class MainClass
	{
		public static void Main(string[] args)
		{

            Application.Init();
            ViewIntegrada Grafi = new ViewIntegrada();
            Application.Run();

            return;
		}
	}
}
