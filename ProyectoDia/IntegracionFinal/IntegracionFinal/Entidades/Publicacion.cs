using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegracionFinal
{
	public class Publicacion
	{
		public string DOI { get; set; }
		public string Titulo { get; set; }
		public string Editorial { get; set; }
		public DateTime FechaPublicacion { get; set; }
		public string PagInicio { get; set; }
		public string PagFin { get; set; }
		public List<string> Autores { get; set; }

		public Publicacion() { }

		public static Publicacion Create(TipoPublicacion tipo, string DOI, string titulo, string editorial,
		                                 DateTime fechaPublicacion, string pagInicio, string pagFin, List<string> autores,
										 string nombre = null, string ciudad = null, string fecha = null)
		{
            if (DOI == null || DOI == "") { DOI = generarDOI(); }
			switch (tipo)
			{
				case TipoPublicacion.Articulo:
					return new Articulo(DOI, titulo, editorial, fechaPublicacion, pagInicio, pagFin, autores);
				case TipoPublicacion.Congreso:
					return new Congreso(DOI, titulo, editorial, fechaPublicacion, pagInicio, pagFin, autores, nombre, ciudad, fecha);
				case TipoPublicacion.Libro:
					return new Libro(DOI, titulo, editorial, fechaPublicacion, pagInicio, pagFin, autores);
				default:
					return null;
			}
		}

		public string getTipo()
		{
			if (this is Congreso) { return "Congreso"; }
			else if (this is Libro) { return "Libro"; }
			else if (this is Articulo) { return "Articulo"; }
			else return "Publicacion";
		}

		public string autoresToString()
		{
			string toRet = "";
			for (int i = 0; i < Autores.Count; i++)
			{
				if (i == 0)
				{
					toRet += Autores[i];
				}
				else
				{
					toRet += ",\n" + Autores[i];
				}
			}
			return toRet;
		}

		public string getNombre()
		{
			if (this is Congreso) { return ((Congreso)this).nombre; }
			else return "";
		}

		public string getCiudad()
		{
			if (this is Congreso) { return ((Congreso)this).ciudad; }
			else return "";
		}

		public string getFecha()
		{
			if (this is Congreso) { return ((Congreso)this).fecha; }
			else return "";
		}

        private static string generarDOI()
        {
            Random random = new Random();
            string toret = "";
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            toret = new string(Enumerable.Repeat(chars, 2).Select(s => s[random.Next(s.Length)]).ToArray());
            toret += ".";
            toret += new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray());
            toret += "/";
            toret += new string(Enumerable.Repeat(chars, 3).Select(s => s[random.Next(s.Length)]).ToArray());
            return toret;
        }

    }
}