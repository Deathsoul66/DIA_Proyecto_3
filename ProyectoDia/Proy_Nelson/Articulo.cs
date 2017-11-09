using System;
using System.Collections.Generic;

namespace Proy_Nelson
{
	public class Articulo : Publicacion
	{
		public Articulo(string DOI, string titulo, string editorial, string anoPublicacion, string pagInicio, string pagFin, List<string> autores)
		{
			base.DOI = DOI;
            base.Titulo = titulo;
            base.Editorial = editorial;
			base.FechaPublicacion = anoPublicacion;
			base.PagInicio = pagInicio;
			base.PagFin = pagFin;
			base.Autores = autores;
		}
	}
}
