using System;
using Gtk;
using Gdk;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.GtkSharp;
using OxyPlot.Axes;


namespace IntegracionFinal
{
    /***
     * Esta clase se encarga de generar los gráficos necesarios para los informes.
     */
	public class Grafico
	{
        //Ejemplo para un gráfico de tipo tarta
		public static Gtk.Image GraficoPieExample()
		{
			var model = new PlotModel();//("World population by continent");
										// http://www.nationsonline.org/oneworld/world_population.htm
										// http://en.wikipedia.org/wiki/Continent

			var ps = new PieSeries();
			ps.Slices.Add(new PieSlice("Africa", 1030) { IsExploded = true });
			ps.Slices.Add(new PieSlice("Americas", 929) { IsExploded = true });
			ps.Slices.Add(new PieSlice("Asia", 4157) { IsExploded = true });
			ps.Slices.Add(new PieSlice("Europe", 739) { IsExploded = true });
			ps.Slices.Add(new PieSlice("Oceania", 35) { IsExploded = true });
			ps.InnerDiameter = 0;
			ps.ExplodedDistance = 0.0;
			ps.Stroke = OxyColors.White;
			ps.StrokeThickness = 2.0;
			ps.InsideLabelPosition = 0.8;
			ps.AngleSpan = 360;
			ps.StartAngle = 0;
			model.Series.Add(ps);
			PngExporter.Export(model, "plot", 600, 400, null);
			Gdk.Pixbuf a = new Gdk.Pixbuf("plot");
			return new Gtk.Image(a);
		}
        //Ejemplo para un gráfico de barras
		public static Gtk.Image GraficoBarrasExample()
		{

			var modela = new PlotModel
			{
				Title = "BarSeries",
				LegendPlacement = LegendPlacement.Outside,
				LegendPosition = LegendPosition.BottomCenter,
				LegendOrientation = LegendOrientation.Horizontal,
				LegendBorderThickness = 0
			};

			var s1 = new BarSeries { Title = "Series 1", StrokeColor = OxyColors.Black, StrokeThickness = 1 };
			s1.Items.Add(new BarItem { Value = 25 });
			s1.Items.Add(new BarItem { Value = 1 });
			s1.Items.Add(new BarItem { Value = 18 });
			s1.Items.Add(new BarItem { Value = 40 });

			var s2 = new BarSeries { Title = "Series 2", StrokeColor = OxyColors.Black, StrokeThickness = 1 };
			s2.Items.Add(new BarItem { Value = 12 });
			s2.Items.Add(new BarItem { Value = 14 });
			s2.Items.Add(new BarItem { Value = 12 });
			s2.Items.Add(new BarItem { Value = 26 });

			var categoryAxis = new CategoryAxis { Position = AxisPosition.Left };
			categoryAxis.Labels.Add("Category A");
			categoryAxis.Labels.Add("Category B");
			categoryAxis.Labels.Add("Category C");
			categoryAxis.Labels.Add("Category D");
			var valueAxis = new LinearAxis { Position = AxisPosition.Bottom, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0 };
			modela.Series.Add(s1);
			modela.Series.Add(s2);
			modela.Axes.Add(categoryAxis);
			modela.Axes.Add(valueAxis);

			PngExporter.Export(modela, "plot", 600, 400, null);
			Gdk.Pixbuf a = new Gdk.Pixbuf("plot");
			return new Gtk.Image(a);
		}
        /***
         * Esta funcion recibe:
         *   (String)nombreMiembro <- El nombre del autor miembro del departamento
         *   (int[])values <- Un array de 12 posiciones con todos los valores para un año en cuestion
         *   (int)year <- El año a consultar
         *   
         *  Devuelve:
         *    Gtk.Image <- Una imagen con el gráfico generado
         */
		public static Gtk.Image merMiembroMes(string nombreMiembro, int[] values, int year)
		{
            //Se inicializa el modelo con un título
			var modelo = new PlotModel
			{
				Title = "Méritos para " + nombreMiembro + " en " + Convert.ToString(year)
			};
            //Para cada valor en el array recibido se añade su valor a una nueva columna (grafico de barras)
			var c1 = new ColumnSeries();
			foreach (int valor in values)
			{
				c1.Items.Add(new ColumnItem { Value = valor, Color = OxyColor.FromRgb(50, 200, 250) });
			}
			modelo.Series.Add(c1);

            //Se especifican los valores del eje X (El eje Y tiene un valor numérico que se modifica de forma dinámica)
			var mesAxis = new CategoryAxis { Position = AxisPosition.Bottom };
            mesAxis.Labels.Add("Enero");
			mesAxis.Labels.Add("Febrero");
			mesAxis.Labels.Add("Marzo");
			mesAxis.Labels.Add("Abril");
			mesAxis.Labels.Add("Mayo");
			mesAxis.Labels.Add("Junio");
			mesAxis.Labels.Add("Julio");
			mesAxis.Labels.Add("Agosto");
			mesAxis.Labels.Add("Sept.");
			mesAxis.Labels.Add("Octubre");
			mesAxis.Labels.Add("Novi.");
			mesAxis.Labels.Add("Diciembre");
			modelo.Axes.Add(mesAxis);

            //Genera el modelo y lo exporta a una imagen con un tamaño específico
			PngExporter.Export(modelo, "plot", 600, 350, null);
			Gdk.Pixbuf toRet = new Gdk.Pixbuf("plot");
			return new Gtk.Image(toRet);

		}
	}
}
