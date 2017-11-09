using System;
using System.Collections.Generic;
using System.Linq;

namespace Proy_ImaTor
{
    public class Publicacion
    {
        public virtual string Tipo{ get { return "Publicacion"; }}
        string DOI {get; set;}
        string Titulo{get; set;}
		string Editorial { get; set; }
		int AnhoPublicacion { get; set; }
		int PaginaInicial { get; set; }
		int PaginaFinal { get; set; }
        private List<string> ListaAutores = new List<string>();
        List<string> Autores { get { return ListaAutores; } }
		

        public Publicacion(string DOI, string Titulo ,string Editorial, int anhoPublicacion, int PaginaInicial, int PaginaFinal, params string[] Autores){
            this.DOI = DOI;
            this.Titulo = Titulo;
            this.Editorial = Editorial;
            this.AnhoPublicacion = anhoPublicacion;
            this.PaginaInicial = PaginaInicial;
            this.PaginaFinal = PaginaFinal;
            foreach( string autor in Autores){
                this.ListaAutores.Add(autor);
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
				this.ListaAutores.Add(autor);
			}
		}

        private string GenerarDOI(){
            return null;
        }

        public override string ToString()
        {
            string toret = string.Format("{0}: [DOI: {1}, Titulo: {2}, Editorial: {3}, AnhoPublicacion: {4}, PaginaInicial: {5}, PaginaFinal: {6}",
                                         Tipo,DOI,Titulo, Editorial,AnhoPublicacion,PaginaInicial,PaginaFinal);
            Autores.ForEach(autor => toret += ", Autor: " + autor);
            return toret;
        }
    }
}
