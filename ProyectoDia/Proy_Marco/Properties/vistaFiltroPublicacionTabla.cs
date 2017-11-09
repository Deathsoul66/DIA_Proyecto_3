using System;
using System.Collections.Generic;

namespace Proy_Marco
{
	public class vistaFiltroPublicacionTabla
	{
		public listaPublicacion listaP
		{
			get; set;
		}

		public vistaFiltroPublicacionTabla()
		{

		}

		public void mostrarListaPantalla()
		{
			Console.WriteLine(listaP.ToString());
		}
	}
}