using System;
namespace IntegracionFinal
{
	using Gtk;
	public partial class MainWindowIMT : Window
	{
		private Toolbar tlb;
		private TreeView mainTree = new TreeView();
		private ScrolledWindow scrWin = new ScrolledWindow();
		private Entry entTipo = new Entry();
        private Entry entDOI = new Entry();
        private Entry entTitulo = new Entry();
        private Entry entEditorial = new Entry();
        private Calendar entAnho = new Calendar();
        private Entry entPIni = new Entry();
        private Entry entPFin = new Entry();
        private Entry entNombre = new Entry();
        private Entry entCiudad = new Entry();
        //private Entry entFecha = new Entry();
        private Calendar entFecha = new Calendar();
        private Entry entAutores = new Entry();
		private ListStore ls = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                                             typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),typeof(string));
		

		public MainWindowIMT() : base("Publicaciones")
		{
			SetDefaultSize(1024, 768);
			SetPosition(WindowPosition.Center);
			DeleteEvent += delegate { Application.Quit(); };

            VBox vbox = new VBox(false, 2);
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
            guardar.Clicked += GuardarXML;
            nuevoPublicacion.Clicked += AñadirPublicacion;

			tlb.Add(listaPublicacion);
			tlb.Add(nuevoPublicacion);
            tlb.Add(guardar);
			tlb.Add(sep);
			tlb.Add(salir);

			/*lista.Clicked += MostrarLista;
			*/
			salir.Clicked += delegate { Application.Quit(); };

			return tlb;
		}

		private ScrolledWindow CrearTabla()
		{
			TreeViewColumn publicacion = new TreeViewColumn { Title = "Publicacion", Alignment = 0.5f };
			publicacion.Expand = true;
			CellRendererText cell1 = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
			publicacion.PackStart(cell1, true);
			publicacion.AddAttribute(cell1, "text", 0);

            TreeViewColumn doi = new TreeViewColumn { Title = "DOI", Alignment = 0.5f };
			doi.Expand = true;
			CellRendererText cell2 = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
			doi.PackStart(cell2, true);
			doi.AddAttribute(cell2, "text", 1);

            TreeViewColumn titulo = new TreeViewColumn { Title = "Titulo", Alignment = 0.5f };
			titulo.Expand = true;
			CellRendererText cell3 = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
			titulo.PackStart(cell3, true);
			titulo.AddAttribute(cell3, "text", 2);

            TreeViewColumn editorial = new TreeViewColumn { Title = "Editorial", Alignment = 0.5f };
			editorial.Expand = true;
			CellRendererText cell4 = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
			editorial.PackStart(cell4, true);
			editorial.AddAttribute(cell4, "text", 3);

            TreeViewColumn anho = new TreeViewColumn { Title = "Fecha publicacion", Alignment = 0.5f };
			anho.Expand = true;
			CellRendererText cell5 = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
			anho.PackStart(cell5, true);
			anho.AddAttribute(cell5, "text", 4);

			TreeViewColumn pagIni = new TreeViewColumn { Title = "PagInicio", Alignment = 0.5f };
			pagIni.Expand = true;
			CellRendererText cell6 = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
			pagIni.PackStart(cell6, true);
			pagIni.AddAttribute(cell6, "text", 5);

			TreeViewColumn pagFin = new TreeViewColumn { Title = "PagFin", Alignment = 0.5f };
			pagFin.Expand = true;
			CellRendererText cell7 = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
			pagFin.PackStart(cell7, true);
			pagFin.AddAttribute(cell7, "text", 6);

			TreeViewColumn autores = new TreeViewColumn { Title = "Autores", Alignment = 0.5f };
			autores.Expand = true;
			CellRendererText cell8 = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
			autores.PackStart(cell8, true);
			autores.AddAttribute(cell8, "text", 7);

			TreeViewColumn nombre = new TreeViewColumn { Title = "Nombre", Alignment = 0.5f };
			nombre.Expand = true;
			CellRendererText cell9 = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
			nombre.PackStart(cell9, true);
			nombre.AddAttribute(cell9, "text", 8);

			TreeViewColumn ciudad = new TreeViewColumn { Title = "Ciudad", Alignment = 0.5f };
			ciudad.Expand = true;
			CellRendererText cell10 = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
			ciudad.PackStart(cell10, true);
			ciudad.AddAttribute(cell10, "text", 9);

			TreeViewColumn fecha = new TreeViewColumn { Title = "Fecha", Alignment = 0.5f };
			fecha.Expand = true;
			CellRendererText cell11 = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
			fecha.PackStart(cell11, true);
			fecha.AddAttribute(cell11, "text", 10);

			//DatosXML();

			mainTree.Model = ls;

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
            mainTree.RowActivated += UpdateForm;
			scrWin.Add(mainTree);

			return scrWin;
		}

        private ComboBox TipoPublicacion(){
            return new ComboBox(new string[] { "Articulo", "Congreso", "Libro" }); ;
        }

        private Alignment CrearBotones(string tipo)
		{
            VBox left = new VBox(false, 3);
            VBox right = new VBox(false, 3);
            HBox hbox = new HBox(false, 5);
			VBox left2 = new VBox(false, 3);
			VBox right2 = new VBox(false, 3);

            Label lblTipo = new Label("Tipo de Publicacion: ");
            lblTipo.Xalign = 1;
            entTipo.Text = tipo;
            entTipo.CanFocus = false;
            left.PackStart(lblTipo);
            right.PackStart(entTipo);

            Label lblDOI = new Label("DOI: ");
            lblDOI.Xalign = 1;
			left.PackStart(lblDOI);
            right.PackStart(entDOI);

			Label lblTitulo = new Label("Titulo: ");
			lblTitulo.Xalign = 1;
			left.PackStart(lblTitulo);
			right.PackStart(entTitulo);

			Label lblEditorial = new Label("Editorial: ");
			lblEditorial.Xalign = 1;
			left.PackStart(lblEditorial);
			right.PackStart(entEditorial);

			Label lblAnho = new Label("Fecha publicacion: ");
			lblAnho.Xalign = 1;
			left.PackStart(lblAnho);
			right.PackStart(entAnho);

			Label lblPI = new Label("Pagina Inicio: ");
			lblPI.Xalign = 1;
			left.PackStart(lblPI);
			right.PackStart(entPIni);
            entPIni.TextInserted += OnlyNumber;

			Label lblPF = new Label("Pagina Fin: ");
			lblPF.Xalign = 1;
			left.PackStart(lblPF);
            right.PackStart(entPFin);
            entPFin.TextInserted += OnlyNumber;

			Label lblAutores = new Label("Autores: ");
			lblAutores.Xalign = 1;
			left.PackStart(lblAutores);
			right.PackStart(entAutores);

            if(tipo.Equals("Congreso")){
				Label lblNombre = new Label("Nombre Congreso: ");
				lblNombre.Xalign = 1;
				left.PackStart(lblNombre);
                right.PackStart(entNombre);

				Label lblCiudad = new Label("Ciudad: ");
				lblCiudad.Xalign = 1;
				left.PackStart(lblCiudad);
				right.PackStart(entCiudad);

				Label lblFecha = new Label("Fecha Congreso: ");
				lblFecha.Xalign = 1;
				left2.PackStart(lblFecha);
				right2.PackStart(entFecha);
               

			}
			

			hbox.Add(left);
			hbox.Add(right);
			hbox.Add(left2);
			hbox.Add(right2);

			Alignment alignment = new Alignment(1, 0, 1, 0);
			alignment.SetPadding(10, 10, 10, 10);
            alignment.Add(hbox);

			return alignment;
		}
	}


}
