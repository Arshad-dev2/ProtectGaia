using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProtectGaia.Entities.WeatherEntity
{

    public  class WeatherResponse
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("current")]
        public Current Current { get; set; }

        //[JsonProperty("hourly")]
        //public List<Hourly> Hourly { get; set; }

        //[JsonProperty("daily")]
        //public List<Daily> Daily { get; set; }
        public WeatherResponse()
        {
            //Hourly = new List<Hourly>();
            //Daily = new List<Daily>();
        }
    }

    public partial class Current
    {
        [JsonProperty("dt")]
        public long Dt { get; set; }

        [JsonProperty("sunrise")]
        public long Sunrise { get; set; }

        [JsonProperty("sunset")]
        public long Sunset { get; set; }

        [JsonProperty("temp")]
        public double Temp { get; set; }

        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }

        [JsonProperty("pressure")]
        public long Pressure { get; set; }

        [JsonProperty("humidity")]
        public long Humidity { get; set; }

        [JsonProperty("dew_point")]
        public double DewPoint { get; set; }

        [JsonProperty("uvi")]
        public double Uvi { get; set; }

        [JsonProperty("clouds")]
        public long Clouds { get; set; }

        [JsonProperty("visibility")]
        public long Visibility { get; set; }

        [JsonProperty("wind_speed")]
        public double WindSpeed { get; set; }

        [JsonProperty("wind_deg")]
        public long WindDeg { get; set; }

        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; }
    }

    public partial class Weather
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("main")]
        public string Main { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }



}
