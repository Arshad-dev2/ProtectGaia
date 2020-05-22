using System;
using ProtectGaia.Entities.WeatherEntity;

namespace ProtectGaia.Interface
{
    public interface IWeatherApi
    {
       WidgetResponse GetWeather(WeatherRequest weatherRequest);
    }
}
