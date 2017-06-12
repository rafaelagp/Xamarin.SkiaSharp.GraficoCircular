using Xamarin.Forms;

namespace GraficoCircular
{
	public partial class App : Application
	{
		public static double FatorDeEscalaDeTela { get; set; }

		public App()
		{
			InitializeComponent();
			MainPage = new NavigationPage(new GraficoCircularPage());
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
