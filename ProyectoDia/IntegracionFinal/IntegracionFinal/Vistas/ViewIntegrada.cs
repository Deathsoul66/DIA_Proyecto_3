using System;
using System.Collections.Generic;
using Gtk;

namespace IntegracionFinal
{
    public partial class ViewIntegrada : Window
    {

        //GLOBAL SECTION
        Box vbox;
        HBox hbox;

        private ListaPublicacion listaP;
        private List<Miembro> listaM;

        public ViewIntegrada() : base("Integracion - DIA")
        {
            //CONFIG WINDOW
            SetDefaultSize(1200, 800);
            SetPosition(WindowPosition.Center);
            //Free DRM icon.png : source @ http://findicons.com/icon/558115/free_bsd#
            SetIconFromFile(".\\imagenes\\icon.png");
            //SYSTEM EVENTS
            DeleteEvent += OnDelete;
            //VERTICAL BOXs
            vbox = new VBox(false, 2);
            //HORIZONTAL BOXs
            hbox = new HBox(false, 2);
            //READ PUBLICACIONES
            this.listaP = XmlReader.leerPublicaciones("Test.xml");
            //READ MIEMBROS
            this.listaM = XmlReader.leerMiembros("TestMiembros.xml");

            //VIEW ELEMENTS

            // = MENU =
            ///////////****************************************************************////////////// Menu_I_Section
            MenuBar barraMenu = new MenuBar();
            MenuItem test = new MenuItem("Publicaciones");
            MenuItem infMensual = new MenuItem("Miembros");
            MenuItem infAnualDep = new MenuItem("Informes");
            MenuItem about = new MenuItem("Acerca de");
            MenuItem salir = new MenuItem("Salir");

            barraMenu.Append(test);
            barraMenu.Append(infMensual);
            barraMenu.Append(infAnualDep);
            barraMenu.Append(about);
            barraMenu.Append(salir);
            ///////////****************************************************************////////////// Menu_E_Section
            ///////////****************************************************************////////////// Filtro_I_Section
            //VIEW FILTRO

            Autor = "";
            Anho = "";
            Tipo = "";

            HBox pnlAutor = new HBox(false, 5);
            HBox pnlAnho = new HBox(false, 5);
            HBox pnlTipo = new HBox(false, 5);
            VBox pnlButton1 = new VBox(false, 5);
            VBox pnlButton2 = new VBox(false, 5);
            VBox pnlButton3 = new VBox(false, 5);

            HBox pnlBuscador = new HBox(false, 5);

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

            Button full = new Button("Mostrar todo");
            full.Clicked += OnClickFull;
            full.SetSizeRequest(90, 20);
            pnlButton2.PackStart(full, false, false, 5);

            Button anhadir = new Button("Añadir");
            anhadir.Clicked += AñadirPublicacion;
            anhadir.SetSizeRequest(50, 20);
            pnlButton3.PackStart(anhadir, false, false, 5);

            pnlBuscador.PackStart(pnlAutor, false, false, 5);
            pnlBuscador.PackStart(pnlAnho, false, false, 5);
            pnlBuscador.PackStart(pnlTipo, false, false, 5);
            pnlBuscador.PackStart(pnlButton1, false, false, 5);
            pnlBuscador.PackStart(pnlButton2, false, false, 5);
            pnlBuscador.PackStart(pnlButton3, false, false, 5);


            //VBox vbox = new VBox(false, 8);

            this.sw = new ScrolledWindow();
            this.sw.ShadowType = ShadowType.EtchedIn;
            this.sw.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
           

            store = CreateModel();

            this.treeView = new TreeView(store);
            this.treeView.RulesHint = true;
            //this.treeView.RowActivated += OnRowActivated;
            this.treeView.RowActivated += UpdatePublicacion;
            this.sw.Add(treeView);

            AddColumns();

            //statusbar = new Statusbar();

            //vbox.PackStart(statusbar, false, false, 0);

            ///////////****************************************************************////////////// Filtro_E_Section
            //ADD ELEMENTS TO HORIZONTAL BOX
            //TO-DO

            ///////////****************************************************************////////////// Miembros_I_Section
            hbox.PackStart(this.BuildTable(),false, false, 2);
            ///////////****************************************************************////////////// Miembros_E_Section
            ///////////****************************************************************////////////// Informes_I_Section
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

            hbox.PackStart(vistaMerDepartamento);
            ///////////****************************************************************////////////// Informes_E_Section

            //ADD ELEMENTS TO VERTICAL BOX
            vbox.PackStart(barraMenu, false, false, 0); //MENU
            vbox.PackStart(pnlBuscador, false, false, 5);//FILTRO (MENU)
            vbox.PackStart(this.sw, true, true, 0); //FILTRO (VIEW)
            vbox.PackStart(hbox, false, false, 0); //HBOX MIEMBROS & INFORMER

            //ADD TO SHOW
            Add(vbox);

            //SHOW INITIAL VIEW
            ShowAll();

            //EVENTS
            about.ButtonPressEvent += OnMenuAboutActivated;
            salir.ButtonPressEvent += OnMenuSalirActivated;
            //test.ButtonPressEvent += OnMenuTestActivated;
            //infMensual.ButtonPressEvent += OnMenuInfMerMiembroMensualActivated;
            btnConsultarMerDepartamento.Clicked += onBtnConsultMerDepClicked;
        }
    }
}
