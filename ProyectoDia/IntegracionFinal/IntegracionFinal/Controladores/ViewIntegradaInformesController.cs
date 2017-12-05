using System;
using System.Collections.Generic;
using Gtk;

namespace IntegracionFinal
{
    public partial class ViewIntegrada
    {
        Dialog viewMeritos;
        VBox vistaMerDepartamento;
        ComboBoxEntry txtEntryAnioDep;
        ComboBoxEntry txtEntryAnioTxt;
        Image plot;
        Label lbAnio = new Label("Año");
        string nombreAutorMiembro = "";
        string anioSelect = "0";

        List<Publicacion> publicaciones = XmlReader.leerPublicaciones(PUBLICACIONES).listPub;
        List<Miembro> miembros = XmlReader.leerMiembros(MIEMBROS);

        void onBtnConsultarMerMiembroMesesClicked(object sender, EventArgs e)
        {
            Dialog askData = new Dialog("Estadísticas Anuales - Seleccionar miembro", this, DialogFlags.Modal, "Aceptar",
                Gtk.ResponseType.Accept, "Cancelar", Gtk.ResponseType.Cancel);

            List<string> miembros = new List<string>();

            foreach (Miembro m in listaM) {
                miembros.Add(m.nombre[0] + "." + m.apellidos.Split(' ')[0]);
            }

            ComboBoxEntry comboMiembros = new ComboBoxEntry(miembros.ToArray());
            ComboBoxEntry cb = new ComboBoxEntry(getAniosPublicaciones());

            HBox contentMiembro = new HBox(false, 2);
            HBox contentAnio = new HBox(false, 2);

            Label lblMiembro = new Label("Miembro: ");
            lblMiembro.Xalign = 1;
            contentMiembro.PackStart(lblMiembro);
            contentMiembro.PackStart(comboMiembros);

            Label lblAnio = new Label("Año: ");
            lblAnio.Xalign = 1;
            contentAnio.PackStart(lblAnio);
            contentAnio.PackStart(cb);

            askData.VBox.PackStart(contentMiembro,true,true,2);
            askData.VBox.PackStart(contentAnio, true, true, 2);
            askData.ShowAll();
            int response = askData.Run();

            if (response == (int)Gtk.ResponseType.Accept)
            {
                if (comboMiembros.ActiveText != "" && cb.ActiveText != "")
                {
                    nombreAutorMiembro = comboMiembros.ActiveText;
                    anioSelect = cb.ActiveText;
                    askData.Destroy();
                    showConsultarMerMiembrosMeses(nombreAutorMiembro, anioSelect);
                }
                else
                {
                    MessageDialog md = new MessageDialog(this,
                        DialogFlags.DestroyWithParent, MessageType.Warning,
                        ButtonsType.Ok, "Los campos no pueden estar vacíos");
                    askData.Destroy();
                    md.Run();
                    md.Destroy();
                }
            }
            else if (response == (int)Gtk.ResponseType.Cancel)
            {
                askData.Destroy();
            }
        }

