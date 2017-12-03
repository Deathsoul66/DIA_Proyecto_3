using System;
using System.Collections.Generic;
using Gtk;

namespace IntegracionFinal
{
	public partial class MainWindowViewFiltroPublicaciones
    {
        private void OnClickBuscar(object o, EventArgs e)
        {

            this.pnlFull.Remove(this.sw);
            this.sw.Remove(this.treeView);
            this.treeView.Destroy();

            this.listaP = new ListaPublicacion();
            ListaPublicacion listaFilt = XmlReader.leerPublicaciones("Test.xml");
            this.listaP = XmlReader.filtrarListaPublicaciones(listaFilt, this.Autor, this.Anho, this.Tipo);


            this.treeView = new TreeView(this.CreateModel());
            this.treeView.RulesHint = true;
            this.AddColumns();
            this.sw.Add(this.treeView);
            this.pnlFull.PackStart(this.sw, true, true, 0); //LISTAR
            this.ShowAll();

        }

        private void OnClickFull(object o, EventArgs e)
        {

            this.pnlFull.Remove(this.sw);
            this.sw.Remove(this.treeView);
            this.treeView.Destroy();

            this.listaP = new ListaPublicacion();
            this.listaP = XmlReader.leerPublicaciones("Test.xml");

            this.treeView = new TreeView(this.CreateModel());
            this.treeView.RulesHint = true;
            this.AddColumns();
            this.sw.Add(this.treeView);
            this.pnlFull.PackStart(this.sw, true, true, 0); //LISTAR
            this.ShowAll();


        }

        private void OnClickQuit(object o, EventArgs e) { this.Hide(); Application.Quit(); }
    }
}

