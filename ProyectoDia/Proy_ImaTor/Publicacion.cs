using System;
using System.Collections.Generic;
using System.Linq;

namespace Proy_ImaTor
{
    public class Publicacion
    {
        public virtual string Tipo{ get { return "Publicacion"; }}
        public string DOI {get; set;}
        public string Titulo{get; set;}
		public string Editorial { get; set; }
		public int AnhoPublicacion { get; set; }
		public int PaginaInicial { get; set; }
		public int PaginaFinal { get; set; }
        private List<string> ListaAutores = new List<string>();
        public List<string> Autores { get { return ListaAutores; } }

        public Publicacion(){}
        public Publicacion(string DOI, string Titulo ,string Editorial, int anhoPublicacion, int PaginaInicial, int PaginaFinal, string[] Autores){
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

		public Publicacion(string Titulo, string Editorial, int anhoPublicacion, int PaginaInicial, int PaginaFinal, string[] Autores)
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
            Random random = new Random();
            string toret = "";
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            toret = new string (Enumerable.Repeat(chars, 2).Select(s => s[random.Next(s.Length)]).ToArray());
            toret += ".";
            toret += new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray());
            toret += "/";
            toret += new string(Enumerable.Repeat(chars, 3).Select(s => s[random.Next(s.Length)]).ToArray());
            return toret;
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
