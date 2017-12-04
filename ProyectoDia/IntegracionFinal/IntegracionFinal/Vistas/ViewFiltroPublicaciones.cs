using System;
using System.Collections.Generic;
using Gtk;

namespace IntegracionFinal
{
	public partial class MainWindowViewFiltroPublicaciones : Window
	{
		private ListStore store;
		private Statusbar statusbar;
		private enum Column { Tipo, Id, Titulo, Editorial, AnhoPublicacion, PaginaIni, PaginaFin, Autores, Nombre, Ciudad, Fecha };
		private ListaPublicacion listaP;
		private ScrolledWindow sw;
		private VBox pnlFull;
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


		public MainWindowViewFiltroPublicaciones() : base("Tabla Filtro")
		{
			Autor = "";
			Anho = "";
			Tipo = "";
            this.listaP = XmlReader.leerPublicaciones("Test.xml");
			Build();
		}

		private void Build()
		{
			BorderWidth = 8;
			SetDefaultSize(1100, 400);
			SetPosition(WindowPosition.Center);
			DeleteEvent += delegate { this.Hide(); Application.Quit(); };

			HBox pnlAutor = new HBox(false, 5);
			HBox pnlAnho = new HBox(false, 5);
			HBox pnlTipo = new HBox(false, 5);
			VBox pnlButton1 = new VBox(false, 5);
			VBox pnlButton2 = new VBox(false, 5);
			VBox pnlButton3 = new VBox(false, 5);

			HBox pnlBuscador = new HBox(false,5);

			pnlFull = new VBox();

			Label labelTAutor = new Label("Autor");
			Entry entryAutor = new Entry();
			entryAutor.Changed += (o, e) => this.Autor = entryAutor.Text;


			pnlAutor.PackStart(labelTAutor, false, false, 5);
			pnlAutor.PackStart(entryAutor, false, false, 5);

			Label labelTAnho = new Label("Año");
			Entry entryAnho = new Entry();
			entryAnho.Changed += (o, e) => this.Anho = entryAnho.Text;


			pnlAnho.PackStart(labelTAnho, false, false, 5);
			pnlAnho.PackStart(entryAnho, false, false, 5);

			Label labelTTipo = new Label("Tipo");
			string[] values = new string[] { "", "Libro", "Articulo", "Congreso" };
			ComboBox comboTipo = new ComboBox(values);
			comboTipo.Changed += (o, e) => this.Tipo = comboTipo.ActiveText;


			pnlTipo.PackStart(labelTTipo, false, false, 5);
			pnlTipo.PackStart(comboTipo, false, false, 5);

			Button buscar = new Button("Buscar");
			buscar.Clicked += OnClickBuscar;
			buscar.SetSizeRequest(50, 20);
			pnlButton1.PackStart(buscar, false, false, 5);

			Button full = new Button("Show All");
			full.Clicked += OnClickFull;
			full.SetSizeRequest(70, 20);
			pnlButton2.PackStart(full, false, false, 5);

			Button quit = new Button("Quit");
			quit.Clicked += OnClickQuit;
			quit.SetSizeRequest(50, 20);
			pnlButton3.PackStart(quit, false, false, 5);

			pnlBuscador.PackStart(pnlAutor, false, false, 5);
			pnlBuscador.PackStart(pnlAnho, false, false, 5);
			pnlBuscador.PackStart(pnlTipo, false, false, 5);
			pnlBuscador.PackStart(pnlButton1, false, false, 5);
			pnlBuscador.PackStart(pnlButton2, false, false, 5);
			pnlBuscador.PackStart(pnlButton3, false, false, 5);

			pnlFull.PackStart(pnlBuscador, false, false, 5);

			//VBox vbox = new VBox(false, 8);

			this.sw = new ScrolledWindow();
			this.sw.ShadowType = ShadowType.EtchedIn;
			this.sw.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
			//vbox.PackStart(sw, true, true, 0);
			pnlFull.PackStart(this.sw, true, true, 0);

			store = CreateModel();

			this.treeView = new TreeView(store);
			this.treeView.RulesHint = true;
			this.treeView.RowActivated += OnRowActivated;
			this.sw.Add(treeView);

			AddColumns();

			statusbar = new Statusbar();

			pnlFull.PackStart(statusbar, false, false, 0);

			Add(pnlFull);
			this.ShowAll();
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
			column = new TreeViewColumn("Año", rendererText, "text", Column.AnhoPublicacion);
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
			ListStore store = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));

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
				store.AppendValues(p.getTipo(), p.DOI, p.Titulo, p.Editorial, p.FechaPublicacion.ToString("yyyy"), p.PagInicio, p.PagFin, p.autoresToString(), p.getNombre(), p.getCiudad(), p.getFecha());
            }

			return store;
		}

	}
}
