using System;
using System.Xml;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
namespace ProyectoFinal
{
    public class Miembro
    {
        
        public Miembro()
        {

        }
        public Miembro(string DNI, string nombre, string telefono, string email, string direccion)
        {
            this.DNI = DNI;
            this.nombre = nombre;
            this.telefono = telefono;
            this.email = email;
            this.direccion = direccion;

        }

       
        public string DNI
        {
            get;set;
        }

        public string nombre
        {
            get;set;
        }

        public string telefono
        {
            get;set;
        }

        public string email
        {
            get;set;
        }

        public string direccion
        {
            get;set;
        }

        public override string ToString()
        {
            string toret = "";

            toret += "Miembro: DNI=" + DNI + ", Nombre=" + nombre + ", Telefono=" + telefono + ", Email=" + email + ", Direccion=" + direccion;
            return toret;
        }



        
        public static void GeneraXML(string fileroute, List<Miembro> listaMiembros)
        {
            XElement raiz = new XElement("Miembros");


            foreach (Miembro miembro in listaMiembros)
            {
                raiz.Add(new XElement("Miembro",
                                       new XAttribute("DNI", miembro.DNI), new XElement("Nombre", miembro.nombre), new XElement("Telefono", miembro.telefono), new XElement("Email", miembro.email), new XElement("Direccion", miembro.direccion)));
            }
       
            new XDocument(raiz).Save(fileroute);
        }

        public static List<Miembro> LeeXML(string fileroute)
        {
            List<Miembro> toret = new List<Miembro>();
            XElement raiz = CargarXML(fileroute);

            foreach (XElement Miembro in raiz.Elements("Miembro"))
            {

                Miembro aux = new Miembro
                {
                    DNI = Miembro.Attribute("DNI").Value,
                    nombre = Miembro.Element("Nombre").Value,
                    telefono = Miembro.Element("Telefono").Value,
                    email = Miembro.Element("Email").Value,
                    direccion = Miembro.Element("Direccion").Value
                };
                toret.Add(aux);
            }
            return toret;
        }

        public static XElement CargarXML(string fileroute)
        {
            XElement raiz = null;
            try
            {
                raiz = XElement.Load(fileroute);
            }
            catch
            {
                /*new XDocument(
					new XElement("Miembros"))
					.Save(fileroute);
				raiz = XElement.Load( fileroute );*/
    }
            return raiz;

        }





    }
}
