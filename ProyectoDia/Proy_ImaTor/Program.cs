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

            Publicacion p1 = new Articulo("12.2SN2/NEO","asd", "Victor", 172, 0, 12, "wedtgds", "gsdgds", "543dsfsd");
            /*Publicacion p2 = new Publicacion("Concha su madre", "Victor", 172, 0, 12, "wedtgds", "gsdgds", "543dsfsd");
            Publicacion p3 = new Publicacion("Concha su madre", "Victor", 172, 0, 12, "wedtgds", "gsdgds", "543dsfsd");
            Publicacion p4 = new Publicacion("Concha su madre", "Victor", 172, 0, 12, "wedtgds", "gsdgds", "543dsfsd");
            Publicacion p5 = new Publicacion("Concha su madre", "Victor", 172, 0, 12, "wedtgds", "gsdgds", "543dsfsd");*/

			ps.Add(new Libro("ssss", "aaaaa", 1, 2, 3, "pepe", "efdsdbf"));
			ps.Add(new Articulo("12.2SN2/NEL", "ssss", "aaaaa", 1, 2, 3, "pepe", "efdsdbf"));
			ps.Add(new Congreso("ssss", "aaaaa", 1, 2, 3, "Nombre", "Ciudad", DateTime.Now, "pepe", "efdsdbf"));
            ps.Add(new Articulo("12.2SN2/NEL", "ssss", "aaaaa", 1, 2, 3, "pepe", "efdsdbf"));
            ps.Add(p1);
            ListaPublicaciones lista = new ListaPublicaciones();
            ps.ForEach(p => Console.WriteLine(string.Format("{0}",(lista.Add(p) ? "correcto" : "IMcorrecto"))));

            //Console.WriteLine(lista);

            lista.CrearXML("/home/yco/Publicaciones.xml");
            lista.CargarXML("/home/yco/Publicaciones.xml");


            /*Preguntar Refencia valor
            p1 = null;
            p1 = new Articulo("12.2SN2/NEO", "ddddd", "Victor", 172, 0, 12, "wedtgds", "gsdgds", "543dsfsd");

            Console.WriteLine(lista);

            Console.WriteLine(lista.Modificar(p1));
            Console.WriteLine(lista);*/
		}
	}
}
