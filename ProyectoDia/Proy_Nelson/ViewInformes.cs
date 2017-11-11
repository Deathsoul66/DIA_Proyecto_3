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

		Box vbox;
		ScrolledWindow vistaLista;
		VBox vistaMerMiembroMeses;
		VBox vistaMerDepartamento;
		VBox vistaMerAnio;
		ComboBox comboMiembros;
		Entry txtEntryAnio;
		Entry txtEntryAnioDep;
		Entry txtEntryAnioTxt;
		Image plot;

		List<Publicacion> publicaciones = XmlReader.readPublicaciones("Test.xml");
		List<Miembro> miembros = XmlReader.readMiembros("TestMiembros.xml");

		TreeView treeView;

		public ViewInformes() : base("Informer - DIA Individual Nelson")
		{
			//CONFIG WINDOW
			SetDefaultSize(1150, 640);
			SetPosition(WindowPosition.Center);
			//Free DRM icon.png : source @ http://findicons.com/icon/558115/free_bsd#
			SetIconFromFile("icon.png");
			//SYSTEM EVENTS
			DeleteEvent += OnDelete;
			//VERTICAL BOXs
			vbox = new VBox(false, 2);
			//ELEMENTS
			// = MENU =
			MenuBar barraMenu = new MenuBar();
			MenuItem test = new MenuItem("Listar");
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

			vistaLista = new ScrolledWindow();
			vistaLista.ShadowType = ShadowType.EtchedIn;
			vistaLista.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
			ListStore conjuntoRecorridos = CreateModel();
			treeView = new TreeView(conjuntoRecorridos);
			treeView.RulesHint = true;
			vistaLista.Add(treeView);
			AddColumns();

			//Vista: Méritos de miembro para los meses del año
			vistaMerMiembroMeses = new VBox();
			HBox menuMerMiembroMeses = new HBox();
			HBox selectoresMerMiembro = new HBox();
			Label lbMiembros = new Label("Miembros");
			Label lbAnio = new Label("Año");
			Button btnConsultarMMM = new Button("Consultar");
			txtEntryAnio = new Entry();

			string[] mmbrs = new string[this.miembros.Count];
			int i = 0;
			foreach (Miembro m in this.miembros)
			{
				mmbrs[i] = m.apellidos + ", " + m.nombre;
				i++;
			}			comboMiembros = new ComboBox(mmbrs);

			selectoresMerMiembro.PackStart(lbMiembros, false, false, 5);
			selectoresMerMiembro.PackStart(comboMiembros, false, false, 2);
			selectoresMerMiembro.PackStart(lbAnio, false, false, 5);
			selectoresMerMiembro.PackStart(txtEntryAnio, false, false, 2);
			menuMerMiembroMeses.PackStart(selectoresMerMiembro, false, false, 5);
			menuMerMiembroMeses.PackStart(btnConsultarMMM, false, false, 5);
			vistaMerMiembroMeses.PackStart(menuMerMiembroMeses, false, false, 0);

			//Vista para el departamento
			vistaMerDepartamento = new VBox();
			HBox menuMerDepartamento = new HBox();
			HBox selectorMerDepartamento = new HBox();
			Button btnConsultarMerDepartamento = new Button("Consultar");
			txtEntryAnioDep = new Entry();

			selectorMerDepartamento.PackStart(lbAnio, false, false, 20);
			selectorMerDepartamento.PackStart(txtEntryAnioDep, false, false, 5);
			menuMerDepartamento.PackStart(selectorMerDepartamento, false, false, 10);
			menuMerDepartamento.PackStart(btnConsultarMerDepartamento, false, false, 5);
			vistaMerDepartamento.PackStart(menuMerDepartamento, false, false, 0);

			//Vista de todos los meritos por año (por escrito)
			vistaMerAnio = new VBox();
			HBox menuMerAnio = new HBox();
			HBox selectorMerAnio = new HBox();
			Button btnConsultarMerAnio = new Button("Consultar");
			txtEntryAnioTxt = new Entry();

			selectorMerAnio.PackStart(lbAnio, false, false, 20);
			selectorMerAnio.PackStart(txtEntryAnioTxt, false, false, 5);
			menuMerAnio.PackStart(selectorMerAnio, false, false, 10);
			menuMerAnio.PackStart(btnConsultarMerAnio, false, false, 5);
			vistaMerAnio.PackStart(menuMerAnio, false, false, 0);

			//ADD TO VERTICAL BOX
			vbox.PackStart(barraMenu, false, false, 0); //MENU
			vbox.PackStart(vistaLista, true, true, 0); //LISTAR TEST
			vbox.PackStart(vistaMerMiembroMeses, true, true, 0); //MERITOS POR MIEMBRO
			vbox.PackStart(vistaMerDepartamento, true, true, 0); //MERITOS POR DEPARTAMENTO
			vbox.PackStart(vistaMerAnio, true, true, 0);

			//ADD TO SHOW
			Add(vbox);

			//SHOW INITIAL VIEW
			ShowAll();
			vistaLista.Hide();
			vistaMerMiembroMeses.Hide();
			vistaMerDepartamento.Hide();
			vistaMerAnio.Hide();

			//EVENTS
			about.ButtonPressEvent += OnMenuAboutActivated;
			salir.ButtonPressEvent += OnMenuSalirActivated;
			test.ButtonPressEvent += OnMenuTestActivated;
			infMensual.ButtonPressEvent += OnMenuInfMerMiembroMensualActivated;
			infAnualDep.ButtonPressEvent += onInfAnualDepActivated;
			infAnualMer.ButtonPressEvent += oninfAnualMerActivated;
			btnConsultarMMM.Clicked += onBtnConsultarMerMiembroMesesClicked;
			btnConsultarMerDepartamento.Clicked += onBtnConsultMerDepClicked;
			btnConsultarMerAnio.Clicked += btnConsultarMerAnioClicked;
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
			about.Version = "1.0.0";
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

		void OnMenuTestActivated(object sender, EventArgs e)
		{
			foreach (Publicacion p in publicaciones)
			{
				Console.WriteLine("PUBLICACION => Tipo:" + p.getTipo() + "\n"
								  + "DOI:" + p.DOI + "\n"
								  + "TITULO:" + p.Titulo + "\n"
								  + "EDITORIAL:" + p.Editorial + "\n"
								  + "Fecha pub:" + p.FechaPublicacion.Date + "\n"
								  + "P.Inicio:" + p.PagInicio + "\n"
								  + "P.Fin:" + p.PagFin);
				if (p is Articulo)
				{
					foreach (String a in p.Autores)
					{
						Console.WriteLine("Autor ARTICULO: " + a);
					}

				}
				if (p is Libro)
				{
					foreach (String a in p.Autores)
					{
						Console.WriteLine("Autor LIBRO: " + a);
					}
				}
				if (p is Congreso)
				{
					foreach (String a in p.Autores)
					{
						Console.WriteLine("Autor CONGRESO: " + a);
					}
					Console.WriteLine("Nombre congreso: " + ((Congreso)p).nombre);
					Console.WriteLine("Ciudad congreso: " + ((Congreso)p).ciudad);
					Console.WriteLine("Fecha congreso: " + ((Congreso)p).fecha);
				}
				Console.WriteLine("\n====  TEST AUTORES ====\n");
				Console.WriteLine(p.autoresToString() + "\n");
			}
			vistaRecorridos();
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
				store.AppendValues(pub.getTipo(), pub.DOI, pub.Titulo, pub.Editorial,
								   pub.FechaPublicacion.ToString("dd/MM/yyyy"), pub.PagInicio, pub.PagFin, pub.autoresToString(),
								   pub.getNombre(), pub.getCiudad(), pub.getFecha());
			}

			return store;
		}


		void OnMenuInfMerMiembroMensualActivated(object o, ButtonPressEventArgs args)
		{
			clearOnViewChanged();
			vistaMerMiembroMeses.ShowAll();
		}
		void vistaRecorridos()
		{
			clearOnViewChanged();
			vistaLista.Remove(treeView);
			treeView = new TreeView(CreateModel());
			treeView.RulesHint = true;
			AddColumns();
			vistaLista.Add(treeView);
			vistaLista.ShowAll();
		}

		void clearOnViewChanged()
		{
			vistaMerMiembroMeses.Visible = false;
			vistaLista.Visible = false;
			vistaMerDepartamento.Visible = false;
			vistaMerAnio.Visible = false;
			vistaMerMiembroMeses.Remove(plot);
			vistaMerDepartamento.Remove(plot);
		}

		void onBtnConsultarMerMiembroMesesClicked(object sender, EventArgs e)
		{

			if (comboMiembros.ActiveText != "" && txtEntryAnio.Text != "")
			{
				try
				{
					clearOnViewChanged();
					vistaMerMiembroMeses.Visible = true;
					Miembro mie = new Miembro();
					foreach (Miembro m in miembros)
					{
						if (m.checkMiembro(comboMiembros.ActiveText))
						{
							mie = m;
							break;
						}
					}
					if (mie.nombre != "")
					{
						int anio = Convert.ToInt32(txtEntryAnio.Text.Trim());
						//plot = Grafico.GraficoBarrasExample();
						//plot = Grafico.GraficoPieExample();
						plot = Grafico.merMiembroMes(comboMiembros.ActiveText, meritosCientificos(mie, anio), anio);
						vistaMerMiembroMeses.PackStart(plot, false, false, 0);
						vistaMerMiembroMeses.ShowAll();
					}
				}
				catch
				{
					MessageDialog number = new MessageDialog(this,
						DialogFlags.DestroyWithParent, MessageType.Error,
						ButtonsType.Ok, "Algo ha fallado, no se ha podido generar el gráfico");
					number.Run();
					number.Destroy();
				}
			}
			else
			{
				MessageDialog md = new MessageDialog(this,
						DialogFlags.DestroyWithParent, MessageType.Warning,
						ButtonsType.Ok, "Los campos no pueden estar vacíos");
				md.Run();
				md.Destroy();
			}
		}

		//Número de méritos científicos publicados por cada miembro, para los meses del año
		int[] meritosCientificos(Miembro m, int year)
		{
			int[] meritos = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
			foreach (Publicacion p in publicaciones)
			{
				if (p.FechaPublicacion.Year == year)
				{
					foreach (String autor in p.Autores)
					{
						if (autor == m.nombre)
						{
							meritos[Convert.ToInt32(p.FechaPublicacion.Month) - 1] += 1;
						}
					}
				}
			}
			return meritos;
		}

		void onInfAnualDepActivated(object o, ButtonPressEventArgs args)
		{
			clearOnViewChanged();
			vistaMerDepartamento.ShowAll();
		}

		//Numero de méritos cientificos pubicados para el año seleccionado para el departamento entero
		int[] meritosCientificosDepartamento(int year)
		{
			int[] meritos = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
			foreach (Miembro miembro in miembros)
			{
				foreach (Publicacion p in publicaciones)
				{
					if (p.FechaPublicacion.Year == year)
					{
						foreach (String autor in p.Autores)
						{
							if (autor == miembro.nombre)
							{
								meritos[Convert.ToInt32(p.FechaPublicacion.Month) - 1] += 1;
							}
						}
					}
				}
			}
			return meritos;
		}

		void onBtnConsultMerDepClicked(object sender, EventArgs e)
		{
			if (txtEntryAnioDep.Text != "")
			{
				try
				{
					clearOnViewChanged();
					vistaMerDepartamento.Visible = true;
					int anio = Convert.ToInt32(txtEntryAnioDep.Text.Trim());
					plot = Grafico.merMiembroMes("el departamento", meritosCientificosDepartamento(anio), anio);
					vistaMerDepartamento.PackStart(plot, false, false, 0);
					vistaMerDepartamento.ShowAll();

				}
				catch
				{
					MessageDialog number = new MessageDialog(this,
						DialogFlags.DestroyWithParent, MessageType.Error,
						ButtonsType.Ok, "Algo ha fallado, no se ha podido generar el gráfico");
					number.Run();
					number.Destroy();
				}
			}
			else
			{
				MessageDialog md = new MessageDialog(this,
						DialogFlags.DestroyWithParent, MessageType.Warning,
						ButtonsType.Ok, "Introduce un año en formato numérico");
				md.Run();
				md.Destroy();
			}
		}

		void oninfAnualMerActivated(object o, ButtonPressEventArgs args)
		{
			clearOnViewChanged();
			vistaMerAnio.ShowAll();
		}

		void btnConsultarMerAnioClicked(object sender, EventArgs e)
		{
			if (txtEntryAnioTxt.Text != "")
			{
				try
				{
					clearOnViewChanged();
					vistaMerAnio.Remove(treeView);
					treeView = new TreeView(CreateFilteredModel(Convert.ToInt32(txtEntryAnioTxt.Text)));
					treeView.RulesHint = true;
					AddColumns();
					vistaMerAnio.PackStart(treeView);
					vistaMerAnio.ShowAll();

				}
				catch
				{
					MessageDialog number = new MessageDialog(this,
						DialogFlags.DestroyWithParent, MessageType.Error,
						ButtonsType.Ok, "Algo ha fallado, no se ha podido generar el gráfico");
					number.Run();
					number.Destroy();
				}
			}
			else
			{
				MessageDialog md = new MessageDialog(this,
						DialogFlags.DestroyWithParent, MessageType.Warning,
						ButtonsType.Ok, "Introduce un año en formato numérico");
				md.Run();
				md.Destroy();
			}
		}

		ListStore CreateFilteredModel(int anio)
		{
			ListStore store = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string),
											typeof(string), typeof(string), typeof(string), typeof(string),
											typeof(string), typeof(string), typeof(string));

			foreach (Publicacion pub in this.publicaciones)
			{
				if (pub.FechaPublicacion.Year == anio)
				{
					store.AppendValues(pub.getTipo(), pub.DOI, pub.Titulo, pub.Editorial,
									   pub.FechaPublicacion.ToString("dd/MM/yyyy"), pub.PagInicio, pub.PagFin, pub.autoresToString(),
									   pub.getNombre(), pub.getCiudad(), pub.getFecha());
				}
			}

			return store;
		}
	}
}