using System;
using System.Collections.Generic;

namespace Proy_Marco
{
	public class listaPublicacion
	{
		public List<Publicacion> listPub {
			get; set;
		}

		public listaPublicacion()
		{
			listPub = new List<Publicacion>();
		}

		public void addPublicacion(Publicacion pub) 
		{

			this.listPub.Add(pub);
		}

		public Publicacion getPublicacion(int i) 
		{
			return this.listPub[i];
		}

		public int getLength() 
		{
			return this.listPub.Count;
		}

	}
}