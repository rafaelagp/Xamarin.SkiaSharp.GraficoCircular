# Xamarin.SkiaSharp.GraficoCircular
Controle para criar gráficos circulares em Xamarin.Forms usando SkiaSharp.

### Dependências
Adicionar em todos os projetos (PCL e de plataforma), os nugets
* SkiaSharp
* SkiaSharp.Views.Forms

Adicionar somente nos projetos de plataforma, o nuget
* SkiaSharp.Views

### Fator de Conversão
SkiaSharp utiliza valores em pixels e Xamarin.Forms utiliza Device Independent Points, então é preciso criar uma propriedade que receba o fator de conversão da plataforma.

No projeto PCL, em App.xaml.cs, adicione a seguinte propriedade:
```csharp
public static double FatorDeEscalaDeTela { get; set; }
```
No projeto Android, em MainActivity.cs, adicione a seguinte linha em seu método OnCreate:
```csharp
App.FatorDeEscalaDeTela = Resources.DisplayMetrics.Density;
```
No projeto iOS, em AppDelegate.cs, adicione a seguinte linha em seu método FinishedLaunching:
```csharp
App.FatorDeEscalaDeTela = UIScreen.MainScreen.Scale;
```
