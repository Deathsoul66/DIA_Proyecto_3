using System;
namespace Proy_ImaTor
{
    public class Congreso: Publicacion
	{
        public override string Tipo { get { return "Congreso"; } }
		public string Nombre { get; set; }
		public string Ciudad { get; set; }
		public DateTime Fecha { get; set; }

		public Congreso(string DOI, string Titulo, string Editorial, int Anho, int PInicial, int PFinal,
                        string Nombre, string Ciudad, DateTime Fecha, string[] Autores) : base(DOI, Titulo, Editorial, Anho,
                                                                                                      PInicial, PFinal, Autores)
		{
			this.Nombre = Nombre;
			this.Ciudad = Ciudad;
            this.Fecha = Fecha.Date;
		}

		public Congreso(string Titulo, string Editorial, int Anho, int PInicial, int PFinal,
						string Nombre, string Ciudad, DateTime Fecha, string[] Autores) : base(Titulo, Editorial, Anho,
																									  PInicial, PFinal, Autores)
		{
			this.Nombre = Nombre;
			this.Ciudad = Ciudad;
            this.Fecha = Fecha.Date;
		}

		public override string ToString()
		{
            string toret = base.ToString() + String.Format(", Nombre: {0}, Ciudad: {1}, Fecha: {2}",
                                         Nombre, Ciudad, Fecha.ToString("d"));
			toret += "]";
			return toret;
		}
	}
}
