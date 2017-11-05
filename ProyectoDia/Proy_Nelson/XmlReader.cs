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
		public XmlReader(string filename)
		{
			XElement XML = XElement.Load(filename);

			var consulta = from Publicaciones in XML.Elements("Articulo")
			select Publicacion.Create(
					   TipoPublicacion.Articulo,
					   Convert.ToString(Publicaciones.Attribute("DOI").Value).Trim(),
					   Convert.ToString(Publicaciones.Element("titulo").Value).Trim(),
					   Convert.ToString(Publicaciones.Element("editorial").Value).Trim(),
					   Convert.ToString(Publicaciones.Element("anoPublicacion").Value).Trim(),
					   Convert.ToString(Publicaciones.Element("pagInicio").Value).Trim(),
					   Convert.ToString(Publicaciones.Element("pagFin").Value).Trim(),
					   new String[] { "abc", "DEF" });

		}
	}
}
