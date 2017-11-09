using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;

namespace Proy_ImaTor
{
    public class ListaPublicaciones
    {
        private List<Publicacion> listaP = new List<Publicacion>();
        private Dictionary<string, Publicacion> dicionario = new Dictionary<string, Publicacion>();
        public List<Publicacion> Publicaciones { get { return listaP; } }

        public bool Add(Publicacion p)
        {
            if(dicionario.ContainsKey(p.DOI)){
                return false;
            }else{
                dicionario.Add(p.DOI,p);
                return true;
            }
        }

        public bool Eliminar(Publicacion p)
        {
            return dicionario.Remove(p.DOI);
        }

        public bool Modificar(Publicacion p)
        {
            return Add(p);
        }

        public override string ToString()
        {
            string toret = "";

            foreach(Publicacion p in dicionario.Values){
                toret += p + "\n";
            }
            return toret;
        }

        public void CrearXML(string ruta)
        {
            XElement raiz = new XElement("Publicaciones");

            foreach(Publicacion p in dicionario.Values){
                XElement autores = new XElement("Autores");

                foreach(string autor in p.Autores){
                    autores.Add(new XElement("Autor", autor));
                }

                if(p.Tipo.Equals("Congreso")){

                    raiz.Add(new XElement("Publicacion", new XElement(p.Tipo, new XAttribute("DOI", p.DOI)), new XElement("Titulo", p.Titulo), 
                                          new XElement("Editorial", p.Editorial), new XElement("Anho", p.AnhoPublicacion), new XElement("PaginaIni", p.PaginaInicial),
                                          new XElement("PaginaFin", p.PaginaFinal), autores, new XElement("Nombre", ((Congreso)p).Nombre), 
                                          new XElement("Ciudad", ((Congreso)p).Ciudad), new XElement("Fecha", ((Congreso)p).Fecha)));

                }else{
					raiz.Add(new XElement("Publicacion", new XElement(p.Tipo, new XAttribute("DOI", p.DOI)), new XElement("Titulo", p.Titulo),
										  new XElement("Editorial", p.Editorial), new XElement("Anho", p.AnhoPublicacion), new XElement("PaginaIni", p.PaginaInicial),
										  new XElement("PaginaFin", p.PaginaFinal), autores));
                }
            }
            new XDocument(raiz).Save(ruta);
        }

        public void CargarXML(string ruta)
        {
            try{
                XElement raiz = XElement.Load(ruta);

                foreach(XElement publicacion in raiz.Elements("Publicacion")){

                    List<string> autores = new List<string>();
                    foreach (XElement autor in publicacion.Elements("Autores"))
					{
                        autores.Add(autor.Element("Autor").Value);
					}

                    if(publicacion.Element("Congreso") != null){
                        
                        Add(new Congreso(publicacion.Attribute("DOI").Value, publicacion.Element("Titulo").Value,
                                         publicacion.Element("Editorial").Value, Int32.Parse(publicacion.Element("Anho").Value), Int32.Parse(publicacion.Element("PaginaIni").Value),
                                         Int32.Parse(publicacion.Element("PaginaFin").Value), publicacion.Element("Nombre").Value, 
                                         publicacion.Element("Ciudad").Value, publicacion.Element("Fecha").Value, autores.ToArray());
                    }
                }
                    
            }catch{
                Console.WriteLine("Podrian llamarte Hoyu");
            }
        }
    }
}
