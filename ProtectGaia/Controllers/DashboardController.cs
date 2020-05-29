using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProtectGaia.Interfaces;
using ProtectGaia.Models;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProtectGaia.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private readonly IUser _user;
        private readonly IChallenge _challenge;
        public DashboardController(ILogger<DashboardController> _logger, IHttpContextAccessor httpContextAccessor, IUser _user, IChallenge _challenge)
        {
            this._logger = _logger;
            this._httpContextAccessor = httpContextAccessor;
            this._user = _user;
            this._challenge = _challenge;
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            TempData["isUpdate"] = false.ToString();
            if (_session != null && _session.GetString("UserModel") != null)
            {
                UserModel userModel = JsonConvert.DeserializeObject<UserModel>(_session.GetString("UserModel"));
                UserViewModel userViewModel = new UserViewModel();
                if (userModel == null)
                {
                    return Redirect("/home/index");
                }
                userViewModel.userModel = userModel;
                var challenges = _challenge.GetChallengesByLevelIdAsync(userModel.LevelId);
                userViewModel.ChallengeTitle = _challenge.GetChallengesByLevelIdAsync(userModel.LevelId).Select(x =>
                     x.ChallengeTitle
                ).ToList();
                
                return View(userViewModel);
            }
            else
            {
                return Redirect("/home/index");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Index(UserModel userModel)
        {
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.IsPost = true;
            userViewModel.IsErrorException = false;
            userViewModel.userModel.IsFirstTimeLogin = false;
            TempData["isPost"] = true.ToString();
            if (TempData.ContainsKey("isUpdate") && !string.IsNullOrEmpty(TempData["isUpdate"].ToString() ) && TempData["isUpdate"].ToString().ToLower()=="true")
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["isUpdate"] = false.ToString();
            }
            try
            {
                if (_session != null && _session.GetString("UserModel") != null)
                {
                    var usr = JsonConvert.DeserializeObject<UserModel>(_session.GetString("UserModel"));

                    var activity = JsonConvert.DeserializeObject<Dictionary<string, int>>(usr.Activity);
                    userViewModel.userModel = usr;

                    if (_user.GetUserByLevelAsync(userViewModel.userModel.UserEmail, userViewModel.userModel.LevelId))
                    {
                        var challenges = _challenge.GetChallengesByLevelIdAsync(userViewModel.userModel.LevelId);
                        userViewModel.ChallengeTitle = _challenge.GetChallengesByLevelIdAsync(userViewModel.userModel.LevelId).Select(x =>
                             x.ChallengeTitle
                        ).ToList();
                        Dictionary<string, int> Carb_Obj = JsonConvert.DeserializeObject<Dictionary<string, int>>(userViewModel.userModel.CarbonActivity);

                        if (Carb_Obj == null)
                        {
                            Carb_Obj = new Dictionary<string, int>();
                        }
                        //Points Update done here
                        if (!usr.IsTask1Completed && userModel.IsTask1Completed)
                        {
                            userViewModel.userModel.IsFirstTimeLogin = false;
                            userViewModel.userModel.IsTask1Completed = true;
                            userViewModel.userModel.PendingTask -= 1;
                            userViewModel.userModel.TotalPointScored += 2;
                            userViewModel.userModel.TotalTaskCompleted += 1;
                            userViewModel.userModel.LevelCompletedTask += 1;
                            userViewModel.userModel.LastModified = DateTime.Now;
                            userViewModel.IndividualCarbonScore = challenges[0].CarbonScore;
                            userViewModel.userModel.CarbonScore += challenges[0].CarbonScore;
                            if (Carb_Obj != null && Carb_Obj.ContainsKey(challenges[0].Sector))
                            {
                                Carb_Obj[challenges[0].Sector] += challenges[0].CarbonScore;
                            }
                            else
                            {
                                Carb_Obj.Add(challenges[0].Sector, challenges[0].CarbonScore);
                            }
                            userViewModel.userModel.CarbonActivity = JsonConvert.SerializeObject(Carb_Obj);
                            // if (activity.ContainsKey(DateTime.Now.ToLongDateString()))
                            //{
                            activity.Add(DateTime.Now.ToString(), userViewModel.userModel.TotalPointScored);
                            // }

                            userViewModel.userModel.Activity = JsonConvert.SerializeObject(activity);
                            userViewModel.userModel = await _user.UpdateMembershipAsync(userViewModel.userModel);
                            TempData["isUpdate"] = true.ToString();
                        }
                        if (Carb_Obj != null && !usr.IsTask2Completed && userModel.IsTask2Completed)
                        {
                            userViewModel.IndividualCarbonScore = challenges[1].CarbonScore;
                            userViewModel.userModel.CarbonScore += challenges[1].CarbonScore;
                            userViewModel.userModel.IsTask2Completed = true;
                            userViewModel.userModel.PendingTask -= 1;
                            userViewModel.userModel.TotalPointScored += 3;
                            userViewModel.userModel.LevelCompletedTask += 1;
                            userViewModel.userModel.TotalTaskCompleted += 1;
                            userViewModel.userModel.LastModified = DateTime.Now;
                            activity.Add(userViewModel.userModel.LastModified.ToString(), userViewModel.userModel.TotalPointScored);
                            userViewModel.userModel.Activity = JsonConvert.SerializeObject(activity);
                            if (Carb_Obj != null && Carb_Obj.ContainsKey(challenges[1].Sector))
                            {
                                Carb_Obj[challenges[1].Sector] += challenges[1].CarbonScore;
                            }
                            else
                            {
                                Carb_Obj.Add(challenges[1].Sector, challenges[1].CarbonScore);
                            }
                            userViewModel.userModel.Activity = JsonConvert.SerializeObject(activity);
                            userViewModel.userModel.CarbonActivity = JsonConvert.SerializeObject(Carb_Obj);
                            userViewModel.userModel = await _user.UpdateMembershipAsync(userViewModel.userModel);
                            TempData["isUpdate"] = true.ToString();

                        }
                        if (Carb_Obj != null && !usr.IsTask3Completed && userModel.IsTask3Completed)
                        {
                            userViewModel.IndividualCarbonScore = challenges[2].CarbonScore;
                            userViewModel.userModel.CarbonScore += challenges[2].CarbonScore;
                            userViewModel.userModel.IsTask3Completed = true;
                            userViewModel.userModel.PendingTask -= 1;
                            userViewModel.userModel.TotalPointScored += 4;
                            userViewModel.userModel.LevelCompletedTask += 1;
                            userViewModel.userModel.TotalTaskCompleted += 1;
                            userViewModel.userModel.LastModified = DateTime.Now;
                            activity.Add(userViewModel.userModel.LastModified.ToString(), userViewModel.userModel.TotalPointScored);
                            userViewModel.userModel.Activity = JsonConvert.SerializeObject(activity);
                            if (Carb_Obj != null && Carb_Obj.ContainsKey(challenges[2].Sector))
                            {
                                Carb_Obj[challenges[2].Sector] += challenges[2].CarbonScore;
                            }
                            else
                            {
                                Carb_Obj.Add(challenges[2].Sector, challenges[2].CarbonScore);
                            }
                            userViewModel.userModel.CarbonActivity = JsonConvert.SerializeObject(Carb_Obj);
                            userViewModel.userModel = await _user.UpdateMembershipAsync(userViewModel.userModel);
                            TempData["isUpdate"] = true.ToString();

                        }
                        if (Carb_Obj != null && !usr.IsTask4Completed && userModel.IsTask4Completed)
                        {
                            userViewModel.IndividualCarbonScore = challenges[3].CarbonScore;
                            userViewModel.userModel.CarbonScore += challenges[3].CarbonScore;
                            userViewModel.userModel.IsTask4Completed = true;
                            userViewModel.userModel.PendingTask = 0;
                            userViewModel.userModel.TotalPointScored += 5;
                            userViewModel.userModel.TotalTaskCompleted += 1;
                            userViewModel.userModel.LevelCompletedTask += 1;
                            userViewModel.userModel.LastModified = DateTime.Now;
                            activity.Add(userViewModel.userModel.LastModified.ToString(), userViewModel.userModel.TotalPointScored);
                            userViewModel.userModel.Activity = JsonConvert.SerializeObject(activity);
                            if (Carb_Obj != null && Carb_Obj.ContainsKey(challenges[3].Sector))
                            {
                                Carb_Obj[challenges[3].Sector] += challenges[3].CarbonScore;
                            }
                            else
                            {
                                Carb_Obj.Add(challenges[3].Sector, challenges[3].CarbonScore);
                            }
                            userViewModel.userModel.CarbonActivity = JsonConvert.SerializeObject(Carb_Obj);
                            userViewModel.userModel = await _user.UpdateMembershipAsync(userViewModel.userModel);
                            userViewModel.userModel.LevelId += 1;
                            userViewModel.userModel.LevelCompletedTask = 0;
                            userViewModel.userModel.PendingTask = 4;
                            userViewModel.userModel.IsTask1Completed = false;
                            userViewModel.userModel.IsTask2Completed = false;
                            userViewModel.userModel.IsTask3Completed = false;
                            userViewModel.userModel.IsTask4Completed = false;
                            userViewModel.userModel.IsTask5Completed = false;

                            userViewModel.userModel = await _user.CreateUserAsync(userViewModel.userModel);
                            var new_challenges = _challenge.GetChallengesByLevelIdAsync(userViewModel.userModel.LevelId);
                            userViewModel.ChallengeTitle = _challenge.GetChallengesByLevelIdAsync(userViewModel.userModel.LevelId).Select(x =>
                                 x.ChallengeTitle
                            ).ToList();
                            TempData["isUpdate"] = true.ToString();

                        }
                    }
                    else
                    {
                        activity.Add(userViewModel.userModel.LastModified.ToString(), userViewModel.userModel.TotalPointScored);
                        userViewModel.userModel.Activity = JsonConvert.SerializeObject(activity);
                        userViewModel.userModel.IsFirstTimeLogin = false;
                        userViewModel.userModel = await _user.CreateUserAsync(userViewModel.userModel);
                        TempData["isUpdate"] = true.ToString();
                    }
                    _session.SetString("UserModel", JsonConvert.SerializeObject(userViewModel.userModel));
                        ModelState.Clear();
                        return View(userViewModel);
                    }
                else
                {
                    return Redirect("/home/index");
                }
            }
            catch (Exception ex)
            {
                userViewModel.IsErrorException = true;
            }
            _session.SetString("UserModel", JsonConvert.SerializeObject(userViewModel.userModel));
            ModelState.Clear();
            return View(userViewModel);
            //return View(userViewModel);
        }

        [HttpPost]
        public IActionResult GetChallenges(int LevelId)
        {
            UserViewModel userViewModel = new UserViewModel();

            if (_session != null && _session.GetString("userEmail") != null)
            {
                var userModel = _user.FetchUserByLevel(_session.GetString("userEmail"), LevelId);
                var challenges = _challenge.GetChallengesByLevelIdAsync(LevelId);
                var ChallengeTitle = _challenge.GetChallengesByLevelIdAsync(LevelId).Select(x =>
                     x.ChallengeTitle
                ).ToList();
                userViewModel.userModel = userModel != null ? userModel : new UserModel();
                userViewModel.ChallengeTitle = ChallengeTitle;
                //return Json(new { userModel = userModel, challenges = ChallengeTitle }) ;
            }

            else
            {
                userViewModel.userModel = new UserModel();
                var challenges = _challenge.GetChallengesByLevelIdAsync(LevelId);
                var ChallengeTitle = _challenge.GetChallengesByLevelIdAsync(LevelId).Select(x =>
                     x.ChallengeTitle
                ).ToList();
            }
            return PartialView("~/Views/Dashboard/TaskDisplayPartialView.cshtml", userViewModel);

        }

    }
}

//Json(new { newUrl = Url.Action("Index", "Home") })