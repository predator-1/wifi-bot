using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wifi_bot.Models
{
    public class TelegramGeoModel
    {
        [JsonProperty(PropertyName = "geo")]
        public Geo Geo { get; set; }
    }

   
    public class Geo
    {
        [JsonProperty(PropertyName = "elevation")]
        public decimal Elevation { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public decimal Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public decimal Longitude { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }

}