        void showConsultarMerMiembrosMeses(string miembro, string ano) {

            Dialog showMiembroMes = new Dialog("Estadísticas Anuales - " + miembro, this, DialogFlags.Modal, 
                "Cerrar", Gtk.ResponseType.Close);
            int response = 0;

            if (miembro != "" && ano != "")
            {
                try
                {
                    Miembro mie = new Miembro();
                    foreach (Miembro m in listaM)
                    {
                        if (m.checkMiembro(miembro.Trim()))
                        {
                            mie = m;
                            break;
                        }
                    }
                    if (mie.nombre != "" && mie.apellidos != "")
                    {
                        int anio = Convert.ToInt32(ano.Trim());
                        //plot = Grafico.GraficoBarrasExample();
                        //plot = Grafico.GraficoPieExample();
                        plot = Grafico.merMiembroMes(miembro, meritosCientificos(mie, anio), anio);
                        showMiembroMes.VBox.PackStart(plot, false, false, 0);
                        showMiembroMes.ShowAll();
                        response = showMiembroMes.Run();
                    }
                }
                catch
                {
                    MessageDialog number = new MessageDialog(this,
                        DialogFlags.DestroyWithParent, MessageType.Error,
                        ButtonsType.Ok, "Algo ha fallado, no se ha podido generar el gráfico");
                    number.Run();
                    number.Destroy();
                    showMiembroMes.Destroy();
                }
            }

            if (response == (int)Gtk.ResponseType.Close)
            {
                showMiembroMes.Destroy();
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
                        if (autor == (m.nombre[0] + "." + m.apellidos.Split(' ')[0]))
                        {
                            meritos[Convert.ToInt32(p.FechaPublicacion.Month) - 1] += 1;
                        }
                    }
                }
            }
            return meritos;
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
                            if (autor == miembro.nombre[0] + "." + miembro.apellidos.Split(' ')[0])
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
            if (txtEntryAnioDep.ActiveText != "")
            {
                try
                {
                    //clearOnViewChanged();
                    vistaMerDepartamento.Remove(plot);
                    vistaMerDepartamento.Visible = true;
                    int anio = Convert.ToInt32(txtEntryAnioDep.ActiveText.Trim());
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

        void btnConsultarMerAnioClicked(object sender, EventArgs e)
        {
            viewMeritos = new Dialog("Meritos Anuales departamento", this, DialogFlags.Modal,
                "Cerrar", Gtk.ResponseType.Close);

            HBox menuMerAnio = new HBox();
            HBox selectorMerAnio = new HBox();
            Button btnConsultarMerAnio = new Button("Consultar");
            txtEntryAnioTxt = new ComboBoxEntry(getAniosPublicaciones());
            txtEntryAnioTxt.Active = 0;
            treeView = new TreeView();
            AddColumns();
            selectorMerAnio.PackStart(lbAnio, false, false, 20);
            selectorMerAnio.PackStart(txtEntryAnioTxt, false, false, 5);
            menuMerAnio.PackStart(selectorMerAnio, false, false, 10);
            menuMerAnio.PackStart(btnConsultarMerAnio, false, false, 5);
            viewMeritos.VBox.PackStart(menuMerAnio, false, false, 0);
            viewMeritos.VBox.PackStart(treeView, false, false, 5);
            showMeritos(this, null);
            viewMeritos.VBox.PackStart(treeView);
            viewMeritos.ShowAll();

            btnConsultarMerAnio.Clicked += showMeritos;
            int response = viewMeritos.Run();
            if (response == (int)Gtk.ResponseType.Close)
            {
                viewMeritos.Destroy();
            }
        }

        private void showMeritos(object sender, EventArgs e) { 

            if (txtEntryAnioTxt.ActiveText != "")
            {
                try
                {
                    viewMeritos.VBox.Remove(treeView);
                    this.treeView.Destroy();
                    treeView = new TreeView(CreateFilteredModel(Convert.ToInt32(txtEntryAnioTxt.ActiveText)));
                    treeView.RulesHint = true;
                    AddColumns();
                    viewMeritos.VBox.PackStart(treeView);
                    viewMeritos.VBox.ShowAll();

                }
                catch
                {
                    MessageDialog number = new MessageDialog(this,
                        DialogFlags.DestroyWithParent, MessageType.Error,
                        ButtonsType.Ok, "Algo ha fallado, no se ha podido generar el resumen");
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

        public string[] getAniosPublicaciones() {

            SortedSet<string> aniosRecuperados = new SortedSet<string>();

            foreach (Publicacion p in listaP.listPub){
                foreach (string autor in p.Autores) {
                    foreach (Miembro m in listaM) {
                        string aut = m.nombre[0]+ "." + m.apellidos.Split(' ')[0];
                        if (autor.Equals(aut)) {
                            aniosRecuperados.Add(p.FechaPublicacion.Year.ToString());
                            break;
                        }
                    }
                }
            }
            
            List<string> toRet = new List<string>();

            //De mayor a menor
            foreach (string anio in aniosRecuperados.Reverse()) {
                toRet.Add(anio);
            }
            
            return toRet.ToArray();
        }

    }
}
