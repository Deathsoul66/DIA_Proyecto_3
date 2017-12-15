using System;
using System.Collections.Generic;

namespace IntegracionFinal
{
    /***
     * Articulo hereda de Publicacion los atributos.
     */
	public class Articulo : Publicacion
	{
        /***
         * Constructor específico para Artículo
         */
		public Articulo(string DOI, string titulo, string editorial, DateTime fechaPublicacion, string pagInicio, string pagFin, List<string> autores)
		{
			base.DOI = DOI;
            base.Titulo = titulo;
            base.Editorial = editorial;
			base.FechaPublicacion = fechaPublicacion;
			base.PagInicio = pagInicio;
			base.PagFin = pagFin;
			base.Autores = autores;
		}
	}
}
