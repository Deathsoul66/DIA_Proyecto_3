using System;
using System.Collections.Generic;

namespace Proy_Nelson
{
	public class Congreso : Publicacion
	{
		public string nombre { get; set; }
		public string ciudad { get; set; }
		public string fecha { get; set; }
		private List<string> autores { get; set; }

		public Congreso(string DOI, string titulo, string editorial, string anoPublicacion, string pagInicio, string pagFin, List<string> autores, string nombre, string ciudad, string fecha)
		{
			base.DOI = DOI;
			base.Titulo = titulo;
			base.Editorial = editorial;
			base.AnoPublicacion = anoPublicacion;
			base.PagInicio = pagInicio;
			base.PagFin = pagFin;
			this.autores = autores;
			this.nombre = nombre;
			this.ciudad = ciudad;
			this.fecha = fecha;
		}

		public List<string> getAutores()
		{
			return autores;
		}

	}
}