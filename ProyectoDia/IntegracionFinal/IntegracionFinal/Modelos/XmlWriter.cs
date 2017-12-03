using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IntegracionFinal
{
    class XmlWriter
    {
        public static void GuardarXmlMiembros(string fileroute, List<Miembro> listaMiembros)
        {
            XElement raiz = new XElement("Miembros");


            foreach (Miembro miembro in listaMiembros)
            {
                raiz.Add(new XElement("Miembro",
                            new XAttribute("DNI", miembro.dni),
                            new XElement("Nombre", miembro.nombre),
                            new XElement("Apellidos", miembro.apellidos),
                            new XElement("Telefono", miembro.telefono),
                            new XElement("Email", miembro.email),
                            new XElement("Direccion", miembro.direccion)));
            }

            new XDocument(raiz).Save(fileroute);
        }
   
        public static void GuardarXmlPublicaciones(string ruta, List<Publicacion> listaPublicaciones)
        {
            XElement raiz = new XElement("Publicaciones");

            foreach (Publicacion p in listaPublicaciones)
            {
                XElement autores = new XElement("Autores");

                foreach (string autor in p.Autores)
                {
                    autores.Add(new XElement("Autor", autor));
                }

                XElement publicacion = new XElement("Publicacion");

                if (p.getTipo().Equals("Congreso"))
                {
                    XElement pub = new XElement(p.getTipo(), new XAttribute("DOI", p.DOI));
                    pub.Add(new XElement("Titulo", p.Titulo),
                                          new XElement("Editorial", p.Editorial), new XElement("FechaPublicacion", p.FechaPublicacion.ToString("dd/MM/yyyy")), new XElement("PaginaIni", p.PagInicio),
                                          new XElement("PaginaFin", p.PagFin), new XElement("Nombre", p.getNombre()),
                                          new XElement("Ciudad", p.getCiudad()), new XElement("Fecha", p.getFecha()), autores);
                    publicacion.Add(pub);

                }
                else if (p.getTipo().Equals("Libro"))
                {
                    XElement pub = new XElement(p.getTipo(), new XAttribute("DOI", p.DOI));
                    pub.Add(new XElement("Titulo", p.Titulo),
                                          new XElement("Editorial", p.Editorial), new XElement("FechaPublicacion", p.FechaPublicacion.ToString("dd/MM/yyyy")), new XElement("PaginaIni", p.PagInicio),
                                          new XElement("PaginaFin", p.PagFin), autores);
                    publicacion.Add(pub);
                }
                else if (p.getTipo().Equals("Articulo"))
                {
                    XElement pub = new XElement(p.getTipo(), new XAttribute("DOI", p.DOI));
                    pub.Add(new XElement("Titulo", p.Titulo),
                                          new XElement("Editorial", p.Editorial), new XElement("FechaPublicacion", p.FechaPublicacion.ToString("dd/MM/yyyy")), new XElement("PaginaIni", p.PagInicio),
                                          new XElement("PaginaFin", p.PagFin), autores);
                    publicacion.Add(pub);
                }
                raiz.Add(publicacion);
            }
            new XDocument(raiz).Save(ruta);
        }
    }
}
