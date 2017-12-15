using System;
using System.Collections.Generic;

namespace IntegracionFinal
{
    /***
     * Congreso hereda de Publicacion los atributos comunes y especifica los específicos para los congresos.
     */
    public class Congreso : Publicacion
	{
		public string nombre { get; set; }
		public string ciudad { get; set; }
		public string fecha { get; set; }

        /***
         * Constructor específico para Congreso
         */
		public Congreso(string DOI, string titulo, string editorial, DateTime fechaPublicacion, string pagInicio, string pagFin, List<string> autores, string nombre, string ciudad, string fecha)
		{
			base.DOI = DOI;
			base.Titulo = titulo;
			base.Editorial = editorial;
			base.FechaPublicacion = fechaPublicacion;
			base.PagInicio = pagInicio;
			base.PagFin = pagFin;
			base.Autores = autores;
			this.nombre = nombre;
			this.ciudad = ciudad;
			this.fecha = fecha;
		}
	}
}