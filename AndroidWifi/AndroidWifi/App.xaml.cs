using System;
using AndroidWifi.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace AndroidWifi
{
	public partial class App : Application
	{
        public IWifiConnector WifiConnector { get; }

		public App (IWifiConnector connector)
		{
		    WifiConnector = connector ?? throw new ArgumentNullException(nameof(connector));
			InitializeComponent();
            MainPage = new MainPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
