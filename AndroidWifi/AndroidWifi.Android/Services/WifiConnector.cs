using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Android.Content;
using Android.Net.Wifi;
using AndroidWifi.Models;
using AndroidWifi.Services;

namespace AndroidWifi.Droid.Services
{
    public class WifiConnector : IWifiConnector
    {
        private readonly Context _context;

        public async Task<bool> ConnectToWifi(WifiCredentials credentials)
        {
            if (credentials == null) { throw new ArgumentNullException(nameof(credentials));}

            var tcs = new TaskCompletionSource<bool>();

#pragma warning disable 4014
            Task.Run(async () =>
            {
                try
                {
                    var wifiConfig = new WifiConfiguration
                    {
                        Ssid = $"\"{credentials.Ssid}\"",
                        PreSharedKey = $"\"{credentials.Pwd}\""
                    };

                    WifiManager wifiManager = (WifiManager)_context.GetSystemService(Context.WifiService);

                    // Use ID
                    int netId = wifiManager.AddNetwork(wifiConfig);
                    wifiManager.Disconnect();
                    await Task.Delay(1000);
                    int disconnectedIp = wifiManager.ConnectionInfo?.IpAddress ?? 0;
                    int currentIp = disconnectedIp;
                    wifiManager.EnableNetwork(netId, true);
                    wifiManager.Reconnect();

                    for (int i = 0; i < 100; i++)
                    {
                        currentIp = wifiManager.ConnectionInfo?.IpAddress ?? 0;
                        if (currentIp != disconnectedIp)
                        {
                            await Task.Delay(1000);
                            break;
                        }
                        else
                        {
                            await Task.Delay(250);
                        }
                    }

                    tcs.SetResult(currentIp != disconnectedIp);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                    Debugger.Break();
                    tcs.SetResult(false);
                }
            });
#pragma warning restore 4014

            return await tcs.Task;
        }

        public WifiConnector(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}