using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Proy_Marco;

namespace Proy_Marco
{
	public class XMLreader
	{
		public XMLreader()
		{
		}

		public static listaPublicacion devolverXMLLinqCongreso(String nombre = null, String anho = null, String tipo = null)
		{
			listaPublicacion pubList = new listaPublicacion();
			XElement raiz = XElement.Load("Test.xml");

			var listaPub = from elem in raiz.Elements("Publicacion")
						   select new Publicacion(
												   Convert.ToString(elem.Name).Trim(),
												   Convert.ToString(elem.Attribute("DOI").Value).Trim(),
												   Convert.ToString(elem.Element("Titulo").Value).Trim(),
												   Convert.ToString(elem.Element("Editorial").Value).Trim(),
												   Convert.ToString(elem.Element("AnhoPublicacion").Value).Trim(),
												   Convert.ToString(elem.Element("PaginaIni").Value).Trim(),
												   Convert.ToString(elem.Element("PaginaFin").Value).Trim(),
												   null,
												   Convert.ToString(elem.Attribute("Nombre").Value).Trim(),
												   Convert.ToString(elem.Attribute("Ciudad").Value).Trim(),
												   Convert.ToString(elem.Attribute("Fecha").Value).Trim()
											   );

			List<Publicacion> lp = listaPub.ToList<Publicacion>();

			foreach (Publicacion pub in listaPub)
			{
				pubList.addPublicacion(pub);
			}

			return pubList;
		}

		public static listaPublicacion devolverXML()
		{
			listaPublicacion toRet = new listaPublicacion();
			XmlDocument xmlDoc = new XmlDocument();
			string tipo = "";
			string id = "";
			string titulo = "";
			string editorial = "";
			string anho = "";
			string inicio = "";
			string fin = "";
			string autor = "";
			string nombre = "";
			string ciudad = "";
			string fecha = "";
			List<string> autores = new List<string>();

			xmlDoc.Load("Test.xml");

			if (xmlDoc.DocumentElement.Name == "Publicaciones")
			{
				foreach (XmlNode publicacion in xmlDoc.DocumentElement.ChildNodes)
				{
					foreach (XmlNode n in publicacion.ChildNodes)
					{
						if (n.Name == "Articulo")
						{
							tipo = n.Name;
							id = n.Value;
						}

						if (n.Name == "Titulo")
						{
							titulo = n.Name;
							titulo = titulo.Trim();
						}

						if (n.Name == "Editorial")
						{
							editorial = n.InnerText;
							editorial = editorial.Trim();
						}

						if (n.Name == "AnhoPublicacion")
						{
							anho = n.InnerText;
							anho = anho.Trim();
						}

						if (n.Name == "PaginaIni")
						{
							inicio = n.InnerText;
							inicio = inicio.Trim();
						}

						if (n.Name == "PaginaFin")
						{
							fin = n.InnerText;
							fin = fin.Trim();
						}

						foreach (XmlNode n2 in n.ChildNodes)
						{
							if (n.Name == "Autor")
							{
								autor = n.InnerText;
								autor = autor.Trim();
							}

							autores.Add(autor);
						}

						if (n.Name == "Nombre")
						{
							nombre = n.InnerText;
							nombre = nombre.Trim();
						}

						if (n.Name == "Ciudad")
						{
							ciudad = n.InnerText;
							ciudad = ciudad.Trim();
						}

						if (n.Name == "Fecha")
						{
							fecha = n.InnerText;
							fecha = fecha.Trim();
						}


						Publicacion pub = new Publicacion();

						pub.Tipo = tipo;
						pub.Id = id;
						pub.Titulo = titulo;
						pub.Editorial = editorial;
						pub.AnhoPublicacion = anho;
						pub.PaginaIni = inicio;
						pub.PaginaFin = fin;
						pub.Autores = autores;
						pub.Nombre = nombre;
						pub.Ciudad = ciudad;
						pub.Fecha = fecha;

						toRet.addPublicacion(pub);
					}
				}
			}

			return toRet;
		}

	}
}