using System;
using System.Collections.Generic;

namespace Proy_Marco
{
	public class Publicacion
	{

		public String Tipo
		{
			get; set;
		}

		public String Id
		{
			get; set;
		}

		public String Titulo
		{
			get; set;
		}

		public String Editorial
		{
			get; set;
		}

		public String AnhoPublicacion
		{
			get; set;
		}

		public String PaginaIni
		{
			get; set;
		}

		public String PaginaFin
		{
			get; set;
		}

		public List<string> Autores
		{
			get; set;
		}

		public String Nombre
		{
			get; set;
		}

		public String Ciudad
		{
			get; set;
		}

		public String Fecha
		{
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

		public override string ToString()
		{
			string autFormat = "";
			for (int i = 0; i < Autores.Count; i++)
			{
				if (i != Autores.Count - 1)
				{
					autFormat += Autores[i] + ", ";
				}
				else { 
					autFormat += Autores[i]; 
				}
			}

			return string.Format("[Publicacion: Tipo={0}, Id={1}, Titulo={2}, Editorial={3}, AnhoPublicacion={4}, PaginaIni={5}, PaginaFin={6}, Autores={7}, Nombre={8}, Ciudad={9}, Fecha={10}]", Tipo, Id, Titulo, Editorial, AnhoPublicacion, PaginaIni, PaginaFin, autFormat, Nombre, Ciudad, Fecha);
		}

	}
}