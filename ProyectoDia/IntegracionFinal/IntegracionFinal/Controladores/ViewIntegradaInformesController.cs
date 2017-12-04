using System;
using System.Collections.Generic;
using Gtk;

namespace IntegracionFinal
{
    public partial class ViewIntegrada
    {

        ScrolledWindow vistaLista;
        VBox vistaMerMiembroMeses;
        VBox vistaMerDepartamento;
        VBox vistaMerAnio;
        ComboBox comboMiembros;
        Entry txtEntryAnio;
        Entry txtEntryAnioDep;
        Entry txtEntryAnioTxt;
        Image plot;
        Label lbAnio = new Label("Año");

        List<Publicacion> publicaciones = XmlReader.leerPublicaciones("Test.xml").listPub;
        List<Miembro> miembros = XmlReader.leerMiembros("TestMiembros.xml");

        void clearOnViewChanged()
        {
            vistaMerMiembroMeses.Visible = false;
            vistaLista.Visible = false;
            vistaMerDepartamento.Visible = false;
            vistaMerAnio.Visible = false;
            vistaMerMiembroMeses.Remove(plot);
            vistaMerDepartamento.Remove(plot);
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

        void onBtnConsultarMerMiembroMesesClicked(object sender, EventArgs e)
        {

            if (comboMiembros.ActiveText != "" && txtEntryAnio.Text != "")
            {
                try
                {
                    clearOnViewChanged();
                    vistaMerMiembroMeses.Visible = true;
                    Miembro mie = new Miembro();
                    foreach (Miembro m in listaM)
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
            //clearOnViewChanged();
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
                    //clearOnViewChanged();
                    vistaMerDepartamento.Remove(plot);
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
            //clearOnViewChanged();
            vistaMerAnio.ShowAll();
        }

        void btnConsultarMerAnioClicked(object sender, EventArgs e)
        {
            if (txtEntryAnioTxt.Text != "")
            {
                try
                {
                    //clearOnViewChanged();
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
