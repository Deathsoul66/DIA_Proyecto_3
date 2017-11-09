using System;
using System.Collections.Generic;
using System.Linq;
namespace Proy_ImaTor
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");


            List<Publicacion> ps = new List<Publicacion>();

            ps.Add(new Libro("11111", "ssss", "aaaaa", 1, 2, 3, "pepe", "efdsdbf"));
            ps.Add(new Articulo("11111", "ssss", "aaaaa", 1, 2, 3, "pepe", "efdsdbf"));
            ps.Add(new Congreso("11111", "ssss", "aaaaa", 1, 2, 3, "Nombre", "Ciudad", DateTime.Now,"pepe", "efdsdbf"));

            ps.ForEach(p => Console.WriteLine(p));
		}
	}
}
