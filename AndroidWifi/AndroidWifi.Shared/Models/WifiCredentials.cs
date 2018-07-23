using System;
using AndroidWifi.Conversion;
using Newtonsoft.Json;

namespace AndroidWifi.Models
{
    public class WifiCredentials
    {
        private string _ssid = String.Empty;
        private string _pwd = String.Empty;

        public string Ssid
        {
            get => _ssid;
            set => _ssid = value ?? String.Empty;
        }

        public string Pwd
        {
            get => _pwd;
            set => _pwd = value ?? String.Empty;
        }

        public string ToBase40()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.None);
            using (var base40 = new Base40Converter())
            {
                return base40.ToBase40String(json);
            }
        }

        public static WifiCredentials FromBase40(string base40Text)
        {
            using (var base40 = new Base40Converter())
            {
                string json = base40.StringFromBase40(base40Text);
                return JsonConvert.DeserializeObject<WifiCredentials>(json);
            }
        }
    }
}
