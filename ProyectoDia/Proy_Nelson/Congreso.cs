using System;
using System.Collections.Generic;

namespace Proy_Nelson
{
	public class Congreso : Publicacion
	{
		private string DOI { get; set; }
		private string titulo { get; set; }
		private string editorial { get; set; }
		private string anoPublicacion { get; set; }
		private string pagInicio { get; set; }
		private string pagFin { get; set; }
		private string nombre { get; set; }
		private string ciudad { get; set; }
		private string fecha { get; set; }
		private List<string> autores { get; set; }

		public Congreso(string DOI, string titulo, string editorial, string anoPublicacion, string pagInicio, string pagFin, List<string> autores, string nombre, string ciudad, string fecha)
		{
			this.DOI = DOI;
			this.titulo = titulo;
			this.editorial = editorial;
			this.anoPublicacion = anoPublicacion;
			this.pagInicio = pagInicio;
			this.pagFin = pagFin;
			this.autores = autores;
			this.nombre = nombre;
			this.ciudad = ciudad;
			this.fecha = fecha;
		}
	}
}
