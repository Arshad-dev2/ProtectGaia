using System;
using System.Net;
using Newtonsoft.Json;
using ProtectGaia.Entities.WeatherEntity;
using ProtectGaia.Interface;

namespace ProtectGaia.DataAccess
{
    public class WeatherApi : IWeatherApi
    {

        public readonly string apiKey = "abe86acc75901d9c33ecbd8fd859755a";
        public readonly string BaseURL = @"https://api.openweathermap.org/data/2.5/onecall?";


        public readonly string locationURL = @"https://api.opencagedata.com/geocode/v1/json?key=";
        public readonly string locationApiKey = "5d8a908231ed4cdeb254dbc4b6ba1ffe";

        public readonly string ozoneUrl = @"https://api.waqi.info/feed/";
        public readonly string ozoneApikey = "4ee533727e6e053924140c344c48caa41463da4d";

        public WidgetResponse GetWeather(WeatherRequest request)
        {
            WidgetResponse widgetResponse = new WidgetResponse();

            try
            {
                WeatherResponse weatherResponse = new WeatherResponse();
                var url = string.Format("{0}lat={1}&lon={2}&appid={3}", BaseURL, request.Latitude, request.Longitude, apiKey);

                var jsonString = new WebClient().DownloadString(url);
                weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(jsonString);
                widgetResponse.Temp = Convert.ToInt32(weatherResponse.Current.Temp - 273);
                widgetResponse.FeelsLikeTemp = Convert.ToInt32(weatherResponse.Current.FeelsLike - 273);
                widgetResponse.Icon = @"https://openweathermap.org/img/wn/" + weatherResponse.Current.Weather[0].Icon + "@2x.png";
                // Locationfetch

                string locUrl = string.Format("{0}{1}&q={2}+{3}&pretty=1&no_annotations=1", locationURL, locationApiKey, request.Latitude, request.Longitude);
                var locationJson = new WebClient().DownloadString(locUrl);
                LocationEntity locationResponse = new LocationEntity();
                locationResponse = JsonConvert.DeserializeObject<LocationEntity>(locationJson);
                string station = string.Empty;
                // Ozone value Fetch
                if (locationResponse != null && locationResponse.Status.Code == 200)
                {
                    if (locationResponse.Results != null &&
                        locationResponse.Results[0].Components != null)
                    {

                        var postCode = Convert.ToInt32(locationResponse.Results[0].Components.Postcode.Split(' ')[locationResponse.Results[0].Components.Postcode.Split(' ').Length - 1]);
                        widgetResponse.Address = string.Format("{0},{1}", locationResponse.Results[0].Components.Suburb, locationResponse.Results[0].Components.Country);

                        if (postCode >= 3000 && postCode <= 3999)
                        {
                            station = "Melbourne";

                        }
                        //2000—2599 2619—2899 2921—2999
                        else if ((postCode >= 2000 && postCode <= 2599) || (postCode >= 2619 && postCode <= 2899) || (postCode >= 2921 && postCode <= 2999))
                        {
                            station = "Sydney";
                        }
                        //7000—7799
                        else if (postCode >= 7000 && postCode <= 7799)
                        {
                            station = "hobart";
                        }
                        //2600—26182900—2920
                        else if ((postCode >= 2600 && postCode <= 2618) || (postCode >= 2900 && postCode <= 2920))
                        {
                            station = "Canberra";
                        }
                        //5000—5799
                        else if (postCode >= 5000 && postCode <= 5799)
                        {
                            station = "Adelaide";
                        }
                        //6000—6797
                        else if (postCode >= 6000 && postCode <= 6797)
                        {
                            station = "australia/western/camersham";
                        }
                        //4000—4999
                        else if (postCode >= 4000 && postCode <= 4999)
                        {
                            station = "Brisbane";
                        }
                        //0800—0899
                        else if (postCode >= 0800 && postCode <= 0899)
                        {
                            station = "Darwin";
                        }
                        else
                        {
                            station = "Melbourne";
                        }
                    }

                }
                string ozoneString = string.Format("{0}{1}/?token={2}", ozoneUrl, station, ozoneApikey);
                var ozoneJsonString = new WebClient().DownloadString(ozoneString);
                OzoneEntity ozoneResponse = new OzoneEntity();
                ozoneResponse = JsonConvert.DeserializeObject<OzoneEntity>(ozoneJsonString);
                widgetResponse.Ozone = ozoneResponse.Data.Iaqi.O3.V;
                widgetResponse.Aqi = ozoneResponse.Data.Aqi;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return widgetResponse;
        }

        public WeatherApi()
        {
        }
    }
}
