using System;
using System.Collections.Generic;

namespace Proy_Nelson
{
	public class Publicacion
	{
		public string DOI { get; set; }
		public string Titulo { get; set; }
		public string Editorial { get; set; }
		public string AnoPublicacion { get; set; }
		public string PagInicio { get; set; }
		public string PagFin { get; set; }

		protected Publicacion(){}

		public static Publicacion Create(TipoPublicacion tipo, string DOI, string titulo, string editorial,
										 string anoPublicacion, string pagInicio, string pagFin, List<string> autores,
										 string nombre = null, string ciudad = null, string fecha = null)
		{
			switch (tipo)
			{
				case TipoPublicacion.Articulo:
					return new Articulo(DOI, titulo, editorial, anoPublicacion, pagInicio, pagFin, autores);
				case TipoPublicacion.Congreso:
					return new Congreso(DOI, titulo, editorial, anoPublicacion, pagInicio, pagFin, autores, nombre, ciudad, fecha);
				case TipoPublicacion.Libro:
					return new Libro(DOI, titulo, editorial, anoPublicacion, pagInicio, pagFin, autores);
				default:
					return null;
			}
		}

	}
}