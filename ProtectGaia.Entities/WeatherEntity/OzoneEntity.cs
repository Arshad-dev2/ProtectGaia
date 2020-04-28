using System;
using Newtonsoft.Json;

namespace ProtectGaia.Entities.WeatherEntity
{
    public partial class OzoneEntity
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("aqi")]
        public string Aqi { get; set; }

        [JsonProperty("idx")]
        public string Idx { get; set; }


        [JsonProperty("dominentpol")]
        public string Dominentpol { get; set; }

        [JsonProperty("iaqi")]
        public Iaqi Iaqi { get; set; }
    }

    public partial class Iaqi
    {
        [JsonProperty("co")]
        public Co Co { get; set; }

        [JsonProperty("h")]
        public Co H { get; set; }

        [JsonProperty("no2")]
        public Co No2 { get; set; }

        [JsonProperty("o3")]
        public Co O3 { get; set; }

        [JsonProperty("p")]
        public Co P { get; set; }

        [JsonProperty("pm10")]
        public Co Pm10 { get; set; }

        [JsonProperty("pm25")]
        public Co Pm25 { get; set; }

        [JsonProperty("so2")]
        public Co So2 { get; set; }

        [JsonProperty("t")]
        public Co T { get; set; }

        [JsonProperty("w")]
        public Co W { get; set; }

        [JsonProperty("wd")]
        public Co Wd { get; set; }

        [JsonProperty("wg")]
        public Co Wg { get; set; }
    }

    public partial class Co
    {
        [JsonProperty("v")]
        public double V { get; set; }
    }
}
