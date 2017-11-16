using System;
namespace Proy_ImaTor.View
{
    using Gtk;
    public partial class MainWindow
    {

		private string fileroute;
        private int cont;
        private ListaPublicaciones listaP = new ListaPublicaciones();

		private void AbrirXML(object sender, EventArgs e)
		{

			Gtk.FileChooserDialog fc =
				new Gtk.FileChooserDialog("Choose the file to open",
										  this,
										  Gtk.FileChooserAction.Open,
										  "Cancel", Gtk.ResponseType.Cancel,
										  "Open", Gtk.ResponseType.Accept);
			fc.Filter = new Gtk.FileFilter();
			fc.Filter.AddPattern("*.xml");

			if (fc.Run() == (int)Gtk.ResponseType.Accept)
			{
				System.IO.FileStream file = System.IO.File.OpenRead(fc.Filename);
				this.fileroute = fc.Filename;
				ls.Clear();
				InsertaDatos();
				file.Close();

			}
			//Destroy() to close the File Dialog
			fc.Destroy();

		}

		private void InsertaDatos()
		{
			cont = 1;
            listaP.CargarXML(fileroute);
            foreach (Publicacion p in listaP.GetPublicaciones())
			{
                string autores = "";
                foreach(string autor in p.Autores){
                    autores += autor + " ";
                }

                if(p.Tipo.Equals("Congreso")){
                    ls.AppendValues(cont++, p.Tipo, p.DOI, p.Titulo, p.Editorial, p.AnhoPublicacion, p.PaginaInicial, p.PaginaFinal, autores, 
                                    ((Congreso)p).Nombre,((Congreso)p).Ciudad,((Congreso)p).Fecha.ToString("d"));
                }else{
                    ls.AppendValues(cont++, p.Tipo, p.DOI, p.Titulo, p.Editorial, p.AnhoPublicacion, p.PaginaInicial, p.PaginaFinal, autores, "-", "-", "-");
                }
				
			}
		}
    }

}
