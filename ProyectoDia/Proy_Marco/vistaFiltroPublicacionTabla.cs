using System;
using System.Collections.Generic;
using Gtk;

namespace Proy_Marco
{
	public class vistaFiltroPublicacionTabla : Window
	{
		private ListStore store;
		private Statusbar statusbar;
		private enum Column { Tipo, Id, Titulo, Editorial, AnhoPublicacion, PaginaIni, PaginaFin, Autores, Nombre, Ciudad, Fecha };
		private listaPublicacion listaP;


		public vistaFiltroPublicacionTabla(listaPublicacion listaP) : base("Tabla Filtrada")
		{
			this.listaP = listaP;
			Build();
		}

		private void Build()
		{
			BorderWidth = 8;
			SetDefaultSize(1100, 250);
			SetPosition(WindowPosition.Center);
			DeleteEvent += delegate { this.Hide(); };

			VBox vbox = new VBox(false, 8);

			ScrolledWindow sw = new ScrolledWindow();
			sw.ShadowType = ShadowType.EtchedIn;
			sw.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
			vbox.PackStart(sw, true, true, 0);

			store = CreateModel();

			TreeView treeView = new TreeView(store);
			treeView.RulesHint = true;
			treeView.RowActivated += OnRowActivated;
			sw.Add(treeView);

			AddColumns(treeView);

			statusbar = new Statusbar();

			vbox.PackStart(statusbar, false, false, 0);

			Add(vbox);
			ShowAll();
		}

		private void OnRowActivated(object sender, RowActivatedArgs args)
		{
			TreeIter iter;
			TreeView view = (TreeView)sender;

			if (view.Model.GetIter(out iter, args.Path))
			{
				string row = (string)view.Model.GetValue(iter, (int)Column.Tipo);
				row += ", " + (string)view.Model.GetValue(iter, (int)Column.Id);
				row += ", " + (string)view.Model.GetValue(iter, (int)Column.Titulo);
				row += ", " + (string)view.Model.GetValue(iter, (int)Column.Editorial);
				row += ", " + (string)view.Model.GetValue(iter, (int)Column.AnhoPublicacion);
				row += ", " + (string)view.Model.GetValue(iter, (int)Column.PaginaIni);
				row += ", " + (string)view.Model.GetValue(iter, (int)Column.PaginaFin);
				row += ", " + (string)view.Model.GetValue(iter, (int)Column.Autores);
				row += ", " + (string)view.Model.GetValue(iter, (int)Column.Nombre);
				row += ", " + (string)view.Model.GetValue(iter, (int)Column.Ciudad);
				row += ", " + (string)view.Model.GetValue(iter, (int)Column.Fecha);

				statusbar.Push(0, row);
			}
		}

		private void AddColumns(TreeView treeView)
		{
			CellRendererText rendererText = new CellRendererText();
			TreeViewColumn column = new TreeViewColumn("Tipo", rendererText, "text", Column.Tipo);
			column.SortColumnId = (int)Column.Tipo;
			treeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Id", rendererText, "text", Column.Id);
			column.SortColumnId = (int)Column.Id;
			treeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Titulo", rendererText, "text", Column.Titulo);
			column.SortColumnId = (int)Column.Titulo;
			treeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Editorial", rendererText, "text", Column.Editorial);
			column.SortColumnId = (int)Column.Editorial;
			treeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Año", rendererText, "text", Column.AnhoPublicacion);
			column.SortColumnId = (int)Column.AnhoPublicacion;
			treeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Pagina Inicio", rendererText, "text", Column.PaginaIni);
			column.SortColumnId = (int)Column.PaginaIni;
			treeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Pagina Final", rendererText, "text", Column.PaginaFin);
			column.SortColumnId = (int)Column.PaginaFin;
			treeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Autores", rendererText, "text", Column.Autores);
			column.SortColumnId = (int)Column.Autores;
			treeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Nombre", rendererText, "text", Column.Nombre);
			column.SortColumnId = (int)Column.Nombre;
			treeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Ciudad", rendererText, "text", Column.Ciudad);
			column.SortColumnId = (int)Column.Ciudad;
			treeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Fecha", rendererText, "text", Column.Fecha);
			column.SortColumnId = (int)Column.Fecha;
			treeView.AppendColumn(column);
		}


		private ListStore CreateModel()
		{
			ListStore store = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));

			foreach (Publicacion p in listaP.listPub)
			{
				String a = "";
				for (int i = 0; i < p.Autores.Count;i++) 
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
				store.AppendValues(p.Tipo, p.Id, p.Titulo,p.Editorial,p.AnhoPublicacion,p.PaginaIni,p.PaginaFin,a,p.Nombre,p.Ciudad,p.Fecha);
			}

			return store;
		}

	}
}
