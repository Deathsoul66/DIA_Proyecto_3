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
									  Convert.ToDateTime(Publicaciones.Element("anoPublicacion").Value.Trim()),
									  Convert.ToString(Publicaciones.Element("pagInicio").Value).Trim(),
									  Convert.ToString(Publicaciones.Element("pagFin").Value).Trim(),
														autores);
		}


		public static List<Publicacion> readPublicaciones(string filename)
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

							if (n1.Name == "FechaPublicacion")
							{
								/*string[] fech = n1.InnerText.Trim().Split('/');
								fechaPublicacion = new DateTime(Convert.ToInt32(fech[2]),
								                                Convert.ToInt32(fech[1]),
								                                Convert.ToInt32(fech[0]));
								*/
								//option B
								fechaPublicacion = Convert.ToDateTime(n1.InnerText.Trim());
								fechaPublicacion = fechaPublicacion.Date;
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
					Publicacion pub = Publicacion.Create(tipo, DOI, titulo, editorial, fechaPublicacion, pagInicio, pagFin, autores, nombre, ciudad, fecha);
					publicaciones.Add(pub);
					//THE NEXT LINES WAS FOR TESTING PURPOSES ONLY
					/*
					String auts = "";
					foreach (String a in autores)
					{
						auts += a + ", ";
					}
					Console.WriteLine(string.Format("[Publicacion: Tipo={0}, Id={1}, Titulo={2}, Editorial={3}, AnhoPublicacion={4}, PaginaIni={5}, PaginaFin={6}, Autores={7}, Nombre={8}, Ciudad={9}, Fecha={10}]", tipo, DOI, titulo, editorial, anoPublicacion, pagInicio, pagFin, auts, nombre, ciudad, fecha));
					*/
				}
			}
			return publicaciones;
		}

		public static List<Miembro> readMiembros(string filename)
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
					//NEXT LINE IS FOR TESTING PURPOSES ONLY
					Console.WriteLine(miem);
				}
			}
			return miembros;
		}
	}
}