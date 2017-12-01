using System;

namespace Proy_ImaTor
{
    public class Articulo : Publicacion
	{
        public override string Tipo { get { return "Articulo"; } }
		public Articulo(string DOI, string Titulo, string Editorial, int Anho, int PInicial, int PFinal, string[] Autores) 
            : base(DOI, Titulo, Editorial, Anho, PInicial, PFinal, Autores)
		{
		}

		public Articulo(string Titulo, string Editorial, int Anho, int PInicial, int PFinal, string[] Autores)
			: base(Titulo, Editorial, Anho, PInicial, PFinal, Autores)
		{
		}
		public override string ToString()
		{
			return base.ToString() + "]";
		}
	
	}
}
