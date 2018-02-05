using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Wifi_bot.Models
{
    public class WifiModel
    {
        [JsonProperty(PropertyName = "NAME")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "ADDRESS")]
        public string Address { get; set; }
        [JsonProperty(PropertyName = "LAT")]
        public decimal Lat { get; set; }
        [JsonProperty(PropertyName = "LNG")]
        public decimal Lng { get; set; }
        [JsonProperty(PropertyName = "PASSWORD")]
        public string Password { get; set; }
        [JsonProperty(PropertyName = "CATEGORY")]
        public string Category { get; set; }
    }
}