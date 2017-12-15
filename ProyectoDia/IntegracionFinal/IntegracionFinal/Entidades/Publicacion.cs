using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegracionFinal
{
    /***
     * Clase Publicacion
     * Esta clase define los distintos atributos que tendrá una publicación, así como el Tipo de la misma
     * Decidimos utilizar una factoría que mediante herencia
     *
     */
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

        //Factory method encargado de crear publicaciones de cualquier tipo especificado
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

        //Return: Tipo de publicacion
		public string getTipo()
		{
			if (this is Congreso) { return "Congreso"; }
			else if (this is Libro) { return "Libro"; }
			else if (this is Articulo) { return "Articulo"; }
			else return "Publicacion";
		}

        //Return: Lista de autores separados por comas y salto de linea.
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
        //Si es un congreso, devuelve el nombre del congreso
		public string getNombre()
		{
			if (this is Congreso) { return ((Congreso)this).nombre; }
			else return "";
		}
        //Si es un congreso, devuelve la ciudad donde se ha celebrado
		public string getCiudad()
		{
			if (this is Congreso) { return ((Congreso)this).ciudad; }
			else return "";
		}
        //Si es un congreso, devuelve la fecha de celebracion del congreso
		public string getFecha()
		{
			if (this is Congreso) { return ((Congreso)this).fecha; }
			else return "";
		}
        //En caso de que no se haya especificado un DOI para una publicacion se genera uno al azar.
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