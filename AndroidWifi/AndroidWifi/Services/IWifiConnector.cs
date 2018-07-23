using System.Threading.Tasks;
using AndroidWifi.Models;

namespace AndroidWifi.Services
{
    public interface IWifiConnector
    {
        Task<bool> ConnectToWifi(WifiCredentials credentials);
    }
}
