using System;
namespace ProtectGaia.Entities.WeatherEntity
{
    public class WeatherRequest
    {

        public string ApiKey { get; set; }
        public string Location { get; set; }
        public string Date { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public WeatherRequest()
        {
        }
    }
}
