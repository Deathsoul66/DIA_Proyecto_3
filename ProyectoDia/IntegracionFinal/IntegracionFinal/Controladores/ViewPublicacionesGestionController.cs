﻿using System;
namespace IntegracionFinal
{
    using Gtk;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    public partial class MainWindowIMT
    {
        private TreeIter lastIter;
        private string fileroute;
        private ListaPublicacion listaP = new ListaPublicacion();

        private void AbrirXML(object sender, EventArgs e)
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

            }
            fc.Destroy();

        }

        private void InsertaDatos()
        {
            try{
				listaP = XmlReader.leerPublicaciones(fileroute);

            }catch(Exception err){
                Error(err.Message);
            }
			foreach (Publicacion p in listaP.listPub)
			{
				if (p.getTipo().Equals("Congreso"))
				{
					ls.AppendValues(p.getTipo(), p.DOI, p.Titulo, p.Editorial, p.FechaPublicacion.ToString("dd/MM/yyyy"), p.PagInicio, p.PagFin, ListaAutores(p),
									p.getNombre(), p.getCiudad(), p.getFecha());
				}
				else
				{
					ls.AppendValues(p.getTipo(), p.DOI, p.Titulo, p.Editorial, p.FechaPublicacion.ToString("dd/MM/yyyy"), p.PagInicio, p.PagFin, ListaAutores(p), "-", "-", "-");
				}
			}
        }

        private string ListaAutores(Publicacion p){
			string autores = "";
			foreach (string autor in p.Autores)
			{
				autores += autor + ", ";
			}
            return (autores.Length > 1)? autores.Substring(0,autores.Length-2):autores;
        }
        private void GuardarXML(object sender, EventArgs e)
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
                if (fc.Filename.EndsWith(".xml"))
                {
                    XmlWriter.GuardarXmlPublicaciones(fc.Filename, listaP.listPub);
                }
                else
                {
                    XmlWriter.GuardarXmlPublicaciones(fc.Filename + ".xml", listaP.listPub);
                }

            }
            fc.Destroy();
        }
        private void AñadirPublicacion(object sender, EventArgs e)
        {
			Dialog d = new Dialog("Elige tipo de publicacion", this, DialogFlags.Modal, "Aceptar", Gtk.ResponseType.Accept,
								  "Cancelar", Gtk.ResponseType.Cancel);
            ComboBox cb = TipoPublicacion();
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
                    foreach(string autor in entAutores.Text.Split(',')){
                        autores.Add(autor.Trim());
                    }
                    Publicacion p;

                    try{
						switch (cb.ActiveText)
						{
							case "Congreso":
                                p = Publicacion.Create(IntegracionFinal.TipoPublicacion.Congreso, entDOI.Text, entTitulo.Text, entEditorial.Text, entAnho.Date, entPIni.Text
                                                        , entPFin.Text, autores, entNombre.Text, entCiudad.Text, entFecha.GetDate().ToString("dd/MM/yyyy"));
								if (listaP.addPublicacion(p))
								{
									ls.AppendValues(p.getTipo(), p.DOI, p.Titulo, p.Editorial, p.FechaPublicacion.ToString("dd/MM/yyyy"), p.PagInicio, p.PagFin, ListaAutores(p),
										p.getNombre(), p.getCiudad(), p.getFecha());
								}
								break;
							case "Articulo":
                                p = Publicacion.Create(IntegracionFinal.TipoPublicacion.Articulo, entDOI.Text, entTitulo.Text, entEditorial.Text, entAnho.Date, entPIni.Text
                                                        , entPFin.Text, autores);
                                if (listaP.addPublicacion(p))
								{
									ls.AppendValues(p.getTipo(), p.DOI, p.Titulo, p.Editorial, p.FechaPublicacion.ToString("dd/MM/yyyy"), p.PagInicio, p.PagFin, ListaAutores(p), "-", "-", "-");
								}

								break;
							case "Libro":
                                p = Publicacion.Create(IntegracionFinal.TipoPublicacion.Libro, entDOI.Text, entTitulo.Text, entEditorial.Text, entAnho.Date, entPIni.Text
                                                        , entPFin.Text, autores);
                                if (listaP.addPublicacion(p))
								{
									ls.AppendValues(p.getTipo(), p.DOI, p.Titulo, p.Editorial, p.FechaPublicacion.ToString("dd/MM/yyyy"), p.PagInicio, p.PagFin, ListaAutores(p), "-", "-", "-");
								}
								break;
						}
                    }catch{
                        Error("Error al insertar");
                    }
                }
                d1.Destroy();
            }else{
                d.Destroy();
            }
        }

        private void UpdateForm(object o, RowActivatedArgs args)
        {
			Dialog d = new Dialog("Modifica Publicacion", this, DialogFlags.Modal, "Actualizar", Gtk.ResponseType.Accept,
                                  "Eliminar", Gtk.ResponseType.None, "Cancelar", Gtk.ResponseType.Cancel);
            ls.GetIter(out lastIter, args.Path);
            d.VBox.Add(CrearBotones((string)ls.GetValue(lastIter, 0)));
            d.ShowAll();
            
            entTipo.Text = (string)ls.GetValue(lastIter, 0);
            string oldDoi = entDOI.Text = (string)ls.GetValue(lastIter, 1);
            entTitulo.Text = (string)ls.GetValue(lastIter, 2);
            entEditorial.Text = (string)ls.GetValue(lastIter, 3);
            entAnho.Date = DateTime.Parse((string)ls.GetValue(lastIter, 4));
            entPIni.Text = ((string)ls.GetValue(lastIter, 5)).ToString();
            entPFin.Text = ((string)ls.GetValue(lastIter, 6)).ToString();
            entAutores.Text = (string)ls.GetValue(lastIter, 7);

            if (entTipo.Text.Equals("Congreso")){
				entNombre.Text = (string)ls.GetValue(lastIter, 8);
				entCiudad.Text = (string)ls.GetValue(lastIter, 9);
				entFecha.Date = DateTime.Parse(listaP.getPublicacionDOI((string)ls.GetValue(lastIter, 1)).getFecha());
            }


			int response;
			response = d.Run();

            if(response == (int)Gtk.ResponseType.Accept){
				List<string> autores = new List<string>();
				foreach (string autor in entAutores.Text.Split(','))
				{
					autores.Add(autor.Trim());
				}
                Publicacion p;
                
                try{
					switch (entTipo.Text)
					{
						case "Congreso":
                            p = Publicacion.Create(IntegracionFinal.TipoPublicacion.Congreso, entDOI.Text, entTitulo.Text, entEditorial.Text, entAnho.Date, entPIni.Text
                                                       , entPFin.Text, autores, entNombre.Text, entCiudad.Text, entFecha.GetDate().ToString("dd/MM/yyyy"));
                            if (listaP.Modificar(p,oldDoi))
							{
								ls.SetValues(lastIter, p.getTipo(), p.DOI, p.Titulo, p.Editorial, p.FechaPublicacion.ToString("dd/MM/yyyy"), p.PagInicio, p.PagFin, ListaAutores(p),
									p.getNombre(), p.getCiudad(), p.getFecha());
							}

							break;
						case "Articulo":
                            p = Publicacion.Create(IntegracionFinal.TipoPublicacion.Articulo, entDOI.Text, entTitulo.Text, entEditorial.Text, entAnho.Date, entPIni.Text
                                                       , entPFin.Text, autores);
                            if (listaP.Modificar(p, oldDoi))
							{
								ls.SetValues(lastIter, p.getTipo(), p.DOI, p.Titulo, p.Editorial, p.FechaPublicacion.ToString("dd/MM/yyyy"), p.PagInicio, p.PagFin, ListaAutores(p), "-", "-", "-");
							}
							break;
						case "Libro":
                            p = Publicacion.Create(IntegracionFinal.TipoPublicacion.Libro, entDOI.Text, entTitulo.Text, entEditorial.Text, entAnho.Date, entPIni.Text
                                                        , entPFin.Text, autores);
                            if (listaP.Modificar(p, oldDoi))
							{
								ls.SetValues(lastIter, p.getTipo(), p.DOI, p.Titulo, p.Editorial, p.FechaPublicacion.ToString("dd/MM/yyyy"), p.PagInicio, p.PagFin, ListaAutores(p), "-", "-", "-");
							}
							break;
					}
                }catch{
                    Error("Error al actualizar");
                }

            }else if (response == (int)Gtk.ResponseType.None){
                if(listaP.Eliminar(entDOI.Text)){
                    ls.Remove(ref lastIter);
                }else{
                    Error("No se pudo eliminar la entrada!");
                }
            }

            d.Destroy();
        }

        private void OnlyNumber(object obj, TextInsertedArgs tia){
            var regex = new Regex("^[0-9]+$");
            if (!regex.IsMatch(tia.Text))
            {
                ((Entry)obj).Text = ((Entry)obj).Text.Length > 0 ? ((Entry)obj).Text.Substring(0, ((Entry)obj).Text.Length - 1): "";
            }

        }

		private void Error(string error)
		{
			MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error,
                                                 ButtonsType.Close, error);
			md.Run();
			md.Destroy();
		}
    }
}
