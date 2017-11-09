using System;
using System.Collections.Generic;

namespace Proy_Nelson
{
	public class Congreso : Publicacion
	{
		public string nombre { get; set; }
		public string ciudad { get; set; }
		public string fecha { get; set; }

		public Congreso(string DOI, string titulo, string editorial, string anoPublicacion, string pagInicio, string pagFin, List<string> autores, string nombre, string ciudad, string fecha)
		{
			base.DOI = DOI;
			base.Titulo = titulo;
			base.Editorial = editorial;
			base.FechaPublicacion = anoPublicacion;
			base.PagInicio = pagInicio;
			base.PagFin = pagFin;
			base.Autores = autores;
			this.nombre = nombre;
			this.ciudad = ciudad;
			this.fecha = fecha;
		}
	}
}