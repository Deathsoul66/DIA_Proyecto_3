using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gtk;
using System.Text.RegularExpressions;

namespace IntegracionFinal
{
    public partial class ViewIntegrada
    {
        private TreeIter lastIter;

        //private Toolbar tlb;
        //private TreeView mainTree = new TreeView();
        //private ScrolledWindow scrWin = new ScrolledWindow();
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

        private void AñadirPublicacion(object sender, EventArgs e)
        {
            Dialog d = new Dialog("Elige tipo de publicacion", this, DialogFlags.Modal, "Aceptar", Gtk.ResponseType.Accept,
                                  "Cancelar", Gtk.ResponseType.Cancel);
            ComboBox cb = TipoPublicacion();
            cb.Active = 0; //Ponemos por defecto el valor "Articulo"
            d.VBox.Add(cb);
            d.ShowAll();
            if (d.Run() == (int)Gtk.ResponseType.Accept)
            {
                d.Destroy();
                Dialog d1 = new Dialog("Nuevo " + cb.ActiveText, this, DialogFlags.Modal, "Insertar", Gtk.ResponseType.Accept,
                                  "Cancelar", Gtk.ResponseType.Cancel);
                d1.VBox.Add(CrearBotones(cb.ActiveText));
                d1.ShowAll();
                if (d1.Run() == (int)Gtk.ResponseType.Accept)
                {
                    List<string> autores = new List<string>();
                    foreach (string autor in entAutores.Text.Split(','))
                    {
                        autores.Add(autor.Trim());
                    }
                    Publicacion p;
                    listaP = XmlReader.leerPublicaciones(PUBLICACIONES);

                    try
                    {
                        switch (cb.ActiveText)
                        {
                            case "Congreso":
                                p = Publicacion.Create(IntegracionFinal.TipoPublicacion.Congreso, entDOI.Text, entTitulo.Text, entEditorial.Text, entAnho.Date, entPIni.Text
                                                        , entPFin.Text, autores, entNombre.Text, entCiudad.Text, entFecha.GetDate().ToString("dd/MM/yyyy"));
                                if (listaP.addPublicacion(p))
                                {
                                    store.AppendValues(p.getTipo(), p.DOI, p.Titulo, p.Editorial, p.FechaPublicacion.ToString("dd/MM/yyyy"), p.PagInicio, p.PagFin, ListaAutores(p),
                                        p.getNombre(), p.getCiudad(), p.getFecha());
                                }
                                break;
                            case "Articulo":
                                p = Publicacion.Create(IntegracionFinal.TipoPublicacion.Articulo, entDOI.Text, entTitulo.Text, entEditorial.Text, entAnho.Date, entPIni.Text
                                                        , entPFin.Text, autores);
                                if (listaP.addPublicacion(p))
                                {
                                    store.AppendValues(p.getTipo(), p.DOI, p.Titulo, p.Editorial, p.FechaPublicacion.ToString("dd/MM/yyyy"), p.PagInicio, p.PagFin, ListaAutores(p), "-", "-", "-");
                                }

                                break;
                            case "Libro":
                                p = Publicacion.Create(IntegracionFinal.TipoPublicacion.Libro, entDOI.Text, entTitulo.Text, entEditorial.Text, entAnho.Date, entPIni.Text
                                                        , entPFin.Text, autores);
                                if (listaP.addPublicacion(p))
                                {
                                    store.AppendValues(p.getTipo(), p.DOI, p.Titulo, p.Editorial, p.FechaPublicacion.ToString("dd/MM/yyyy"), p.PagInicio, p.PagFin, ListaAutores(p), "-", "-", "-");
                                }
                                break;
                        }
                        XmlWriter.GuardarXmlPublicaciones(PUBLICACIONES, listaP.listPub);
                    }
                    catch
                    {
                        Error("Error al insertar");
                    }
                }
               
                d1.Destroy();
            }
            else
            {
                d.Destroy();
            }
        }

        private void MenuPublicacionesActivate(object o, EventArgs args) {
            subMenuDeletePublicacion.Sensitive = true;
            subMenuEditPublicacion.Sensitive = true;
        }

