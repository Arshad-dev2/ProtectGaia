using System;
namespace ProtectGaia.Entities.WeatherEntity
{
    public class WidgetResponse
    {
        public string Address { get; set; }
        public int Temp { get; set; }
        public int FeelsLikeTemp { get; set; }
        public string Icon { get; set; }
        public double Ozone { get; set; }
        public string Aqi { get; set; }

        public WidgetResponse()
        {
    }
    }
}
