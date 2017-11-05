using System;
using System.Text;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Proy_Nelson
{
	public class XmlReader
	{



		public static void readLinq(string filename)
		{
			XElement XML = XElement.Load(filename);
			List<string> autores = new List<string>();
			var consulta = from Publicaciones in XML.Elements("Articulo")
						   select Publicacion.Create(
									  TipoPublicacion.Articulo,
									  Convert.ToString(Publicaciones.Attribute("DOI").Value).Trim(),
									  Convert.ToString(Publicaciones.Element("titulo").Value).Trim(),
									  Convert.ToString(Publicaciones.Element("editorial").Value).Trim(),
									  Convert.ToString(Publicaciones.Element("anoPublicacion").Value).Trim(),
									  Convert.ToString(Publicaciones.Element("pagInicio").Value).Trim(),
									  Convert.ToString(Publicaciones.Element("pagFin").Value).Trim(),
														autores);
		}

		public static List<Publicacion> read()
		{
			List<Publicacion> publicaciones = new List<Publicacion>();

			XmlDocument xmlDoc = new XmlDocument();
			TipoPublicacion tipo = new TipoPublicacion();
			string DOI = "";
			string titulo = "";
			string editorial = "";
			string anoPublicacion = "";
			string pagInicio = "";
			string pagFin = "";
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
							DOI = n.Attributes["DOI"].InnerText;
						}
						foreach (XmlNode n1 in n.ChildNodes)
						{
							if (n1.Name == "Titulo")
							{
								titulo = n1.InnerText;
								titulo = titulo.Trim();
							}

							if (n1.Name == "Editorial")
							{
								editorial = n1.InnerText;
								editorial = editorial.Trim();
							}

							if (n1.Name == "AnhoPublicacion")
							{
								anoPublicacion = n1.InnerText;
								anoPublicacion = anoPublicacion.Trim();
							}

							if (n1.Name == "PaginaIni")
							{
								pagInicio = n1.InnerText;
								pagInicio = pagInicio.Trim();
							}

							if (n1.Name == "PaginaFin")
							{
								pagFin = n1.InnerText;
								pagFin = pagFin.Trim();
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
					}
					Publicacion pub = Publicacion.Create(tipo, DOI, titulo, editorial, anoPublicacion, pagInicio, pagFin, autores, nombre, ciudad, fecha);
					publicaciones.Add(pub);
					//Console.WriteLine(string.Format("[Publicacion: Tipo={0}, Id={1}, Titulo={2}, Editorial={3}, AnhoPublicacion={4}, PaginaIni={5}, PaginaFin={6}, Autores={7}, Nombre={8}, Ciudad={9}, Fecha={10}]", tipo, DOI, titulo, editorial, anoPublicacion, pagInicio, pagFin, autores, nombre, ciudad, fecha));
				}
			}
			return publicaciones;
		}
	}
}