        private void UpdatePublicacion(object o, RowActivatedArgs args)
        {
            Dialog d = new Dialog("Modifica Publicacion", this, DialogFlags.Modal, "Actualizar", Gtk.ResponseType.Accept,
                                  "Eliminar", Gtk.ResponseType.None, "Cancelar", Gtk.ResponseType.Cancel);

            store.GetIter(out lastIter, args.Path);
            d.VBox.Add(CrearBotones((string)store.GetValue(lastIter, 0)));
            d.ShowAll();

            entTipo.Text = (string)store.GetValue(lastIter, 0);
            string oldDoi = entDOI.Text = (string)store.GetValue(lastIter, 1);
            entTitulo.Text = (string)store.GetValue(lastIter, 2);
            entEditorial.Text = (string)store.GetValue(lastIter, 3);
            entAnho.Date = DateTime.Parse((string)store.GetValue(lastIter, 4));
            entPIni.Text = ((string)store.GetValue(lastIter, 5)).ToString();
            entPFin.Text = ((string)store.GetValue(lastIter, 6)).ToString();
            entAutores.Text = (string)store.GetValue(lastIter, 7);

            if (entTipo.Text.Equals("Congreso"))
            {
                entNombre.Text = (string)store.GetValue(lastIter, 8);
                entCiudad.Text = (string)store.GetValue(lastIter, 9);
                entFecha.Date = DateTime.Parse((string)listaP.getPublicacionDOI((string)store.GetValue(lastIter, 1)).getFecha());
            }


            int response;
            response = d.Run();

            if (response == (int)Gtk.ResponseType.Accept)
            {
                List<string> autores = new List<string>();
                foreach (string autor in entAutores.Text.Split(','))
                {
                    autores.Add(autor.Trim());
                }
                Publicacion p;
                listaP = XmlReader.leerPublicaciones(PUBLICACIONES);
                try
                {
                    switch (entTipo.Text)
                    {
                        case "Congreso":
                            p = Publicacion.Create(IntegracionFinal.TipoPublicacion.Congreso, entDOI.Text, entTitulo.Text, entEditorial.Text, entAnho.Date, entPIni.Text
                                                       , entPFin.Text, autores, entNombre.Text, entCiudad.Text, entFecha.GetDate().ToString("dd/MM/yyyy"));
                            if (listaP.Modificar(p, oldDoi))
                            {
                                store.SetValues(lastIter, p.getTipo(), p.DOI, p.Titulo, p.Editorial, p.FechaPublicacion.ToString("dd/MM/yyyy"), p.PagInicio, p.PagFin, ListaAutores(p),
                                    p.getNombre(), p.getCiudad(), p.getFecha());
                            }

                            break;
                        case "Articulo":
                            p = Publicacion.Create(IntegracionFinal.TipoPublicacion.Articulo, entDOI.Text, entTitulo.Text, entEditorial.Text, entAnho.Date, entPIni.Text
                                                       , entPFin.Text, autores);
                            if (listaP.Modificar(p, oldDoi))
                            {
                                store.SetValues(lastIter, p.getTipo(), p.DOI, p.Titulo, p.Editorial, p.FechaPublicacion.ToString("dd/MM/yyyy"), p.PagInicio, p.PagFin, ListaAutores(p), "-", "-", "-");
                            }
                            break;
                        case "Libro":
                            p = Publicacion.Create(IntegracionFinal.TipoPublicacion.Libro, entDOI.Text, entTitulo.Text, entEditorial.Text, entAnho.Date, entPIni.Text
                                                        , entPFin.Text, autores);
                            if (listaP.Modificar(p, oldDoi))
                            {
                                store.SetValues(lastIter, p.getTipo(), p.DOI, p.Titulo, p.Editorial, p.FechaPublicacion.ToString("dd/MM/yyyy"), p.PagInicio, p.PagFin, ListaAutores(p), "-", "-", "-");
                            }
                            break;
                    }
                    XmlWriter.GuardarXmlPublicaciones(PUBLICACIONES, listaP.listPub);
                }
                catch
                {
                    Error("Error al actualizar");
                }

            }
            else if (response == (int)Gtk.ResponseType.None)
            {
                if (listaP.Eliminar(entDOI.Text))
                {
                    store.Remove(ref lastIter);
                    XmlWriter.GuardarXmlPublicaciones(PUBLICACIONES, listaP.listPub);
                }
                else
                {
                    Error("No se pudo eliminar la entrada!");
                }
                //statusbar.Push(0, row);
            }
            d.Destroy();
        }

        private Alignment CrearBotones(string tipo)
        {
            VBox left = new VBox(false, 3);
            VBox right = new VBox(false, 3);
            HBox hbox = new HBox(false, 5);
            VBox vboxCal = new VBox(false, 3);
            VBox left2 = new VBox(false, 3);
            VBox right2 = new VBox(false, 3);
            VBox left3 = new VBox(false, 3);
            VBox right3 = new VBox(false, 3);

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
            left3.PackStart(lblAnho);
            right3.PackStart(entAnho);

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

            Label lblAutoresComA = new Label("Formato autores: ");
            Label lblAutoresComB = new Label("N.Apellido, N.Apellido... ");
            lblAutoresComA.Xalign = 1;
            lblAutoresComA.Yalign = 1;
            lblAutoresComB.Xalign = 0;
            right.PackStart(lblAutoresComB);
            left.PackStart(lblAutoresComA);

            if (tipo.Equals("Congreso"))
            {
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
            vboxCal.Add(left3);
            vboxCal.Add(right3);
            vboxCal.Add(left2);
            vboxCal.Add(right2);
            hbox.Add(vboxCal);

            Alignment alignment = new Alignment(1, 0, 1, 0);
            alignment.SetPadding(10, 10, 10, 10);
            alignment.Add(hbox);

            return alignment;
        }

        private ComboBox TipoPublicacion()
        {
            return new ComboBox(new string[] { "Articulo", "Congreso", "Libro" });
        }

        private void OnlyNumber(object obj, TextInsertedArgs tia)
        {
            var regex = new Regex("^[0-9]+$");
            if (!regex.IsMatch(tia.Text))
            {
                ((Entry)obj).Text = ((Entry)obj).Text.Length > 0 ? ((Entry)obj).Text.Substring(0, ((Entry)obj).Text.Length - 1) : "";
            }

        }

        private void Error(string error)
        {
            MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error,
                                                 ButtonsType.Close, error);
            md.Run();
            md.Destroy();
        }

        private string ListaAutores(Publicacion p)
        {
            string autores = "";
            foreach (string autor in p.Autores)
            {
                autores += autor + ", ";
            }
            return (autores.Length > 1) ? autores.Substring(0, autores.Length - 2) : autores;
        }
    }
}
