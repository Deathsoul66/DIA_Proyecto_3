using System;
using System.Collections.Generic;

namespace Proy_Marco
{
	public class Publicacion
	{

		public String Tipo {
			get; set;
		}

		public String Id {
			get; set;
		}

		public String Titulo{
			get; set;
		}

		public String Editorial {
			get; set;
		}

		public String AnhoPublicacion {
			get; set;
		}

		public String PaginaIni {
			get; set;
		}

		public String PaginaFin {
			get; set;
		}

		public List<string> Autores {
			get; set;
		}

		public String Nombre {
			get; set;
		}

		public String Ciudad {
			get; set;
		}

		public String Fecha {
			get; set;
		}

		public Publicacion(String tipo = "", String id = "", String titulo = "", String editorial = "", String anho = "", String ini = "", String fin = "", List<String> autores = null, String nombre = "", String ciudad = "", String fecha = "")
		{
			this.Tipo = tipo;
			this.Id = id;
			this.Titulo = titulo;
			this.Editorial = editorial;
			this.AnhoPublicacion = anho;
			this.PaginaIni = ini;
			this.PaginaFin = fin;
			this.Autores = autores;
			this.Nombre = nombre;
			this.Ciudad = ciudad;
			this.Fecha = fecha;
		}

	}
}