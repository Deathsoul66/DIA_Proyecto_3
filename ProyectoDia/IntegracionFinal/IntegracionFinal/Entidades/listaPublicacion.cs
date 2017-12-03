using System;
using System.Collections.Generic;

namespace IntegracionFinal
{
	public class ListaPublicacion
	{
		public List<Publicacion> listPub
        {
			get; set;
		}

		public ListaPublicacion()
		{
			listPub = new List<Publicacion>();
		}

        public bool addPublicacion(Publicacion pub)
        {
            try
            {
                Publicacion p = getPublicacionDOI(pub.DOI);
                if (p != null)
                {
                    return false;
                }
                else
                {
                    this.listPub.Add(pub);
                    return true;
                }
            }
            catch {
                return false;
            }
		}

        public bool Eliminar(string DOI)
        {
            Publicacion pub = this.getPublicacionDOI(DOI);
            if (pub != null)
            {
                this.listPub.Remove(pub);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Modificar(Publicacion p, string oldDOI)
        {
            for (int i = 0; i < this.getLength(); i++)
            {
                if (this.getPublicacionIndex(i).DOI == oldDOI)
                {
                    this.listPub[i] = p;
                    return true;
                }
            }
            return false;
        }

        //DUDA CURIOSIDAD
        //¿Mejor mismo nombre o separado? (Sobrecarga de operadores o nombres diferentes para funcionalidad)
		public Publicacion getPublicacionIndex(int i) 
		{
			return this.listPub[i];
		}

        public Publicacion getPublicacionDOI(string DOI)
        {
            foreach (Publicacion p in this.listPub) {

                if (p.DOI == DOI) {
                    return p;
                }
            }
            return null;
        }

		public int getLength() 
		{
			return this.listPub.Count;
		}

		public override string ToString()
		{
			string toRet = "";
			foreach(Publicacion pub in listPub){
				toRet += pub.ToString() + "\n";
			}
			return toRet;
		}
	}
}