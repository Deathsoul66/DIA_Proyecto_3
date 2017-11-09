using System;
using System.Collections.Generic;

namespace Proy_Nelson
{
	public class Libro : Publicacion
	{
		private List<string> autores { get; set; }

		public Libro(string DOI, string titulo, string editorial, string anoPublicacion, string pagInicio, string pagFin, List<string> autores)
		{
			base.DOI = DOI;
			base.Titulo = titulo;
			base.Editorial = editorial;
			base.AnoPublicacion = anoPublicacion;
			base.PagInicio = pagInicio;
			base.PagFin = pagFin;
			this.autores = autores;
		}

		public List<string> getAutores()
		{
			return autores;
		}
	}
}