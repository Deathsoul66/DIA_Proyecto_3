using Gtk;
using System;
using System.Collections.Generic;

namespace IntegracionFinal
{
    public partial class ViewIntegrada
    {
        private ListStore store;
        private enum Column { Tipo, Id, Titulo, Editorial, AnhoPublicacion, PaginaIni, PaginaFin, Autores, Nombre, Ciudad, Fecha };

        private ScrolledWindow sw;
        private TreeView treeView;
        //Utilizado en el filtro
        public string Autor
        {
            get; set;
        }
        //Utilizado en el filtro
        public string Anho
        {
            get; set;
        }
        //Utilizado en el filtro
        public string Tipo
        {
            get; set;
        }
        //Accion a ejecutar cuando se hace click en "Buscar" del filtro de publicaciones
        private void OnClickBuscar(object o, EventArgs e)
        {
            this.sw.Remove(this.treeView); //Limpia la vista
            this.treeView.Destroy();

            ListaPublicacion listaFilt = XmlReader.leerPublicaciones(PUBLICACIONES); //Se genera una nueva lista de publicaciones
            this.listaP = XmlReader.filtrarListaPublicaciones(listaFilt, this.Autor, this.Anho, this.Tipo); //Filtrada por lo estipulado

            this.treeView = new TreeView(this.CreateModel()); //Se genera el nuevo modelo
            //this.treeView.RulesHint = true;  //Para el sombreado de lineas pares
            this.AddColumns();
            this.sw.Add(this.treeView);
            this.treeView.RowActivated += UpdatePublicacion; //Al hacer clic en la fila ejecutar UpdatePublicacion
            this.ShowAll(); //Mostrar nuevo

        }
        //Devuelve todas las publicaciones contenidas en el XML
        private void OnClickFull(object o, EventArgs e)
        {

            this.sw.Remove(this.treeView);
            this.treeView.Destroy();

            this.listaP = XmlReader.leerPublicaciones(PUBLICACIONES);

            this.treeView = new TreeView(this.CreateModel());
            this.AddColumns();
            this.sw.Add(this.treeView);
            this.treeView.RowActivated += UpdatePublicacion;
            this.ShowAll();
        }

        //Método para añadir las columnas que se necesitan en el TreeView
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
        //Rellenar las filas del treeView con los datos recogidos
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

