
namespace ViajesIU.View
{
  
    using System.Collections.Generic;
    using Gtk;
    using System;
    using ProyectoFinal;

    public partial class MainWindow
    {
        private int cont = 1;
        private TreeIter lastIter;
        private int lastcont;
        private void CerrarPrograma(object sender, EventArgs e)
        {
            Gtk.Application.Quit();
        }





        private void InsertaDatos()
        {
            cont = 1;
            foreach (Miembro m in Miembro.LeeXML(fileroute))
            {
                ls.AppendValues(cont++, m.DNI, m.nombre, m.telefono, m.email, m.direccion);
            }
        }





        private void GuardarArchivo(object sender, EventArgs e)
        {
            Gtk.FileChooserDialog fc =
                new Gtk.FileChooserDialog("Save file",
                                          this,
                                          Gtk.FileChooserAction.Save,
                                          "Cancel", Gtk.ResponseType.Cancel,
                                          "Save", Gtk.ResponseType.Accept);
            fc.Filter = new Gtk.FileFilter();
            fc.Filter.AddPattern("*.xml");
            fc.DoOverwriteConfirmation = true;
            fc.Modal = true;

            if (fc.Run() == (int)Gtk.ResponseType.Accept)
            {
                List<Miembro> listaMiembros = new List<Miembro>();

                Gtk.TreeModel model = tvDatos.Model;
                Gtk.TreeIter iter;
                if (model.GetIterFirst(out iter))
                {
                    do
                    {
                        Miembro aux = new Miembro
                        {
                            DNI = (string)model.GetValue(iter, 1),
                            nombre = (string)model.GetValue(iter, 2),
                            telefono = (string)model.GetValue(iter, 3),
                            email = (string)model.GetValue(iter, 4),
                            direccion = (string)model.GetValue(iter, 5)
                        };
                        listaMiembros.Add(aux);
                    } while (model.IterNext(ref iter));
                }
                if (fc.Filename.EndsWith(".xml"))
                {
                    Miembro.GeneraXML(fc.Filename, listaMiembros);
                }
                else
                {
                    Miembro.GeneraXML(fc.Filename + ".xml", listaMiembros);
                }

            }
            fc.Destroy();
        }
        //MIRAR ESTO MUCHO MAS--------------------------------------------------------------------------------------------------------
        private void AbrirArchivo(object sender, EventArgs e)
        {

            Gtk.FileChooserDialog fc =
                new Gtk.FileChooserDialog("Choose the file to open",
                                          this,
                                          Gtk.FileChooserAction.Open,
                                          "Cancel", Gtk.ResponseType.Cancel,
                                          "Open", Gtk.ResponseType.Accept);
            fc.Filter = new Gtk.FileFilter();
            fc.Filter.AddPattern("*.xml");

            if (fc.Run() == (int)Gtk.ResponseType.Accept)
            {
                System.IO.FileStream file = System.IO.File.OpenRead(fc.Filename);
                this.fileroute = fc.Filename;
                ls.Clear();
                InsertaDatos();
                file.Close();

                eDNI.Text = "";
                eNombre.Text = "";
                eTelefono.Text = "";
                eEmail.Text = "";
                eDireccion.Text = "";

                eUpdateDNI.Text = "";
                eUpdateNombre.Text = "";
                eUpdateTelefono.Text = "";
                eUpdateEmail.Text = "";
                eUpdateDireccion.Text = "";
            }
            //Destroy() to close the File Dialog
            fc.Destroy();

        }
        //..........................................................................................


        private void AñadirMiembro(object sender, EventArgs e)
        {
            try
            {
                Miembro m = new Miembro(eDNI.Text, eNombre.Text, eTelefono.Text, eEmail.Text, eDireccion.Text);
                ls.AppendValues(cont++, m.DNI, m.nombre, m.telefono, m.email, m.direccion);
            }
            catch
            {
                Error();
            }
            eDNI.Text = "";
            eNombre.Text = "";
            eTelefono.Text = "";
            eEmail.Text = "";
            eDireccion.Text = "";
            


        }

        private void ActualizarLista(object sender, EventArgs e)
        {
            try
            {
                Miembro m = new Miembro(eUpdateDNI.Text, eUpdateNombre.Text, eUpdateTelefono.Text, eUpdateEmail.Text, eUpdateDireccion.Text);
                ls.SetValues(lastIter, lastcont, m.DNI, m.nombre, m.telefono, m.email, m.direccion);

                foreach (Gtk.Label lbl in lbllist)
                {
                    lbl.Sensitive = false;
                }

                eUpdateDNI.Text = "";
                eUpdateDNI.CanFocus = false;

                eUpdateNombre.Text = "";
                eUpdateNombre.CanFocus = false;

                eUpdateTelefono.Text = "";
                eUpdateTelefono.CanFocus = false;

                eUpdateEmail.Text = "";
                eUpdateEmail.CanFocus = false;

                eUpdateDireccion.Text = "";
                eUpdateDireccion.CanFocus = false;

               

                btnUpdate.Sensitive = false;
            }
            catch
            {
                Error();
            }
        }

        private void CargarUpdateForm(object o, RowActivatedArgs args)
        {

            ls.GetIter(out lastIter, args.Path);
            foreach (Gtk.Label lbl in lbllist)
            {
                lbl.Sensitive = true;
            }
            lastcont = (int)ls.GetValue(lastIter, 0);

            eUpdateDNI.Text = (string)ls.GetValue(lastIter, 1);
            eUpdateDNI.CanFocus = true;

            eUpdateNombre.Text = (string)ls.GetValue(lastIter, 2);
            eUpdateNombre.CanFocus = true;

            eUpdateTelefono.Text = (string)ls.GetValue(lastIter, 3);
            eUpdateTelefono.CanFocus = true;

            eUpdateEmail.Text = (string)ls.GetValue(lastIter, 4);
            eUpdateEmail.CanFocus = true;

            eUpdateDireccion.Text = (string)ls.GetValue(lastIter, 5);
            eUpdateDireccion.CanFocus = true;
            

            btnUpdate.Sensitive = true;
        }




        private void Error()
        {
          
            MessageDialog md = new MessageDialog(this,
                                                  DialogFlags.DestroyWithParent, MessageType.Error,
                                                  ButtonsType.Close, "Unexpected Error\n");
            md.Run();
            md.Destroy();
        }

    }
}

