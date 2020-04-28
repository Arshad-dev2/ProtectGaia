using System;
using System.Diagnostics;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProtectGaia.Entities.NewsEntity;
using ProtectGaia.Interface;
using ProtectGaia.Models;
using MetOfficeDataPoint;
using MetOfficeDataPoint.Models;
using MetOfficeDataPoint.Models.GeoCoordinate;
using ProtectGaia.Entities.WeatherEntity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ProtectGaia.Controllers
{
    /// <summary>
    /// Home Contrller Navigates through all  pages
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INewsApi _newsApi;
        private readonly IWeatherApi _weatherApi;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public HomeController(ILogger<HomeController> logger, INewsApi _newsApi,IWeatherApi _weatherApi, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            this._newsApi = _newsApi;
            this._weatherApi = _weatherApi;
            this._httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Index page with News API
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public  IActionResult Index()
        {
          
            IndexViewModel indexViewModel = new IndexViewModel();
            NewsRequest newsRequest = new NewsRequest();
            var newsResponse = new NewsResponse();
            // Session Check for the user to consume the News API. If already retrieved, Get from session else call the API
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("News")))
            {
                newsResponse = JsonConvert.DeserializeObject<NewsResponse>(_session.GetString("News"));
            }
            else
            {
                newsRequest.queryString = HttpUtility.UrlEncode("carbon emission Environment climate change");
                newsRequest.domainList.Add("abc.net.au");
                newsRequest.domainList.Add("9news.com.au");
                newsRequest.language = "en";
                newsRequest.sortby = "popularity";
                try
                {
                    newsResponse = _newsApi.UpcomingNews(newsRequest);
                    string output = JsonConvert.SerializeObject(newsResponse);
                    _session.SetString("News", output);
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.StackTrace);
                }
            }
            // If the newsAPI is retrieves, status will be ok
            if (newsResponse.Status.ToLower() == "ok" && newsResponse.Articles!=null && newsResponse.Articles.Count()>0)
            {
                indexViewModel.ArticleList = newsResponse.Articles.Select(x => new Models.Article()
                {Title = x.Title,
                 ImageUrl = x.UrlToImage!=null? x.UrlToImage.ToString():string.Empty,
                 PublishedDate = TimeCalculator(DateTime.UtcNow,x.PublishedAt.UtcDateTime),
                  Source=x.Source.Name,
                   Url = x.Url.AbsoluteUri
                }).ToList();
            }
            return View(indexViewModel.ArticleList);
        }

        /// <summary>
        /// Published Post time calculator
        /// </summary>
        /// <param name="cDate"></param>
        /// <param name="pDate"></param>
        /// <returns></returns>
        private string TimeCalculator(DateTime cDate,DateTime pDate)
        {
            string days;
            TimeSpan diff2 = cDate - pDate;
            if (diff2.TotalDays > 0)
            {
                days = string.Format("{0} days ago", Convert.ToUInt32(diff2.TotalDays).ToString());
            }
            else
            {
                if(diff2.TotalHours > 0)
                {
                days = string.Format("{0} hours ago", Convert.ToUInt32(diff2.TotalHours).ToString());
                }
                else
                {
                    if(diff2.TotalMinutes > 0)
                    {
                        days = string.Format("{0} Minutes ago", Convert.ToUInt32(diff2.TotalMinutes).ToString());

                    }
                    else
                    {
                        days = "few seconds ago";
                    }
                }
            }
            return days;
        }

        /// <summary>
        /// Get the Location of the user based on the user consent & call the APIs for the widget
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetLocation(string location)
        {
            // IF the User denied the location access, Widget will return unavailable.
            if (location == "UnAvailable")
            {
                return Json(new { isSucess = false, location = "" });
            }
            // Else continue calling the API
            WidgetResponse widgetResponse = new WidgetResponse();
            // Get if the session is available
            if (_session != null && !string.IsNullOrEmpty(_session.GetString("Location")))
            {
                widgetResponse.Address = _session.GetString("Address");
                widgetResponse.FeelsLikeTemp = Convert.ToInt32(_session.GetString("FeelsLikeTemp"));
                widgetResponse.Icon = _session.GetString("Icon");
                widgetResponse.Ozone = Convert.ToDouble(_session.GetString("Ozone"));
                widgetResponse.Temp = Convert.ToInt32(_session.GetString("Temp"));
                widgetResponse.Aqi = _session.GetString("Aqi");
            }
            else
            {
                _session.SetString("Location", location);
                ViewData["Location"] = location;
                WeatherRequest weatherRequest = new WeatherRequest();
                weatherRequest.Latitude = Convert.ToDouble(location.Split(',')[0]);
                weatherRequest.Longitude = Convert.ToDouble(location.Split(',')[1]);
                try
                {
                    widgetResponse = _weatherApi.GetWeather(weatherRequest);
                    _session.SetString("Address", widgetResponse.Address);
                    _session.SetString("FeelsLikeTemp", widgetResponse.FeelsLikeTemp.ToString());
                    _session.SetString("Icon", widgetResponse.Icon);
                    _session.SetString("Ozone", widgetResponse.Ozone.ToString());
                    _session.SetString("Temp", widgetResponse.Temp.ToString());
                    _session.SetString("Aqi", widgetResponse.Aqi.ToString());
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.StackTrace);
                }
            }
            return Json(new{isSucess=true,location= widgetResponse });
        }

       
        //public IActionResult Contact()
        //{
            
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
