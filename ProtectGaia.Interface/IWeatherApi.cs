using System;
using ProtectGaia.Entities.WeatherEntity;

namespace ProtectGaia.Interface
{
    public interface IWeatherApi
    {
        public WidgetResponse GetWeather(WeatherRequest weatherRequest);
    }
}
