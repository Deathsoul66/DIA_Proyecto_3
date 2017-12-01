using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProyectoFinal
{

    public class ListMiembro
    {
       static List<Miembro> listM;


        public ListMiembro()
        {
            listM = new List<Miembro>();
        }

        public static List<Miembro> listarMiembros
        {
            get; set;
        }



        public static void AddMiembro(Miembro miembro)
        {
            listM.Add(miembro);
        }

        public static Miembro GetMiembro(int i)
        {
            return listM[i];
        }

        public int getLength()
        {
            return listM.Count;
        }

        public override string ToString()
        {
            string toRet = "";
            foreach (Miembro pub in listM)
            {
                toRet += pub.ToString() + "\n";
            }
            return toRet;
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
    







































        /*
        public List<Miembro> listaMiembro
        {
            get; set;
        }

        public ListMiembro()
        {
            listaMiembro = new List<Miembro>();
        }

        public void addMiembro(Miembro miembro)
        {

            this.listaMiembro.Add(miembro);
        }

       
        public override string ToString()
        {
            string toret = "";
            foreach (Miembro member in listaMiembro)
            {
                toret += member.ToString() + "\n";
            }
            return toret;
        }
        */
    
