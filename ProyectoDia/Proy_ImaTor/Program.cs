﻿using System;
using System.Collections.Generic;
using System.Linq;
namespace Proy_ImaTor
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

            //string[] autores = { "pepe", "asfad" };

            List<Publicacion> ps = new List<Publicacion>();

            Publicacion p1 = new Articulo("12.2SN2/NEO","asd", "Victor", 172, 0, 12, new string[] { "pepe", "asfad" });

			ps.Add(new Libro("ssss", "aaaaa", 1, 2, 3, "pepe", "efdsdbf"));
			ps.Add(new Articulo("12.2SN2/NEL", "ssss", "aaaaa", 1, 2, 3, new string[] { "pepe", "asfad" }));
			ps.Add(new Congreso("ssss", "aaaaa", 1, 2, 3, "Nombre", "Ciudad", DateTime.Now, new string[] { "pepe", "asfad" }));
            ps.Add(p1);
            ListaPublicaciones lista = new ListaPublicaciones();
            ps.ForEach(p => Console.WriteLine(string.Format("{0}",(lista.Add(p) ? "correcto" : "IMcorrecto"))));

            Console.WriteLine(lista);

            lista.CrearXML("/home/yco/Publicaciones.xml");

            ListaPublicaciones l2 = new ListaPublicaciones();


            l2.CargarXML("/home/yco/Publicaciones.xml");

            Console.WriteLine(l2);
		}
	}
}
