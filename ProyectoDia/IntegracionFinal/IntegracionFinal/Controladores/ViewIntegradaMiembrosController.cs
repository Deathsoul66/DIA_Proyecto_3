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

        private void InsertaDatos()
        {
            cont = 1;

            foreach (Miembro m in listaM)
            {
                ls.AppendValues(cont++, m.dni, m.nombre, m.apellidos, m.telefono, m.email, m.direccion);
            }
        }

        private ScrolledWindow BuildTable()
        {

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
            InsertaDatos();
            tvDatos.Model = ls;

            ScrolledWindow sw = new ScrolledWindow();

            sw.Add(tvDatos);
            sw.SetSizeRequest(550,380);

            return sw;
        }
    }

}  

