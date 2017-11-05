using System;

namespace Proy_Marco
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			Console.WriteLine("Testing XML Reader");
			listaPublicacion toRet = XMLreader.devolverXML();
			//listaPublicacion toRet = XMLreader.devolverXMLLinqCongreso();
			Console.WriteLine(toRet.ToString());
			Console.Read();
		}
	}
}
