using System;


namespace IntegracionFinal
{
    using Gtk;
    using System.Collections.Generic;
    public partial class MainWindow : Window
    {

        private Entry eDNI;
        private Entry eNombre;
        private Entry eApellidos;
        private Entry eTelefono;
        private Entry eEmail;
        private Entry eDireccion;

        private Entry eUpdateDNI;
        private Entry eUpdateNombre;
        private Entry eUpdateApellidos;
        private Entry eUpdateTelefono;
        private Entry eUpdateEmail;
        private Entry eUpdateDireccion;

        private List<Label> lbllist = new List<Label>();
        private Button btnUpdate;
        private string fileroute;
        private TreeView tvDatos = new TreeView();
        private ListStore ls;

        private int WIDTH = 800;
        private int HEIGHT = 600;

        public MainWindow() : base(WindowType.Toplevel)
        {
            SetDefaultSize(WIDTH, HEIGHT);
            SetPosition(WindowPosition.Center);
            DeleteEvent += delegate { Application.Quit(); };

            VBox vbox = new VBox(false, 3);
            vbox.PackStart(this.BuildInsert(), false, false, 0);
            vbox.PackStart(this.BuildUpdate(), false, false, 0);
            vbox.PackEnd(this.BuildExit(), false, false, 10);

            vbox.PackEnd(this.BuildTable(), true, true, 10);

            Add(vbox);
        }

        private Alignment BuildInsert()
        {

            Alignment alignment = new Alignment(1, 0, 1, 0);
            alignment.SetPadding(10, 10, 10, 10);

            HBox hbox = new HBox(false, 6);

            this.eDNI = new Entry();
            Label lblDNI = new Label("<b>DNI:</b>");
            lblDNI.UseMarkup = true;

            hbox.Add(lblDNI);
            hbox.Add(this.eDNI);



            this.eNombre = new Entry();
            Label lblNombre = new Label("<b>Nombre:</b>");
            lblNombre.UseMarkup = true;

            hbox.Add(lblNombre);
            hbox.Add(this.eNombre);

            this.eApellidos = new Entry();
            Label lblApellidos = new Label("<b>Apellidos:</b>");
            lblApellidos.UseMarkup = true;

            hbox.Add(lblApellidos);
            hbox.Add(this.eApellidos);



            this.eTelefono = new Entry();
            Label lblTelefono = new Label("<b>Telefono:</b>");
            lblTelefono.UseMarkup = true;

            hbox.Add(lblTelefono);
            hbox.Add(this.eTelefono);



            this.eEmail = new Entry();
            Label lblEmail = new Label("<b>Email:</b>");
            lblEmail.UseMarkup = true;

            hbox.Add(lblEmail);
            hbox.Add(this.eEmail);



            this.eDireccion = new Entry();
            Label lblDireccion = new Label("<b>Direccion:</b>");
            lblDireccion.UseMarkup = true;

            hbox.Add(lblDireccion);
            hbox.Add(this.eDireccion);


            Button btnInsert = new Button("Añadir");
            btnInsert.Clicked += AñadirMiembro;

            hbox.Add(btnInsert);

            alignment.Add(hbox);

            return alignment;
        }

        private Alignment BuildUpdate()
        {

            Alignment alignment = new Alignment(1, 0, 1, 0);
            alignment.SetPadding(10, 10, 10, 10);

            HBox hbox = new HBox(false, 6);

            this.eUpdateDNI = new Entry() { CanFocus = false };
            Label lblDNI = new Label("<b>DNI:</b>");
            lblDNI.UseMarkup = true;
            lblDNI.Sensitive = false;

            hbox.Add(lblDNI);
            hbox.Add(this.eUpdateDNI);

            this.eUpdateNombre = new Entry() { CanFocus = false };
            Label lblNombre = new Label("<b>Nombre:</b>");
            lblNombre.UseMarkup = true;
            lblNombre.Sensitive = false;

            hbox.Add(lblNombre);
            hbox.Add(this.eUpdateNombre);

            this.eUpdateApellidos = new Entry() { CanFocus = false };
            Label lblApellidos = new Label("<b>Apellidos:</b>");
            lblApellidos.UseMarkup = true;
            lblApellidos.Sensitive = false;

            hbox.Add(lblApellidos);
            hbox.Add(this.eUpdateApellidos);

            this.eUpdateTelefono = new Entry() { CanFocus = false };
            Label lblTelefono = new Label("<b>Telefono:</b>");
            lblTelefono.UseMarkup = true;
            lblTelefono.Sensitive = false;

            hbox.Add(lblTelefono);
            hbox.Add(this.eUpdateTelefono);




            this.eUpdateEmail = new Entry() { CanFocus = false };
            Label lblEmail = new Label("<b>Email:</b>");
            lblEmail.UseMarkup = true;
            lblEmail.Sensitive = false;

            hbox.Add(lblEmail);
            hbox.Add(this.eUpdateEmail);




            this.eUpdateDireccion = new Entry() { CanFocus = false };
            Label lblDireccion = new Label("<b>Direccion:</b>");
            lblDireccion.UseMarkup = true;
            lblDireccion.Sensitive = false;

            hbox.Add(lblDireccion);
            hbox.Add(this.eUpdateDireccion);

            this.btnUpdate = new Button("Update");
            btnUpdate.Sensitive = false;
            btnUpdate.Clicked += ActualizarLista;
            hbox.Add(this.btnUpdate);

            alignment.Add(hbox);
            lbllist.Add(lblDNI);
            lbllist.Add(lblNombre);
            lbllist.Add(lblApellidos);
            lbllist.Add(lblTelefono);
            lbllist.Add(lblEmail);
            lbllist.Add(lblDireccion);
            

            return alignment;
        }

