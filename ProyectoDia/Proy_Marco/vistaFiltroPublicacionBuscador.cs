using System;
using System.Collections.Generic;
using Gtk;

namespace Proy_Marco
{
	public class vistaFiltroPublicacionBuscador : Dialog
	{
		public string Autor
		{
			get; set;
		}

		public string Anho 
		{
			get; set;
		}

		public string Tipo
		{
			get; set;
		}

		public int Buscar
		{
			get; set;
		}

		public vistaFiltroPublicacionBuscador()
		{
			Build();

		}

		private void Build()
		{
			SetDefaultSize(250, 200);

			SetPosition(WindowPosition.Center);
			BorderWidth = 7;

			HBox pnlAutor = new HBox(false, 5);
			HBox pnlAnho = new HBox(false, 5);
			HBox pnlTipo = new HBox(false, 5);
			HBox pnlButton = new HBox(false, 5);

			Label labelTAutor = new Label("Autor");
			Entry entryAutor = new Entry();
			entryAutor.Changed += (o, e) => this.Autor = entryAutor.Text;
			pnlAutor.PackStart(labelTAutor, false, false, 5);
			pnlAutor.PackStart(entryAutor, true, true, 5);

			Label labelTAnho = new Label("Año");
			Entry entryAnho = new Entry();
			entryAnho.Changed += (o, e) => this.Anho = entryAnho.Text;
			pnlAnho.PackStart(labelTAnho, false, false, 5);
			pnlAnho.PackStart(entryAnho, true, true, 5);

			Label labelTTipo = new Label("Tipo");
			Entry entryTipo = new Entry();
			entryTipo.Changed += (o, e) => this.Tipo = entryTipo.Text;
			pnlTipo.PackStart(labelTTipo, false, false, 5);
			pnlTipo.PackStart(entryTipo, true, true, 5);

			Button buscar = new Button("Buscar");
			buscar.Clicked += OnClickBuscar;
			buscar.SetSizeRequest(80, 35);
			pnlButton.PackStart(buscar, true, true, 5);

			Button quit = new Button("Quit");
			quit.Clicked += OnClickQuit;
			quit.SetSizeRequest(80, 35);
			pnlButton.PackStart(quit, true, true, 5);

			VBox.PackStart(pnlAutor, true, false, 5);
			VBox.PackStart(pnlAnho, true, false, 5);
			VBox.PackStart(pnlTipo, true, false, 5);
			VBox.PackStart(pnlButton, true, false, 5);

			this.ShowAll();

		}

		private void OnClickBuscar(object o, EventArgs e){this.Buscar=1;this.Respond(ResponseType.Ok);this.Hide();}

		private void OnClickQuit(object o, EventArgs e){this.Buscar=0;this.Respond(ResponseType.Cancel);this.Hide();}
	}
}
