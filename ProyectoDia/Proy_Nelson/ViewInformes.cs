using Gtk;
using System;
using System.Collections.Generic;

namespace Proy_Nelson
{
	public class ViewInformes : Window
	{
		enum Column
		{
			tipo, DOI, titulo, editorial, anoPublicacion, pagInicio, pagFin, autores, nombre, ciudad, fecha
		}

		List<Publicacion> publicaciones = new List<Publicacion>();

		TreeView treeView;

		public ViewInformes() : base("Informer - DIA Individual Nelson")
		{
			//CONFIG WINDOW
			SetDefaultSize(860, 640);
			SetPosition(WindowPosition.Center);
			//Free DRM icon.png : source @ http://findicons.com/icon/558115/free_bsd#
			SetIconFromFile("icon.png");
			//SYSTEM EVENTS
			DeleteEvent += OnDelete;
			//VERTICAL BOXs
			Box vbox = new VBox(false, 2);
			//ELEMENTS
			// = MENU =
			MenuBar barraMenu = new MenuBar();
			MenuItem test = new MenuItem("Test");
			MenuItem infMensual = new MenuItem("Mensual Miembros");
			MenuItem infAnualDep = new MenuItem("Anual Departamento");
			MenuItem infAnualMer = new MenuItem("Anual Meritos");
			MenuItem about = new MenuItem("Acerca de");
			MenuItem salir = new MenuItem("Salir");

			barraMenu.Append(test);
			barraMenu.Append(infMensual);
			barraMenu.Append(infAnualDep);
			barraMenu.Append(infAnualMer);
			barraMenu.Append(about);
			barraMenu.Append(salir);

			//TESTING LISTAR XML

			ScrolledWindow sw = new ScrolledWindow();
			sw.ShadowType = ShadowType.EtchedIn;
			sw.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
			ListStore conjuntoRecorridos = CreateModel();
			treeView = new TreeView(conjuntoRecorridos);
			treeView.RulesHint = true;
			sw.Add(treeView);
			AddColumns();


			//ADD TO VERTICAL BOX
			vbox.PackStart(barraMenu, false, false, 0); //MENU

			//ADD TO SHOW
			Add(vbox);

			//SHOW
			ShowAll();

			//EVENTS
			about.ButtonPressEvent += OnMenuAboutActivated;
			salir.ButtonPressEvent += OnMenuSalirActivated;
			test.ButtonPressEvent += OnMenuTestActivated;
		}

		void OnDelete(object obj, DeleteEventArgs args) { Application.Quit(); }

		void OnMenuSalirActivated(object sender, EventArgs e)
		{
			this.Destroy();
			Application.Quit();		}

		void OnMenuAboutActivated(object sender, EventArgs e)
		{
			AboutDialog about = new AboutDialog();
			about.SetIconFromFile("icon.png");
			about.ProgramName = "Informer";
			about.Version = "0.0.1";
			about.Copyright = "(c) Nelson Martinez";
			about.Comments = @"Informer is a simple solution proyect for DIA @ ESEI";
			about.Website = "informer.fake.web";
			try
			{
				//DRM FREE image found @ https://pixabay.com/es/portapapeles-de-papel-clip-negocio-2899533/
				about.Logo = new Gdk.Pixbuf("about.png", 200, 300);
			}
			catch
			{
				throw new Exception("Imagen 'about.png' no encontrada");
			}
			about.Run();
			about.Destroy();
		}

		void OnMenuTestActivated(object sender, EventArgs e) {
			this.publicaciones = XmlReader.read();
			foreach (Publicacion p in publicaciones) {
				Console.WriteLine(p);
			}
		}

		void AddColumns()
		{
			CellRendererText rendererText = new CellRendererText();
			TreeViewColumn column = new TreeViewColumn("Tipo publicacion", rendererText, "text", Column.tipo);
			column.SortColumnId = (int)Column.tipo;
			treeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("DOI", rendererText, "text", Column.DOI);
			column.SortColumnId = (int)Column.DOI;
			treeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Titulo", rendererText, "text", Column.titulo);
			column.SortColumnId = (int)Column.titulo;
			treeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Editorial", rendererText, "text", Column.editorial);
			column.SortColumnId = (int)Column.editorial;
			treeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Año publicacion", rendererText, "text", Column.anoPublicacion);
			column.SortColumnId = (int)Column.anoPublicacion;
			treeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Inicio", rendererText, "text", Column.pagInicio);
			column.SortColumnId = (int)Column.pagInicio;
			treeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Fin", rendererText, "text", Column.pagFin);
			column.SortColumnId = (int)Column.pagFin;
			treeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Autores", rendererText, "text", Column.autores);
			column.SortColumnId = (int)Column.autores;
			treeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Nombre Congreso", rendererText, "text", Column.nombre);
			column.SortColumnId = (int)Column.nombre;
			treeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Ciudad", rendererText, "text", Column.ciudad);
			column.SortColumnId = (int)Column.ciudad;
			treeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Fecha", rendererText, "text", Column.fecha);
			column.SortColumnId = (int)Column.fecha;
			treeView.AppendColumn(column);

		}

		ListStore CreateModel()
		{
			ListStore store = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string),
			                                typeof(string), typeof(string), typeof(string), typeof(string),
			                                typeof(string), typeof(string), typeof(string));

			foreach (Publicacion pub in this.publicaciones)
			{
				store.AppendValues(null);
			}

			return store;
		}

	}
}