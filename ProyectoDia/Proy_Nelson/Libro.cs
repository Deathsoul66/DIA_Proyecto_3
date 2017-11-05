using System;
namespace Proy_Nelson
{
	public class Libro : Publicacion
	{
		private string DOI { get; set; }
		private string titulo { get; set; }
		private string editorial { get; set; }
		private string anoPublicacion { get; set; }
		private string pagInicio { get; set; }
		private string pagFin { get; set; }
		private string[] autores { get; set; }

		public Libro(string DOI, string titulo, string editorial, string anoPublicacion, string pagInicio, string pagFin, string[] autores)
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