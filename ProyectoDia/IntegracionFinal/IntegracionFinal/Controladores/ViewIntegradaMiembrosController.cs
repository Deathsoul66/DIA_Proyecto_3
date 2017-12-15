using System;
using System.Collections.Generic;
using Gtk;

namespace IntegracionFinal
{
    public partial class ViewIntegrada
    {
        private List<Label> lbllist = new List<Label>();
        private TreeView tvDatos = new TreeView();
        private ListStore ls;
        private int cont = 1;

        private Entry entNum = new Entry();
        private Entry entDNI = new Entry();
        private Entry entNombreMiemb = new Entry();
        private Entry entApellidosMiemb = new Entry();
        private Entry entTele = new Entry();
        private Entry entEmail = new Entry();
        private Entry entDir = new Entry();

        private TreeIter lastIterMiemb;

        private enum ColumnMiembro { Id, Dni, Nombre, Apellidos, Telefono, Email, Direccion};

        //Metodo para insertar los datos de los miembros en la listStore
        private void InsertaDatos()
        {
            cont = 1;

            foreach (Miembro m in listaM)
            {
                ls.AppendValues(cont++, m.dni, m.nombre, m.apellidos, m.telefono, m.email, m.direccion);
            }
        }

        //Metodo para construir la tabla de miembros
        private ScrolledWindow BuildTable()
        {

            TreeViewColumn index = new TreeViewColumn { Title = "#" };
            CellRendererText indexCell = new CellRendererText();
            index.PackStart(indexCell, true);
            index.AddAttribute(indexCell, "text", 0);
            index.SortColumnId = (int)ColumnMiembro.Id; //Para ordenar
            tvDatos.AppendColumn(index);

            TreeViewColumn DNI = new TreeViewColumn { Title = "DNI", Alignment = (float)0.5 };
            DNI.Expand = true;
            CellRendererText DNICell = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
            DNI.PackStart(DNICell, true);
            DNI.AddAttribute(DNICell, "text", 1);
            DNI.SortColumnId = (int)ColumnMiembro.Dni; //Para ordenar
            tvDatos.AppendColumn(DNI);

            TreeViewColumn nombre = new TreeViewColumn { Title = "Nombre", Alignment = (float)0.5 };
            nombre.Expand = true;
            CellRendererText nombreCell = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
            nombre.PackStart(nombreCell, true);
            nombre.AddAttribute(nombreCell, "text", 2);
            nombre.SortColumnId = (int)ColumnMiembro.Nombre;
            tvDatos.AppendColumn(nombre);

            TreeViewColumn apellidos = new TreeViewColumn { Title = "Apellidos", Alignment = (float)0.5 };
            apellidos.Expand = true;
            CellRendererText apellidosCell = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
            apellidos.PackStart(apellidosCell, true);
            apellidos.AddAttribute(apellidosCell, "text", 3);
            apellidos.SortColumnId = (int)ColumnMiembro.Apellidos;
            tvDatos.AppendColumn(apellidos);

            TreeViewColumn telefono = new TreeViewColumn { Title = "Telefono", Alignment = (float)0.5 };
            telefono.Expand = true;
            CellRendererText telefonoCell = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
            telefono.PackStart(telefonoCell, true);
            telefono.AddAttribute(telefonoCell, "text", 4);
            telefono.SortColumnId = (int)ColumnMiembro.Telefono;
            tvDatos.AppendColumn(telefono);

            TreeViewColumn email = new TreeViewColumn { Title = "Email", Alignment = (float)0.5 };
            email.Expand = true;
            CellRendererText emailCell = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
            email.PackStart(emailCell, true);
            email.AddAttribute(emailCell, "text", 5);
            email.SortColumnId = (int)ColumnMiembro.Email;
            tvDatos.AppendColumn(email);

            TreeViewColumn direccion = new TreeViewColumn { Title = "Direccion", Alignment = (float)0.5 };
            direccion.Expand = true;
            CellRendererText direccionCell = new CellRendererText { Alignment = Pango.Alignment.Center, Xalign = 0.5f };
            direccion.PackStart(direccionCell, true);
            direccion.AddAttribute(direccionCell, "text", 6);
            direccion.SortColumnId = (int)ColumnMiembro.Direccion;
            tvDatos.AppendColumn(direccion);

            ls = new ListStore(typeof(int), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            InsertaDatos();
            tvDatos.Model = ls;
            tvDatos.RowActivated += UpdateMiembro;
            
            ScrolledWindow sw = new ScrolledWindow();

            sw.Add(tvDatos);
            sw.SetSizeRequest(550,380);

            return sw;
        }

        //Funcion para añadir un nuevo miembro (Se muestra en una ventana Modal)
        private void AñadirMiembro(object sender, EventArgs e)
        {
            
            Dialog d1 = new Dialog("Nuevo Miebro", this, DialogFlags.Modal, "Insertar", Gtk.ResponseType.Accept,
                                  "Cancelar", Gtk.ResponseType.Cancel);
            int num = listaM.Count + 1;
            d1.VBox.Add(CrearBotonesMiembros((string)num.ToString()));
            d1.ShowAll();
            if (d1.Run() == (int)Gtk.ResponseType.Accept)
            {

                Miembro m = new Miembro(entDNI.Text, entNombreMiemb.Text, entApellidosMiemb.Text, entTele.Text, entEmail.Text, entDir.Text);

                try
                {
                    listaM.Add(m);
                    ls.Clear();

                    ls = new ListStore(typeof(int), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
                    InsertaDatos();
                    tvDatos.Model = ls;

                    XmlWriter.GuardarXmlMiembros(MIEMBROS, listaM);
                }
                catch
                {
                    Error("Error al insertar");
                }
                }
                d1.Destroy();
           
        }

        //Funcion para actualizar los datos de un miembro (Se muestra en una venta modal)
        private void UpdateMiembro(object o, RowActivatedArgs args)
        {
            Dialog d = new Dialog("Modifica Publicacion", this, DialogFlags.Modal, "Actualizar", Gtk.ResponseType.Accept,
                                  "Eliminar", Gtk.ResponseType.None, "Cancelar", Gtk.ResponseType.Cancel);

            ls.GetIter(out lastIterMiemb, args.Path);
            d.VBox.Add(CrearBotonesMiembros((string)ls.GetValue(lastIterMiemb, 0).ToString()));
            d.ShowAll();

            entNum.Text = (string)ls.GetValue(lastIterMiemb, 0).ToString();
            entDNI.Text = (string)ls.GetValue(lastIterMiemb, 1).ToString();
            entNombreMiemb.Text = (string)ls.GetValue(lastIterMiemb, 2);
            entApellidosMiemb.Text = (string)ls.GetValue(lastIterMiemb, 3);
            entTele.Text = (string)ls.GetValue(lastIterMiemb, 4).ToString();
            entEmail.Text = ((string)ls.GetValue(lastIterMiemb, 5)).ToString();
            entDir.Text = ((string)ls.GetValue(lastIterMiemb, 6)).ToString();
            
            int response;
            response = d.Run();

            Miembro m = new Miembro(entDNI.Text, entNombreMiemb.Text, entApellidosMiemb.Text, entTele.Text, entEmail.Text, entDir.Text);

            if (response == (int)Gtk.ResponseType.Accept)
            {

                try
                {
                    listaM[Int32.Parse(entNum.Text)-1] = m;
                    ls.Clear();

                    ls = new ListStore(typeof(int), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
                    InsertaDatos();
                    tvDatos.Model = ls;

                    XmlWriter.GuardarXmlMiembros(MIEMBROS, listaM);
                }
                catch
                {
                    Error("Error al actualizar");
                }

            }
            else if (response == (int)Gtk.ResponseType.None)
            {
                int num = (Int32.Parse(entNum.Text)) - 1;
                try{
                    listaM.RemoveAt(num);
                    ls.Remove(ref lastIterMiemb);
                    XmlWriter.GuardarXmlMiembros(MIEMBROS,listaM);
                }
                catch
                {
                    Error("No se pudo eliminar la entrada!");
                }
            }
            
            d.Destroy();
        }

        //Funcion para colocar los elementos en la vista
        private Alignment CrearBotonesMiembros(string num)
        {
            VBox left = new VBox(false, 3);
            VBox right = new VBox(false, 3);
            HBox hbox = new HBox(false, 5);

            Label lblNum = new Label("Num: ");
            lblNum.Xalign = 1;
            entNum.Text = num;
            entNum.CanFocus = false;
            left.PackStart(lblNum);
            right.PackStart(entNum);

            Label lblDNI = new Label("DNI: ");
            lblDNI.Xalign = 1;
            left.PackStart(lblDNI);
            right.PackStart(entDNI);

            Label lblNom = new Label("Nombre: ");
            lblNom.Xalign = 1;
            left.PackStart(lblNom);
            right.PackStart(entNombreMiemb);

            Label lblApellidos = new Label("Apellidos: ");
            lblApellidos.Xalign = 1;
            left.PackStart(lblApellidos);
            right.PackStart(entApellidosMiemb);

            Label lblTele = new Label("Telefono: ");
            lblTele.Xalign = 1;
            left.PackStart(lblTele);
            right.PackStart(entTele);

            Label lblEmail = new Label("Email: ");
            lblEmail.Xalign = 1;
            left.PackStart(lblEmail);
            right.PackStart(entEmail);

            Label lblDir = new Label("Direccion: ");
            lblDir.Xalign = 1;
            left.PackStart(lblDir);
            right.PackStart(entDir);

            hbox.Add(left);
            hbox.Add(right);

            Alignment alignment = new Alignment(1, 0, 1, 0);
            alignment.SetPadding(10, 10, 10, 10);
            alignment.Add(hbox);

            return alignment;
        }
    }
}  

