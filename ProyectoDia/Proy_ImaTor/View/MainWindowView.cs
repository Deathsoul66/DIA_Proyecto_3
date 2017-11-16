using System;
namespace Proy_ImaTor.View
{
	using Gtk;
	public partial class MainWindow : Window
	{
		private Toolbar tlb;
		private TreeView mainTree = new TreeView();
		private ScrolledWindow scrWin = new ScrolledWindow();
		private HBox hbox = new HBox(false, 3);
		private Entry entInicio = new Entry();
		private Entry entDestino = new Entry();
		private Entry entKm = new Entry();
		private ListStore ls = new ListStore(typeof(int), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                                             typeof(int), typeof(int), typeof(string), typeof(string), typeof(string));
		VBox vbox = new VBox(false, 2);

		public MainWindow() : base("Publicacions")
		{
			SetDefaultSize(1024, 768);
			SetPosition(WindowPosition.Center);
			DeleteEvent += delegate { Application.Quit(); };

			vbox.PackStart(CrearToolbar(), false, false, 0);
			vbox.PackStart(CrearTabla(), true, true, 2);

			Add(vbox);
			ShowAll();
		}

		private Toolbar CrearToolbar()
		{
			tlb = new Toolbar();
			tlb.ToolbarStyle = ToolbarStyle.Icons;

			SeparatorToolItem sep = new SeparatorToolItem();
			ToolButton listaPublicacion = new ToolButton(Stock.Open);
			ToolButton nuevoPublicacion = new ToolButton(Stock.New);
            ToolButton guardar = new ToolButton(Stock.Save);
            ToolButton salir = new ToolButton(Stock.Close);

            listaPublicacion.Clicked += AbrirXML;

			tlb.Add(listaPublicacion);
			tlb.Add(nuevoPublicacion);
            tlb.Add(guardar);
			tlb.Add(sep);
			tlb.Add(salir);

			/*lista.Clicked += MostrarLista;
			nuevoPublicacion.Clicked += AñadirEntries;*/
			salir.Clicked += delegate { Application.Quit(); };

			return tlb;
		}

		private ScrolledWindow CrearTabla()
		{
			TreeViewColumn num = new TreeViewColumn { Title = "#", Alignment = 0.5f };
			num.Expand = true;
			CellRendererText cell = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
			num.PackStart(cell, true);
			num.AddAttribute(cell, "text", 0);

			TreeViewColumn publicacion = new TreeViewColumn { Title = "Publicacion", Alignment = 0.5f };
			publicacion.Expand = true;
			CellRendererText cell1 = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
			publicacion.PackStart(cell1, true);
			publicacion.AddAttribute(cell1, "text", 1);

            TreeViewColumn doi = new TreeViewColumn { Title = "DOI", Alignment = 0.5f };
			doi.Expand = true;
			CellRendererText cell2 = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
			doi.PackStart(cell2, true);
			doi.AddAttribute(cell2, "text", 2);

            TreeViewColumn titulo = new TreeViewColumn { Title = "Titulo", Alignment = 0.5f };
			titulo.Expand = true;
			CellRendererText cell3 = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
			titulo.PackStart(cell3, true);
			titulo.AddAttribute(cell3, "text", 3);

            TreeViewColumn editorial = new TreeViewColumn { Title = "Editorial", Alignment = 0.5f };
			editorial.Expand = true;
			CellRendererText cell4 = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
			editorial.PackStart(cell4, true);
			editorial.AddAttribute(cell4, "text", 4);

            TreeViewColumn anho = new TreeViewColumn { Title = "Año", Alignment = 0.5f };
			anho.Expand = true;
			CellRendererText cell5 = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
			anho.PackStart(cell5, true);
			anho.AddAttribute(cell5, "text", 5);

			TreeViewColumn pagIni = new TreeViewColumn { Title = "PagInicio", Alignment = 0.5f };
			pagIni.Expand = true;
			CellRendererText cell6 = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
			pagIni.PackStart(cell6, true);
			pagIni.AddAttribute(cell6, "text", 6);

			TreeViewColumn pagFin = new TreeViewColumn { Title = "PagFin", Alignment = 0.5f };
			pagFin.Expand = true;
			CellRendererText cell7 = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
			pagFin.PackStart(cell7, true);
			pagFin.AddAttribute(cell7, "text", 7);

			TreeViewColumn autores = new TreeViewColumn { Title = "Autores", Alignment = 0.5f };
			autores.Expand = true;
			CellRendererText cell8 = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
			autores.PackStart(cell8, true);
			autores.AddAttribute(cell8, "text", 8);

			TreeViewColumn nombre = new TreeViewColumn { Title = "Nombre", Alignment = 0.5f };
			nombre.Expand = true;
			CellRendererText cell9 = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
			nombre.PackStart(cell9, true);
			nombre.AddAttribute(cell9, "text", 9);

			TreeViewColumn ciudad = new TreeViewColumn { Title = "Ciudad", Alignment = 0.5f };
			ciudad.Expand = true;
			CellRendererText cell10 = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
			ciudad.PackStart(cell10, true);
			ciudad.AddAttribute(cell10, "text", 10);

			TreeViewColumn fecha = new TreeViewColumn { Title = "Fecha", Alignment = 0.5f };
			fecha.Expand = true;
			CellRendererText cell11 = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
			fecha.PackStart(cell11, true);
			fecha.AddAttribute(cell11, "text", 11);

			//DatosXML();

			mainTree.Model = ls;

			mainTree.AppendColumn(num);
            mainTree.AppendColumn(publicacion);
			mainTree.AppendColumn(doi);
			mainTree.AppendColumn(titulo);
			mainTree.AppendColumn(editorial);
			mainTree.AppendColumn(anho);
            mainTree.AppendColumn(pagIni);
            mainTree.AppendColumn(pagFin);
            mainTree.AppendColumn(autores);
            mainTree.AppendColumn(nombre);
            mainTree.AppendColumn(ciudad);
            mainTree.AppendColumn(fecha);

			scrWin.Add(mainTree);

			return scrWin;
		}

		private HBox CrearBotones()
		{
			Label lblInicio = new Label("Ciudad Inicio");
			entInicio.MaxLength = 50;

			Label lblDestino = new Label("Ciudad Destino");
			entDestino.MaxLength = 50;

			Label lblKm = new Label("Distancia");
			entKm.MaxLength = 5;

			Button btnAñadir = new Button("Añadir Publicacion");

			hbox.PackStart(lblInicio, false, false, 5);
			hbox.PackStart(entInicio, false, false, 0);
			hbox.PackStart(lblDestino, false, false, 0);
			hbox.PackStart(entDestino, false, false, 0);
			hbox.PackStart(lblKm, false, false, 0);
			hbox.PackStart(entKm, false, false, 0);
			hbox.PackEnd(btnAñadir, false, false, 5);

			//btnAñadir.Clicked += EventoBotonAñadir;

			return hbox;
		}
	}
}
