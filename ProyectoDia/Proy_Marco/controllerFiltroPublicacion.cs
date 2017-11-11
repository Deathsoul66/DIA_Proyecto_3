using System;
using System.Collections.Generic;

namespace Proy_Marco
{
	public class controllerFiltroPublicacion
	{
		public controllerFiltroPublicacion() {
		
		}

		public void generarVistaTablaBuscador()
		{ 
			vistaFiltroPublicacion tabla = new vistaFiltroPublicacion();
			tabla.ShowAll();
		}

	}
}

