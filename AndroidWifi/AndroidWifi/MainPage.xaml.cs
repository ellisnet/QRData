using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using AndroidWifi.Models;
using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;

namespace AndroidWifi
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

	    private async void ScanButton_OnClicked(object sender, EventArgs e)
	    {
	        scanButton.IsEnabled = false;

	        var scanner = new MobileBarcodeScanner();
	        Result scanResult = await scanner.Scan();

	        if (scanResult != null)
	        {
	            await Task.Delay(150);  //Have to wait for the scan window to disappear
	            var wifiCreds = WifiCredentials.FromBase40(scanResult.Text);

	            if (wifiCreds?.Ssid != null)
	            {
	                await UserDialogs.Instance.AlertAsync($"Wifi information received - will now attempt to connect to {wifiCreds.Ssid}");

	                // ReSharper disable once PossibleNullReferenceException
	                var connected = await (Application.Current as App).WifiConnector.ConnectToWifi(wifiCreds);

	                if (connected)
	                {
	                    await UserDialogs.Instance.AlertAsync($"Successfully connected to {wifiCreds.Ssid}");
	                }
                }
            }

	        scanButton.IsEnabled = true;
	    }
	}
}
