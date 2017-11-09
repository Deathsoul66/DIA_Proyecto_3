using System;
using System.Collections.Generic;

namespace Proy_Marco
{
	public class controllerFiltroPublicacion
	{
		public controllerFiltroPublicacion() {
		
		}

		public void generarVistaFormularioFiltro() 
		{
			vistaFiltroPublicacionBuscador buscador = new vistaFiltroPublicacionBuscador();
			buscador.ShowAll();
		}

		public void generarVistaTablaFiltrada(String nombre = "", String anho = "" , String tipo = "")
		{
			listaPublicacion devueltoLista = XMLreader.devolverXML();
			devueltoLista = XMLreader.filtrarLista(devueltoLista, nombre, anho, tipo);

			vistaFiltroPublicacionTabla tabla = new vistaFiltroPublicacionTabla(devueltoLista);
			tabla.ShowAll();
		}

		public void decidirVista(int vista, List<String> datos) 
		{
			if (vista == 1)
			{
				generarVistaFormularioFiltro();
			}
			else
			{
				generarVistaTablaFiltrada();
			}	

		}

	}
}

