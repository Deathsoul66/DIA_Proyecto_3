using Gtk;
using System;
using System.Collections.Generic;

namespace IntegracionFinal
{

    public partial class ViewIntegrada
    {
        private ListStore store;
        //private Statusbar statusbar;
        private enum Column { Tipo, Id, Titulo, Editorial, AnhoPublicacion, PaginaIni, PaginaFin, Autores, Nombre, Ciudad, Fecha };

        private ScrolledWindow sw;
        private TreeView treeView;

        public string Autor
        {
            get; set;
        }

        public string Anho
        {
            get; set;
        }

        public string Tipo
        {
            get; set;
        }


        private void OnClickBuscar(object o, EventArgs e)
        {
            //this.vbox.Remove(this.sw);
            this.sw.Remove(this.treeView);
            this.treeView.Destroy();

            //this.listaP = new ListaPublicacion();
            ListaPublicacion listaFilt = XmlReader.leerPublicaciones(PUBLICACIONES);
            this.listaP = XmlReader.filtrarListaPublicaciones(listaFilt, this.Autor, this.Anho, this.Tipo);

            this.treeView = new TreeView(this.CreateModel());
            //this.treeView.RulesHint = true;  //Para el sombreado de lineas pares
            this.AddColumns();
            this.sw.Add(this.treeView);
            //this.vbox.PackStart(this.sw, true, true, 0); //LISTAR
            this.treeView.RowActivated += UpdatePublicacion;
            this.treeView.SelectCursorRow += OnMenuAboutActivated;
            this.ShowAll();

        }

        private void OnClickFull(object o, EventArgs e)
        {

            //this.vbox.Remove(this.sw);
            this.sw.Remove(this.treeView);
            this.treeView.Destroy();

            //this.listaP = new ListaPublicacion();
            this.listaP = XmlReader.leerPublicaciones(PUBLICACIONES);

            this.treeView = new TreeView(this.CreateModel());
            //this.treeView.RulesHint = true;  //Para el sombreado de lineas pares
            this.AddColumns();
            this.sw.Add(this.treeView);
            //this.vbox.PackStart(this.sw, true, true, 0); //LISTAR
            this.treeView.RowActivated += UpdatePublicacion;
            this.ShowAll();


        }

        //private void OnClickQuit(object o, EventArgs e) { this.Hide(); Application.Quit(); }

        //private void OnRowActivated(object sender, RowActivatedArgs args)
        //{
        //    TreeIter iter;
        //    TreeView view = (TreeView)sender;

        //    if (view.Model.GetIter(out iter, args.Path))
        //    {
        //        string row = (string)view.Model.GetValue(iter, (int)Column.Tipo);
        //        row += ", " + (string)view.Model.GetValue(iter, (int)Column.Id);
        //        row += ", " + (string)view.Model.GetValue(iter, (int)Column.Titulo);
        //        row += ", " + (string)view.Model.GetValue(iter, (int)Column.Editorial);
        //        row += ", " + (string)view.Model.GetValue(iter, (int)Column.AnhoPublicacion);
        //        row += ", " + (string)view.Model.GetValue(iter, (int)Column.PaginaIni);
        //        row += ", " + (string)view.Model.GetValue(iter, (int)Column.PaginaFin);
        //        row += ", " + (string)view.Model.GetValue(iter, (int)Column.Autores);
        //        row += ", " + (string)view.Model.GetValue(iter, (int)Column.Nombre);
        //        row += ", " + (string)view.Model.GetValue(iter, (int)Column.Ciudad);
        //        row += ", " + (string)view.Model.GetValue(iter, (int)Column.Fecha);

        //        //statusbar.Push(0, row);
        //    }
        //}


        private void AddColumns()
        {
            CellRendererText rendererText = new CellRendererText();
            TreeViewColumn column = new TreeViewColumn("Tipo", rendererText, "text", Column.Tipo);
            column.SortColumnId = (int)Column.Tipo;
            this.treeView.AppendColumn(column);

            rendererText = new CellRendererText();
            column = new TreeViewColumn("DOI", rendererText, "text", Column.Id);
            column.SortColumnId = (int)Column.Id;
            this.treeView.AppendColumn(column);

            rendererText = new CellRendererText();
            column = new TreeViewColumn("Titulo", rendererText, "text", Column.Titulo);
            column.SortColumnId = (int)Column.Titulo;
            this.treeView.AppendColumn(column);

            rendererText = new CellRendererText();
            column = new TreeViewColumn("Editorial", rendererText, "text", Column.Editorial);
            column.SortColumnId = (int)Column.Editorial;
            this.treeView.AppendColumn(column);

            rendererText = new CellRendererText();
            column = new TreeViewColumn("Fecha publicacion", rendererText, "text", Column.AnhoPublicacion);
            column.SortColumnId = (int)Column.AnhoPublicacion;
            this.treeView.AppendColumn(column);

            rendererText = new CellRendererText();
            column = new TreeViewColumn("Pagina Inicio", rendererText, "text", Column.PaginaIni);
            column.SortColumnId = (int)Column.PaginaIni;
            this.treeView.AppendColumn(column);

            rendererText = new CellRendererText();
            column = new TreeViewColumn("Pagina Final", rendererText, "text", Column.PaginaFin);
            column.SortColumnId = (int)Column.PaginaFin;
            this.treeView.AppendColumn(column);

            rendererText = new CellRendererText();
            column = new TreeViewColumn("Autores", rendererText, "text", Column.Autores);
            column.SortColumnId = (int)Column.Autores;
            this.treeView.AppendColumn(column);

            rendererText = new CellRendererText();
            column = new TreeViewColumn("Nombre", rendererText, "text", Column.Nombre);
            column.SortColumnId = (int)Column.Nombre;
            this.treeView.AppendColumn(column);

            rendererText = new CellRendererText();
            column = new TreeViewColumn("Ciudad", rendererText, "text", Column.Ciudad);
            column.SortColumnId = (int)Column.Ciudad;
            this.treeView.AppendColumn(column);

            rendererText = new CellRendererText();
            column = new TreeViewColumn("Fecha", rendererText, "text", Column.Fecha);
            column.SortColumnId = (int)Column.Fecha;
            this.treeView.AppendColumn(column);
        }


        private ListStore CreateModel()
        {
            this.store = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));

            foreach (Publicacion p in listaP.listPub)
            {
                String a = "";
                for (int i = 0; i < p.Autores.Count; i++)
                {
                    if (i != p.Autores.Count - 1)
                    {
                        a += p.Autores[i] + ", ";
                    }
                    else
                    {
                        a += p.Autores[i];
                    }

                }
                store.AppendValues(p.getTipo(), p.DOI, p.Titulo, p.Editorial, p.FechaPublicacion.ToString("dd/MM/yyyy"), p.PagInicio, p.PagFin, p.autoresToString(), p.getNombre(), p.getCiudad(), p.getFecha());
            }


            return store;
        }
    }

}

