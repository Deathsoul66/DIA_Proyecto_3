using System;
using System.Collections.Generic;
namespace Proy_ImaTor
{
    public class Publicacion
    {
        string DOI {get; set;}
        string Titulo{get; set;}
		string Editorial { get; set; }
		int AnhoPublicacion { get; set; }
		int PaginaInicial { get; set; }
		int PaginaFinal { get; set; }
        List<string> Autores { get; set; }
		

        public Publicacion(string DOI, string Titulo ,string Editorial, int anhoPublicacion, int PaginaInicial, int PaginaFinal, params string[] Autores){
            this.DOI = DOI;
            this.Titulo = Titulo;
            this.Editorial = Editorial;
            this.AnhoPublicacion = anhoPublicacion;
            this.PaginaInicial = PaginaInicial;
            this.PaginaFinal = PaginaFinal;
            foreach(string autor in Autores){
                this.Autores.Add(autor);
            }
        }

		public Publicacion(string Titulo, string Editorial, int anhoPublicacion, int PaginaInicial, int PaginaFinal, params string[] Autores)
		{
			this.DOI = GenerarDOI();
			this.Titulo = Titulo;
			this.Editorial = Editorial;
			this.AnhoPublicacion = anhoPublicacion;
			this.PaginaInicial = PaginaInicial;
			this.PaginaFinal = PaginaFinal;
			foreach (string autor in Autores)
			{
				this.Autores.Add(autor);
			}
		}

        private string GenerarDOI(){
            return null;
        }
    }
}
