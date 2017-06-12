using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace GraficoCircular
{
	public class GraficoCircular : ContentView
	{
		public int Porcentagem
		{
			get { return (int)GetValue(PorcentagemProperty); }
			set { SetValue(PorcentagemProperty, value); }
		}

		public readonly BindableProperty PorcentagemProperty = BindableProperty.Create(nameof(Porcentagem),
																						typeof(int),
																						typeof(ContentView),
																						(int)100,
																						BindingMode.OneWay);

		public float TamanhoDaFonte
		{
			get { return (float)GetValue(TamanhoDaFonteProperty); }
			set { SetValue(TamanhoDaFonteProperty, value); }
		}

		public readonly BindableProperty TamanhoDaFonteProperty = BindableProperty.Create(nameof(TamanhoDaFonte),
																							typeof(float),
																							typeof(ContentView),
																							(float)9,
																							BindingMode.OneWay);

		public float EspessuraDeFundo
		{
			get { return (float)GetValue(EspessuraDeFundoProperty); }
			set { SetValue(EspessuraDeFundoProperty, value); }
		}

		public readonly BindableProperty EspessuraDeFundoProperty = BindableProperty.Create(nameof(EspessuraDeFundo),
																							typeof(float),
																							typeof(ContentView),
																							(float)12,
																							BindingMode.OneWay);

		public float EspessuraDePreenchimento
		{
			get { return (float)GetValue(EspessuraDePreenchimentoProperty); }
			set { SetValue(EspessuraDePreenchimentoProperty, value); }
		}

		public readonly BindableProperty EspessuraDePreenchimentoProperty = BindableProperty.Create(nameof(EspessuraDePreenchimento),
																									typeof(float),
																									typeof(ContentView),
																									(float)12,
																									BindingMode.OneWay);

		public Color CorDeFundo
		{
			get { return (Color)GetValue(CorDeFundoProperty); }
			set { SetValue(CorDeFundoProperty, value); }
		}

		public readonly BindableProperty CorDeFundoProperty = BindableProperty.Create(nameof(CorDeFundo),
																					  typeof(Color),
																					  typeof(ContentView),
																					  Color.LightGray,
																					  BindingMode.OneWay);

		public Color CorDePreenchimento
		{
			get { return (Color)GetValue(CorDePreenchimentoProperty); }
			set { SetValue(CorDePreenchimentoProperty, value); }
		}

		public readonly BindableProperty CorDePreenchimentoProperty = BindableProperty.Create(nameof(CorDePreenchimento),
																							  typeof(Color),
																							  typeof(ContentView),
																							  Color.Navy,
																							  BindingMode.OneWay);

		public Color CorDoCanvas
		{
			get { return (Color)GetValue(CorDoCanvasProperty); }
			set { SetValue(CorDoCanvasProperty, value); }
		}

		public readonly BindableProperty CorDoCanvasProperty = BindableProperty.Create(nameof(CorDoCanvas),
																					   typeof(Color),
																					   typeof(ContentView),
																					   Color.Transparent,
																					   BindingMode.OneWay);

		public GraficoCircular()
		{
			var view = new SKCanvasView();
			view.PaintSurface += View_PaintSurface;

			Content = view;
		}

		void View_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
		{
			var canvas = e.Surface.Canvas;
			canvas.Clear(CorDoCanvas.ToSKColor());

			var canvasAltura = e.Info.Height;
			var canvasLargura = e.Info.Width;

			var espessuraDeFundo = DpParaPixel(EspessuraDeFundo);
			var espessuraDePreenchimento = DpParaPixel(EspessuraDePreenchimento);

			var fundo = new SKRect(espessuraDeFundo,
								   espessuraDeFundo,
								   canvasLargura - espessuraDeFundo,
								   canvasAltura - espessuraDeFundo);
			DesenharFundo(canvas,
						  fundo,
						  espessuraDeFundo);
			DesenharPreenchimento(canvas,
								  fundo,
								  espessuraDePreenchimento);
			EscreverPorcentagem(canvas,
								canvasLargura,
								canvasAltura);
		}

		void DesenharFundo(SKCanvas canvas, SKRect fundo, float espessuraDeFundo)
		{
			var fundoPaint = new SKPaint
			{
				IsAntialias = true,
				Style = SKPaintStyle.Stroke,
				Color = CorDeFundo.ToSKColor(),
				StrokeWidth = espessuraDeFundo
			};

			canvas.DrawOval(fundo, fundoPaint);
		}

		void DesenharPreenchimento(SKCanvas canvas, SKRect fundo, float espessuraDePreenchimento)
		{
			float anguloInicial = 270;
			float anguloSweep = -(((Porcentagem * 360) / 100) - 0.01f);

			var preenchimentoPaint = new SKPaint
			{
				IsAntialias = true,
				Style = SKPaintStyle.Stroke,
				Color = CorDePreenchimento.ToSKColor(),
				StrokeWidth = espessuraDePreenchimento
			};

			var preenchimentoForma = new SKPath();
			preenchimentoForma.ArcTo(fundo, anguloInicial, anguloSweep, false);

			canvas.DrawPath(preenchimentoForma, preenchimentoPaint);
		}

		void EscreverPorcentagem(SKCanvas canvas, int canvasLargura, int canvasAltura)
		{
			var porcentagemPaint = new SKPaint
			{
				IsAntialias = true,
				Style = SKPaintStyle.StrokeAndFill,
				Color = CorDePreenchimento.ToSKColor(),
				FakeBoldText = true
			};

			var porcentagemTexto = $"{Porcentagem}%";
			var medidaPorcentagem = porcentagemPaint.MeasureText(porcentagemTexto);

			porcentagemPaint.TextSize = 0.6f * canvasLargura * TamanhoDaFonte / medidaPorcentagem;

			SKRect porcentagemBounds;
			porcentagemPaint.MeasureText(porcentagemTexto, ref porcentagemBounds);

			canvas.DrawText(porcentagemTexto,
							(canvasLargura / 2) - porcentagemBounds.MidX,
							(canvasAltura / 2) - porcentagemBounds.MidY,
							porcentagemPaint);
		}

		int DpParaPixel(float valorDp)
		{
			return (int)Math.Round(App.FatorDeEscalaDeTela * valorDp);
		}
	}
}
