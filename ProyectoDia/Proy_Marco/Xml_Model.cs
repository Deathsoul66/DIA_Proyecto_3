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
					autores.Clear();
					foreach (XmlNode n in publicacion.ChildNodes)
					{
						if (n.Name == "Articulo" || n.Name == "Congreso" || n.Name == "Libro")
						{
							tipo = n.Name;
							id = n.Attributes["DOI"].InnerText;
						}
						foreach (XmlNode n1 in n.ChildNodes)
						{
							if (n1.Name == "Titulo")
							{
								titulo = n1.Name;
								titulo = titulo.Trim();
							}

							if (n1.Name == "Editorial")
							{
								editorial = n1.InnerText;
								editorial = editorial.Trim();
							}

							if (n1.Name == "AnhoPublicacion")
							{
								anho = n1.InnerText;
								anho = anho.Trim();
							}

							if (n1.Name == "PaginaIni")
							{
								inicio = n1.InnerText;
								inicio = inicio.Trim();
							}

							if (n1.Name == "PaginaFin")
							{
								fin = n1.InnerText;
								fin = fin.Trim();
							}

							if (n1.Name == "Autores")
							{
								foreach (XmlNode n2 in n1.ChildNodes)
								{
									if (n2.Name == "Autor")
									{
										autor = n2.InnerText;
										autor = autor.Trim();
									}

									autores.Add(autor);
								}
							}

							if (n1.Name == "Nombre")
							{
								nombre = n1.InnerText;
								nombre = nombre.Trim();
							}

							if (n1.Name == "Ciudad")
							{
								ciudad = n1.InnerText;
								ciudad = ciudad.Trim();
							}

							if (n1.Name == "Fecha")
							{
								fecha = n1.InnerText;
								fecha = fecha.Trim();
							}
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