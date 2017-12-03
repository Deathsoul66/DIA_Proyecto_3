using Gtk;
using System.Collections.Generic;

namespace IntegracionFinal
{
	public partial class ViewInformes : Window
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

		List<Publicacion> publicaciones = XmlReader.leerPublicaciones("Test.xml").listPub;
		List<Miembro> miembros = XmlReader.leerMiembros("TestMiembros.xml");

		TreeView treeView;

		public ViewInformes() : base("Informer - DIA Individual Nelson")
		{
			//CONFIG WINDOW
			SetDefaultSize(1150, 640);
			SetPosition(WindowPosition.Center);
			//Free DRM icon.png : source @ http://findicons.com/icon/558115/free_bsd#
			SetIconFromFile(".\\imagenes\\icon.png");
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
			}
			comboMiembros = new ComboBox(mmbrs);

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
	}
}