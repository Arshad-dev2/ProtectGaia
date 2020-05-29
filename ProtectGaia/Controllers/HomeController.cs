using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProtectGaia.ActionFilters;
using ProtectGaia.Entities.NewsEntity;
using ProtectGaia.Entities.WeatherEntity;
using ProtectGaia.Interface;
using ProtectGaia.Interfaces;
using ProtectGaia.Models;

namespace ProtectGaia.Controllers
{
    /// <summary>
    /// Home Contrller Navigates through all  pages
    /// </summary>
    [BasicAuthenticationAttribute("ecoMorph", "Password123", BasicRealm = "ecoMorph")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INewsApi _newsApi;
        private readonly IWeatherApi _weatherApi;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private readonly IUser _user;
        private readonly IChallenge _challenge;

        public HomeController(ILogger<HomeController> logger, INewsApi _newsApi, IWeatherApi _weatherApi, IHttpContextAccessor httpContextAccessor, IUser _user, IChallenge _challenge)
        {
            _logger = logger;
            this._newsApi = _newsApi;
            this._weatherApi = _weatherApi;
            this._httpContextAccessor = httpContextAccessor;
            this._user = _user;
            this._challenge = _challenge;

        }

        /// <summary>
        /// Index page with News API
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
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
                newsRequest.queryString = HttpUtility.UrlEncode("greenhouse-gas+climate-change");
                newsRequest.language = "en";
                newsRequest.sortby = "publishedAt";
                try
                {
                    newsResponse = _newsApi.UpcomingNews(newsRequest);
                    string output = JsonConvert.SerializeObject(newsResponse);
                    _session.SetString("News", output);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.StackTrace);
                }
            }
            // If the newsAPI is retrieves, status will be ok
            if (newsResponse!=null && newsResponse.Status.ToLower() == "ok" && newsResponse.Articles != null && newsResponse.Articles.Count() > 0)
            {
                indexViewModel.ArticleList = newsResponse.Articles.Select(x => new Models.Article()
                {
                    Title = x.Title,
                    ImageUrl = x.UrlToImage != null ? x.UrlToImage.OriginalString : string.Empty,
                    PublishedDate = TimeCalculator(DateTime.Now, x.PublishedAt.DateTime),
                    Source = x.Source.Name,
                    Url = x.Url.AbsoluteUri
                }).ToList();
            }
           
            return View(indexViewModel.ArticleList);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string userEmail, string userName,string userImageUrl)
        {
            
            UserModel userModel = new UserModel();
            if (!string.IsNullOrEmpty(userEmail) || !string.IsNullOrEmpty(userName))
            {
                _session.SetString("userEmail", userEmail);
                _session.SetString("userName", userName);
                _session.SetString("userImageUrl", userImageUrl);
                _session.SetString("IsLoggedIn", "true");

                if (!_user.UserExists(userEmail))
                {
                    userModel.UserEmail = userEmail;
                    userModel.UserName = !string.IsNullOrEmpty(userName)?userName:userEmail;
                    userModel.UserImageUrl = !string.IsNullOrEmpty(userImageUrl) ? userImageUrl : string.Empty;
                    userModel.LevelId = 1;
                    userModel.TotalTaskCompleted=0;
                    userModel.LevelCompletedTask= 0;
                    userModel.PendingTask = 4;
                    userModel.TotalPointScored=0;
                    userModel.CarbonScore=0;
                    userModel.LastModified = DateTime.Now;
                    userModel.CarbonActivity = string.Empty;
                    userModel.IsFirstTimeLogin = true;
                    IDictionary<string, int> Activity_dict = new Dictionary<string, int>();
                    // Activity will have currenttimestamp and total points scored
                    Activity_dict.Add(DateTime.Now.ToString(), userModel.TotalPointScored);
                     var activity_str = JsonConvert.SerializeObject(Activity_dict);
                    userModel.Activity= activity_str;
                    userModel.CarbonActivity = string.Empty;
                    userModel = await _user.CreateUserAsync(userModel);
                    _session.SetString("UserModel", JsonConvert.SerializeObject(userModel));
                }
                else
                {
                    userModel = await _user.GetUserAsync(userEmail);
                    userModel.IsFirstTimeLogin=false;
                    _session.SetString("UserModel", JsonConvert.SerializeObject(userModel));
                }
            }
            else
            {
                return Redirect("/");
            }
            return Json(new { newUrl = Url.Action("Index", "Dashboard") });


        }


        /// <summary>
        /// Get the Location of the user based on the user consent and call the APIs for the widget
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
                catch (Exception ex)
                {
                    _logger.LogError(ex.StackTrace);
                }
            }
            return Json(new { isSucess = true, location = widgetResponse });
        }


        public IActionResult SignOut(string userEmail)
        {
            _session.Clear();
            return Json(new { newUrl = Url.Action("Index", "Home") });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
           // return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

         var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.Title = "404 Page not Found";
                    ViewBag.ErrorMessage = "Oops, The Page you are looking for can't be found!";
                    break;
                case 500:
                    ViewBag.Title = "500 Internal Server Error";
                    ViewBag.ErrorMessage = "The requested url is temporarily unavailable";
                    break;
            }
            return View("NotFound");
        }

        /// <summary>
        /// Published Post time calculator
        /// </summary>
        /// <param name="cDate"></param>
        /// <param name="pDate"></param>
        /// <returns></returns>
        private string TimeCalculator(DateTime cDate, DateTime pDate)
        {
            string days;
            TimeSpan diff2 = cDate - pDate;
            if (diff2.TotalDays > 1)
            {
                days = string.Format("{0} days ago", Convert.ToUInt32(diff2.TotalDays).ToString());
            }
            else
            {
                if (diff2.TotalHours > 0)
                {
                    days = string.Format("{0} hours ago", Convert.ToUInt32(diff2.TotalHours).ToString());
                }
                else
                {
                    if (diff2.TotalMinutes > 0)
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



    }
}
