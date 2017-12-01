using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace Proy_ImaTor
{
    public class ListaPublicaciones
    {
        
        private Dictionary<string, Publicacion> dicionario = new Dictionary<string, Publicacion>();
        public List<Publicacion> GetPublicaciones (){
            
            return dicionario.Select(d => d.Value).ToList();
        }

        public Publicacion GetPublicacion(string DOI){
            Publicacion p = new Publicacion();
			
            return dicionario.TryGetValue(DOI, out p)? p : null;
        }

        public bool Add(Publicacion p)
        {
            if(dicionario.ContainsKey(p.DOI)){
                return false;
            }else{
                dicionario.Add(p.DOI,p);
                return true;
            }
        }

        public bool Eliminar(string DOI)
        {
            return dicionario.Remove(DOI);
        }

        public bool Modificar(Publicacion p)
        {
			if (dicionario.ContainsKey(p.DOI)){
                dicionario[p.DOI] = p;
				return true;
			}else{
                return false;
			}
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
                                          new XElement("PaginaFin", p.PaginaFinal), new XElement("Nombre", ((Congreso)p).Nombre), 
                                          new XElement("Ciudad", ((Congreso)p).Ciudad), new XElement("Fecha", ((Congreso)p).Fecha.ToString("d")), autores));

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
            int cont = 0;
            try{
                XElement raiz = XElement.Load(ruta);

                foreach(XElement publicacion in raiz.Elements("Publicacion")){

                    List<string> autores = new List<string>();

                    foreach (XElement autor in publicacion.Element("Autores").Elements("Autor"))
					{
                        autores.Add(autor.Value);
					}

                    try{
						if (publicacion.Element("Congreso") != null)
						{

							dicionario.Add(publicacion.Element("Congreso").Attribute("DOI").Value, new Congreso(publicacion.Element("Congreso").Attribute("DOI").Value, publicacion.Element("Titulo").Value,
											 publicacion.Element("Editorial").Value, Int32.Parse(publicacion.Element("Anho").Value), Int32.Parse(publicacion.Element("PaginaIni").Value),
											 Int32.Parse(publicacion.Element("PaginaFin").Value), publicacion.Element("Nombre").Value,
											 publicacion.Element("Ciudad").Value, DateTime.ParseExact(publicacion.Element("Fecha").Value, "dd/MM/yyyy",
																									  System.Globalization.CultureInfo.InvariantCulture), autores.ToArray()));
						}
						else if (publicacion.Element("Articulo") != null)
						{

							dicionario.Add(publicacion.Element("Articulo").Attribute("DOI").Value, new Articulo(publicacion.Element("Articulo").Attribute("DOI").Value, publicacion.Element("Titulo").Value,
											 publicacion.Element("Editorial").Value, Int32.Parse(publicacion.Element("Anho").Value), Int32.Parse(publicacion.Element("PaginaIni").Value),
											 Int32.Parse(publicacion.Element("PaginaFin").Value), autores.ToArray()));

						}
						else if (publicacion.Element("Libro") != null)
						{

							dicionario.Add(publicacion.Element("Libro").Attribute("DOI").Value, new Libro(publicacion.Element("Libro").Attribute("DOI").Value, publicacion.Element("Titulo").Value,
											 publicacion.Element("Editorial").Value, Int32.Parse(publicacion.Element("Anho").Value), Int32.Parse(publicacion.Element("PaginaIni").Value),
											 Int32.Parse(publicacion.Element("PaginaFin").Value), autores.ToArray()));
						}
                    }catch{
                        cont++;
                    }
                }
                    
            }catch{
                throw new Exception("Error al abrir archivo!");
            }
            if(cont!=0){
                throw new Exception("Error al crear Publicacion leyendo el archivo. Formato incorrecto!");
            }
        }
    }
}
