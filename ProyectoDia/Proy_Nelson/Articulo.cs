using System;
using System.Collections.Generic;

namespace Proy_Nelson
{
	public class Articulo : Publicacion
	{
		private string DOI { get; set; }
		private string titulo { get; set; }
		private string editorial { get; set; }
		private string anoPublicacion { get; set; }
		private string pagInicio { get; set; }
		private string pagFin { get; set; }
		private List<string> autores { get; set; }

		public Articulo(string DOI, string titulo, string editorial, string anoPublicacion, string pagInicio, string pagFin, List<string> autores)
		{
			this.DOI = DOI;
			this.titulo = titulo;
			this.editorial = editorial;
			this.anoPublicacion = anoPublicacion;
			this.pagInicio = pagInicio;
			this.pagFin = pagFin;
			this.autores = autores;
		}
	}
}
