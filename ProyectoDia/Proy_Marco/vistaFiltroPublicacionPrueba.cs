using System;
using System.Collections.Generic;

namespace Proy_Marco
{
	public class vistaFiltroPublicacionPrueba
	{
		public listaPublicacion listaP {
			get; set;
		}

		public vistaFiltroPublicacionPrueba()
		{

		}

		public void mostrarListaPantalla() {
			Console.WriteLine(listaP.ToString());
		}
	}
}