        private ScrolledWindow BuildTable()
        {
            tvDatos.RowActivated += CargarUpdateForm;

            TreeViewColumn index = new TreeViewColumn { Title = "#" };
            CellRendererText indexCell = new CellRendererText();
            index.PackStart(indexCell, true);
            index.AddAttribute(indexCell, "text", 0);
            tvDatos.AppendColumn(index);

            TreeViewColumn DNI = new TreeViewColumn { Title = "DNI", Alignment = (float)0.5 };
            DNI.Expand = true;
            CellRendererText DNICell = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };

            DNI.PackStart(DNICell, true);
            DNI.AddAttribute(DNICell, "text", 1);
            tvDatos.AppendColumn(DNI);

            TreeViewColumn nombre = new TreeViewColumn { Title = "Nombre", Alignment = (float)0.5 };
            nombre.Expand = true;
            CellRendererText nombreCell = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
            nombre.PackStart(nombreCell, true);
            nombre.AddAttribute(nombreCell, "text", 2);
            tvDatos.AppendColumn(nombre);

            TreeViewColumn apellidos = new TreeViewColumn { Title = "Apellidos", Alignment = (float)0.5 };
            apellidos.Expand = true;
            CellRendererText apellidosCell = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
            apellidos.PackStart(apellidosCell, true);
            apellidos.AddAttribute(apellidosCell, "text", 3);
            tvDatos.AppendColumn(apellidos);

            TreeViewColumn telefono = new TreeViewColumn { Title = "Telefono", Alignment = (float)0.5 };
            telefono.Expand = true;
            CellRendererText telefonoCell = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
            telefono.PackStart(telefonoCell, true);
            telefono.AddAttribute(telefonoCell, "text", 4);
            tvDatos.AppendColumn(telefono);

            TreeViewColumn email = new TreeViewColumn { Title = "Email", Alignment = (float)0.5 };
            email.Expand = true;
            CellRendererText emailCell = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
            email.PackStart(emailCell, true);
            email.AddAttribute(emailCell, "text", 5);
            tvDatos.AppendColumn(email);

            TreeViewColumn direccion = new TreeViewColumn { Title = "Direccion", Alignment = (float)0.5 };
            direccion.Expand = true;
            CellRendererText direccionCell = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
            direccion.PackStart(direccionCell, true);
            direccion.AddAttribute(direccionCell, "text", 6);
            tvDatos.AppendColumn(direccion);

            ls = new ListStore(typeof(int), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            //ActualizarDatos ();
            tvDatos.Model = ls;

            ScrolledWindow sw = new ScrolledWindow();

            sw.Add(tvDatos);

            return sw;
        }

        private HBox BuildExit()
        {
            HBox hbox = new HBox(false, 5);
            Button btnAbir = new Button("Abrir");
            Button btnGuardar = new Button("Guardar");
            Button btnExit = new Button("Salir");
            btnAbir.Clicked += AbrirArchivo;
            btnGuardar.Clicked += GuardarArchivo;
            btnExit.Clicked += CerrarPrograma;

            hbox.Add(btnAbir);
            hbox.Add(btnGuardar);
            hbox.Add(btnExit);
            return hbox;
        }

    }
}

