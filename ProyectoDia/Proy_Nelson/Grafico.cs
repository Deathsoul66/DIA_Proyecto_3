using System;
using Gtk;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.GtkSharp;
using OxyPlot.Axes;


namespace Proy_Nelson
{
	public class Grafico
	{
		public static Image GraficoPieExample()
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
			return new Image(a);
		}

		public static Image GraficoBarrasExample()
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
			return new Image(a);
		}

		public static Image merMiembroMes(string nombreMiembro, int[] values, int year)
		{

			var modelo = new PlotModel
			{
				Title = "Méritos para " + nombreMiembro + " en " + Convert.ToString(year)
			};

			var c1 = new ColumnSeries();
			foreach (int valor in values)
			{
				c1.Items.Add(new ColumnItem { Value = valor, Color = OxyColor.FromRgb(50, 200, 250) });
			}
			modelo.Series.Add(c1);

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

			PngExporter.Export(modelo, "plot", 600, 400, null);
			Gdk.Pixbuf toRet = new Gdk.Pixbuf("plot");
			return new Image(toRet);

		}
	}
}
