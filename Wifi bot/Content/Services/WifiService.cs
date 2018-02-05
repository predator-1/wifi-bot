using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Wifi_Bot.Content.Models;

namespace Wifi_Bot.Content.Services
{
    public class WifiService
    {
        public WifiService()
        {
            
        }


        private async Task<List<WifiModel>> GetWifi(decimal lat, decimal lng, int distance)
        {
            var postBody = $"LAT={lat}&LNG={lng}&distance={distance}";
            var responce = await Post("http://185.231.70.111/getwifi.php", postBody);
            if (!string.IsNullOrEmpty(responce))
            {
                return JsonConvert.DeserializeObject<List<WifiModel>>(responce);
            }
            return new List<WifiModel>();
        }

        private async Task<string> Post(string url, string post)
        {
            using (var handler = new HttpClientHandler())
            {
                handler.ClientCertificateOptions = ClientCertificateOption.Automatic;
                using (var httpClient = new HttpClient(handler))
                {
                    httpClient.Timeout = new TimeSpan(0, 0, 1, 5);
                    httpClient.DefaultRequestHeaders.Add("User-Agent",
                        "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36");
                    byte[] contBytes = Encoding.UTF8.GetBytes(post);
                    HttpContent cont = new ByteArrayContent(contBytes);
                    cont.Headers.ContentType =
                        new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                    var responce = await httpClient.PostAsync(url, cont);
                    if (responce.IsSuccessStatusCode)
                    {
                        var resp = await responce.Content.ReadAsByteArrayAsync();
                        return Encoding.UTF8.GetString(resp);
                    }
                    return "";
                }
            }           
        }
    }
}