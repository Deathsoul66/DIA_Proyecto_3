using System;
namespace Proy_ImaTor
{
    public class Libro: Publicacion
    {

        public override string Tipo { get { return "Libro"; } }

        public Libro(string DOI, string Titulo, string Editorial, int Anho, int PInicial, int PFinal, params string[] Autores)
			: base(DOI, Titulo, Editorial, Anho, PInicial, PFinal, Autores)
        {
        }

		public Libro(string Titulo, string Editorial, int Anho, int PInicial, int PFinal, params string[] Autores)
			: base(Titulo, Editorial, Anho, PInicial, PFinal, Autores)
        {
        }

        public override string ToString()
        {
            return base.ToString() + "]";
        }

    }
}
