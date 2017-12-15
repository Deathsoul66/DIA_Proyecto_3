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

        MenuItem subMenuEditMiembro;
        MenuItem subMenuDeleteMiembro;
        MenuItem subMenuEditPublicacion;
        MenuItem subMenuDeletePublicacion;

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
            this.listaP = XmlReader.leerPublicaciones(PUBLICACIONES);
            //READ MIEMBROS
            this.listaM = XmlReader.leerMiembros(MIEMBROS);

            //VIEW ELEMENTS

            // = MENU =
            ///////////****************************************************************////////////// Menu_I_Section
            MenuBar barraMenu = new MenuBar();
            MenuItem menuPublicaciones = new MenuItem("Publicaciones");
            MenuItem menuMiembros = new MenuItem("Miembros");
            MenuItem menuInformes = new MenuItem("Informes");
            MenuItem menuAbout = new MenuItem("Acerca de");
            MenuItem menuSalir = new MenuItem("Salir");

            ///////////*********************************////////////// SubMenus_I_Section

            ////////// - Publicaciones -
            //Creamos el submenu
            Menu pubsSubMenu = new Menu();
            //Elementos para el submenu
            MenuItem subMenuAddPublicacion = new MenuItem("Añadir publicación");
            subMenuEditPublicacion = new MenuItem("Editar publicación");
            subMenuDeletePublicacion = new MenuItem("Eliminar publicación");
            //Añadimos estos elementos al submenu
            pubsSubMenu.Append(subMenuAddPublicacion);
            //pubsSubMenu.Append(subMenuEditPublicacion);
            //pubsSubMenu.Append(subMenuDeletePublicacion);
            //Añadimos el submenú al "Button" del menu principal
            menuPublicaciones.Submenu = pubsSubMenu;
            
            ////////// - Miembros -
            //Creamos el submenu
            Menu miembSubMenu = new Menu();
            //Elementos para el submenu
            MenuItem subMenuAddMiembro = new MenuItem("Añadir miembro");
            subMenuEditMiembro = new MenuItem("Editar miembro");
            subMenuDeleteMiembro = new MenuItem("Eliminar miembro");
            //Añadimos estos elementos al submenu
            miembSubMenu.Append(subMenuAddMiembro);
            //miembSubMenu.Append(subMenuEditMiembro);
            //miembSubMenu.Append(subMenuDeleteMiembro);
            //Añadimos el submenú al "Button" del menu principal
            menuMiembros.Submenu = miembSubMenu;
            
            ////////// - Informes -
            //Creamos el submenu
            Menu informesSubMenu = new Menu();
            //Elementos para el submenu
            MenuItem subMenuMeritosDepartamento = new MenuItem("Ver méritos departamento");
            MenuItem subMenuMeritosMiembro = new MenuItem("Ver méritos miembro");
            //Añadimos estos elementos al submenu
            informesSubMenu.Append(subMenuMeritosDepartamento);
            informesSubMenu.Append(subMenuMeritosMiembro);
            //Añadimos el submenú al "Button" del menu principal
            menuInformes.Submenu = informesSubMenu;
            
            ///////////**********************************////////////// SubMenus_E_Section

            barraMenu.Append(menuPublicaciones);
            barraMenu.Append(menuMiembros);
            barraMenu.Append(menuInformes);
            barraMenu.Append(menuAbout);
            barraMenu.Append(menuSalir);
            ///////////****************************************************************////////////// Menu_E_Section
           
            ///////////****************************************************************////////////// Filtro_I_Section
            Autor = "";
            Anho = "";
            Tipo = "";

            HBox pnlAutor = new HBox(false, 5);
            HBox pnlAnho = new HBox(false, 5);
            HBox pnlTipo = new HBox(false, 5);
            VBox pnlButton1 = new VBox(false, 5);
            VBox pnlButton2 = new VBox(false, 5);
            VBox pnlButton3 = new VBox(false, 5);
            VBox pnlButton4 = new VBox(false, 5);

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

            Button anhadir = new Button("Añadir Publicacion");
            anhadir.Clicked += AñadirPublicacion;
            anhadir.SetSizeRequest(110, 20);

            Button anhadir2 = new Button("Añadir Miembro");
            anhadir2.Clicked += AñadirMiembro;
            anhadir2.SetSizeRequest(100, 20);

            pnlBuscador.PackStart(pnlAutor, false, false, 5);
            pnlBuscador.PackStart(pnlAnho, false, false, 5);
            pnlBuscador.PackStart(pnlTipo, false, false, 5);
            pnlBuscador.PackStart(pnlButton1, false, false, 5);
            pnlBuscador.PackStart(pnlButton2, false, false, 5);
            pnlBuscador.PackStart(pnlButton3, false, false, 5);
            pnlBuscador.PackStart(pnlButton4, false, false, 5);

            this.sw = new ScrolledWindow();
            this.sw.ShadowType = ShadowType.EtchedIn;
            this.sw.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
           

            store = CreateModel();

            this.treeView = new TreeView(store);
            //this.treeView.RulesHint = true; //Para el sombreado de lineas pares
            this.treeView.Selection.Changed += MenuPublicacionesActivate;
            this.treeView.RowActivated += UpdatePublicacion;
            this.sw.Add(treeView);

            AddColumns();

            ///////////****************************************************************////////////// Filtro_E_Section
            ///////////****************************************************************////////////// Miembros_I_Section
            hbox.PackStart(this.BuildTable(),false, false, 2);
            ///////////****************************************************************////////////// Miembros_E_Section
            ///////////****************************************************************////////////// Informes_I_Section
            vistaMerDepartamento = new VBox();
            HBox menuMerDepartamento = new HBox();
            HBox selectorMerDepartamento = new HBox();
            Button btnConsultarMerDepartamento = new Button("Consultar");
            txtEntryAnioDep = new ComboBoxEntry(this.getAniosPublicaciones()); 
            txtEntryAnioDep.Active = 0; //Seleccionado el último año
            
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

            //Inicializar grafica
            onBtnConsultMerDepClicked(this, null);

            //SHOW INITIAL VIEW
            ShowAll();

            //EVENTS
            menuAbout.ButtonPressEvent += OnMenuAboutActivated;
            menuSalir.ButtonPressEvent += OnMenuSalirActivated;
            btnConsultarMerDepartamento.Clicked += onBtnConsultMerDepClicked;
            subMenuAddPublicacion.ButtonPressEvent += AñadirPublicacion;
            subMenuAddMiembro.ButtonPressEvent += AñadirMiembro;
            //subMenuDeleteMiembro.Sensitive = false;
            //subMenuDeletePublicacion.Sensitive = false;
            //subMenuEditMiembro.Sensitive = false;
            //subMenuEditPublicacion.Sensitive = false;
            //subMenuEditMiembro.ButtonPressEvent += EditarMiembro;
            subMenuMeritosDepartamento.ButtonPressEvent += btnConsultarMerAnioClicked;
            subMenuMeritosMiembro.ButtonPressEvent += onBtnConsultarMerMiembroMesesClicked;
        }
    }
}
