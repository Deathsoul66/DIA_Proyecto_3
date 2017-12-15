using System;
using System.Text;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace IntegracionFinal
{
	public class XmlReader
	{
		//Funcion para recuperar las publicaciones del archivo XML
		public static ListaPublicacion leerPublicaciones(string filename)
		{
			List<Publicacion> publicaciones = new List<Publicacion>();

			XmlDocument xmlDoc = new XmlDocument();
			TipoPublicacion tipo = new TipoPublicacion();
			string DOI = "";
			string titulo = "";
			string editorial = "";
			DateTime fechaPublicacion = new DateTime(); ;
			string pagInicio = "";
			string pagFin = "";
			string autor = "";
			string nombre = "";
			string ciudad = "";
			string fecha = "";

			xmlDoc.Load(filename);

			if (xmlDoc.DocumentElement.Name == "Publicaciones")
			{
				foreach (XmlNode publicacion in xmlDoc.DocumentElement.ChildNodes)
				{
					List<string> autores = new List<string>();
					foreach (XmlNode n in publicacion.ChildNodes)
					{
						if (n.Name == "Articulo" || n.Name == "Congreso" || n.Name == "Libro")
						{
							switch (n.Name)
							{
								case "Articulo":
									tipo = TipoPublicacion.Articulo;
									break;
								case "Congreso":
									tipo = TipoPublicacion.Congreso;
									break;
								case "Libro":
									tipo = TipoPublicacion.Libro;
									break;
							}
							DOI = n.Attributes["DOI"].InnerText.Trim();
						}
						foreach (XmlNode n1 in n.ChildNodes)
						{
							switch (n1.Name)
							{
								case "Titulo":
									titulo = n1.InnerText.Trim();
									break;
								case "Editorial":
									editorial = n1.InnerText.Trim();
									break;
								case "FechaPublicacion":
									fechaPublicacion = DateTime.Parse(n1.InnerText.Trim(), new CultureInfo("es-ES"));
									break;
								case "PaginaIni":
									pagInicio = n1.InnerText.Trim();
									break;
								case "PaginaFin":
									pagFin = n1.InnerText.Trim();
									break;
								case "Autores":
									foreach (XmlNode n2 in n1.ChildNodes)
									{
										if (n2.Name == "Autor")
										{
											autor = n2.InnerText.Trim();
										}
										autores.Add(autor);
									}
									break;
								case "Nombre":
									nombre = n1.InnerText.Trim();
									break;
								case "Ciudad":
									ciudad = n1.InnerText.Trim();
									break;
								case "Fecha":
									fecha = n1.InnerText.Trim();
									break;
							}
						}
					}
					Publicacion pub = Publicacion.Create(tipo, DOI, titulo, editorial, fechaPublicacion, pagInicio, pagFin, autores, nombre, ciudad, fecha);
					publicaciones.Add(pub);
					
				}
			}
            ListaPublicacion toRet = new ListaPublicacion();
            toRet.listPub = publicaciones;
			return toRet;
		}
        //Funcion para recuperar los miemros del archivo XML
        public static List<Miembro> leerMiembros(string filename)
		{
			List<Miembro> miembros = new List<Miembro>();

			XmlDocument xmlDoc = new XmlDocument();
			string dni = "", nombre = "", apellidos = "", telefono = "", email = "", direccion = "";

			xmlDoc.Load(filename);

			if (xmlDoc.DocumentElement.Name == "Miembros")
			{
				foreach (XmlNode miembro in xmlDoc.DocumentElement.ChildNodes)
				{
					dni = miembro.Attributes["DNI"].InnerText.Trim();
					foreach (XmlNode m in miembro.ChildNodes)
					{
						switch (m.Name)
						{
							case ("Nombre"):
								nombre = m.InnerText.Trim();
								break;
							case ("Apellidos"):
								apellidos = m.InnerText.Trim();
								break;
							case ("Telefono"):
								telefono = m.InnerText.Trim();
								break;
							case ("Email"):
								email = m.InnerText.Trim();
								break;
							case ("Direccion"):
								direccion = m.InnerText.Trim();
								break;
						}
					}
					Miembro miem = new Miembro(dni, nombre, apellidos, telefono, email, direccion);
					miembros.Add(miem);
				}
			}
			return miembros;
		}
        //Funcion para filtrar las publicaciones del archivo XML mediante una seria de parámetros
        public static ListaPublicacion filtrarListaPublicaciones(ListaPublicacion listaP, String nombre = "", String anho = "", String tipo = "")
        {
            ListaPublicacion toRet1 = listaP;
            ListaPublicacion toRet2 = new ListaPublicacion();
            ListaPublicacion toRet3 = new ListaPublicacion();
            ListaPublicacion toRetFin = new ListaPublicacion();

            listaP = null;

            if (nombre != "")
            {
                for (int i = 0; i < toRet1.getLength(); i++)
                {
                    List<String> listaAut = toRet1.getPublicacionIndex(i).Autores;
                    bool insert = false;

                    for (int j = 0; j < listaAut.Count(); j++)
                    {
                        if (listaAut[j].Contains(nombre))
                        {
                            insert = true;
                        }
                    }

                    if (insert == true)
                    {
                        toRet2.addPublicacion(toRet1.getPublicacionIndex(i));
                    }

                    listaAut = null;
                }
            }
            else
            {
                toRet2 = toRet1;
            }

            toRet1 = null;

            if (anho != "")
            {
                for (int i = 0; i < toRet2.getLength(); i++)
                {
                    if (toRet2.getPublicacionIndex(i).FechaPublicacion.ToString("yyyy").Contains(anho))
                    {
                        toRet3.addPublicacion(toRet2.getPublicacionIndex(i));
                    }
                }
            }
            else
            {
                toRet3 = toRet2;
            }

            toRet2 = null;

            if (tipo != "")
            {
                for (int i = 0; i < toRet3.getLength(); i++)
                {
                    if (toRet3.getPublicacionIndex(i).getTipo().Contains(tipo))
                    {
                        toRetFin.addPublicacion(toRet3.getPublicacionIndex(i));
                    }
                }
            }
            else
            {
                toRetFin = toRet3;
            }

            toRet3 = null;

            return toRetFin;

        }
        //Funcion para recuperar los miemros del archivo XML utilizando LINQ (Alternativa)
        public static List<Miembro> leerMiembrosLinq(string fileroute)
        {
            List<Miembro> toret = new List<Miembro>();
            XElement raiz = XElement.Load(fileroute);

            foreach (XElement Miembro in raiz.Elements("Miembro"))
            {

                Miembro aux = new Miembro
                {
                    dni = Miembro.Attribute("DNI").Value,
                    nombre = Miembro.Element("Nombre").Value,
                    apellidos = Miembro.Element("Apellidos").Value,
                    telefono = Miembro.Element("Telefono").Value,
                    email = Miembro.Element("Email").Value,
                    direccion = Miembro.Element("Direccion").Value
                };
                toret.Add(aux);
            }
            return toret;
        }

    }
}