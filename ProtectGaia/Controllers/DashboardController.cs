using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProtectGaia.Interfaces;
using ProtectGaia.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProtectGaia.Controllers
{
    public class DashboardController : Controller
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private readonly IUser _user;
        private readonly IChallenge _challenge;
        public DashboardController(IHttpContextAccessor httpContextAccessor, IUser _user, IChallenge _challenge)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._user = _user;
            this._challenge = _challenge;
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
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
                userViewModel.ChallengeTitle.Add("Turn of Light when you go out");
                userViewModel.ChallengeTitle.Add("Turn of tap when you go complete washing");
                userViewModel.ChallengeTitle.Add("Plant a sapling in your  backyard");
                userViewModel.ChallengeTitle.Add("Dont eat frozen  food for  a day");
                userViewModel.ChallengeTitle.Add("Do not use electronic device in your spare time");

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
            if (_session != null && _session.GetString("UserModel") != null)
            {
                var usr = JsonConvert.DeserializeObject<UserModel>(_session.GetString("UserModel"));
                
                var activity = JsonConvert.DeserializeObject<Dictionary<string,int>>(usr.Activity);
                userViewModel.userModel = usr;
                //userViewModel.userModel.Activity = usr.Activity;
                //userViewModel.userModel.UserEmail = usr.UserEmail;
                //userViewModel.userModel.UserName = usr.UserName;
                //userViewModel.userModel.UserImageUrl = usr.UserImageUrl;
                //userViewModel.userModel.TotalTaskCompleted = usr.TotalTaskCompleted;
                //userViewModel.userModel.LevelCompletedTask = usr.LevelCompletedTask;
                //userViewModel.userModel.PendingTask = usr.PendingTask;
                //userViewModel.userModel.TotalPointScored = usr.TotalPointScored;
                //userViewModel.userModel.CarbonScore = usr.CarbonScore;
                //userViewModel.userModel.LevelId = usr.LevelId;
                //userViewModel.userModel.LastModified = usr.LastModified;
                //userViewModel.userModel.IsTask1Completed = usr;
                //userViewModel.userModel.LastModified = usr.LastModified;
                //userViewModel.userModel.LastModified = usr.LastModified;
                //userViewModel.userModel.LastModified = usr.LastModified;
                //userViewModel.userModel.LastModified = usr.LastModified;


                if (_user.GetUserByLevelAsync(userViewModel.userModel.UserEmail, userViewModel.userModel.LevelId))
                {
                    var challenges = _challenge.GetChallengesByLevelIdAsync(userViewModel.userModel.LevelId);
                    userViewModel.ChallengeTitle.Add("Turn of Light when you go out");
                    userViewModel.ChallengeTitle.Add("Turn of tap when you go complete washing");
                    userViewModel.ChallengeTitle.Add("Plant a sapling in your  backyard");
                    userViewModel.ChallengeTitle.Add("Dont eat frozen  food for  a day");
                    userViewModel.ChallengeTitle.Add("Do not use electronic device in your spare time");
                    //Points Update done here
                    if (userModel.IsTask1Completed)
                    {
                        userViewModel.userModel.IsTask1Completed = true;
                        userViewModel.userModel.PendingTask -= 1;
                        userViewModel.userModel.TotalPointScored += 2;
                        userViewModel.userModel.TotalTaskCompleted += 1;
                        userViewModel.userModel.LastModified = DateTime.Now;

                        if (activity.ContainsKey(DateTime.Now.ToShortDateString())){
                            activity[DateTime.Now.ToShortDateString()] = userViewModel.userModel.TotalPointScored;
                        }
                        userViewModel.userModel = await _user.UpdateMembershipAsync(userViewModel.userModel);
                    }
                    if (userModel.IsTask2Completed)
                    {
                        userViewModel.userModel.IsTask2Completed = true;
                        userViewModel.userModel.PendingTask -= 1;
                        userViewModel.userModel.TotalPointScored += 3;
                        userViewModel.userModel.TotalTaskCompleted += 1;
                        userViewModel.userModel.LastModified = DateTime.Now;
                        activity.Add(userViewModel.userModel.LastModified.ToShortDateString(), userViewModel.userModel.TotalTaskCompleted);
                        userViewModel.userModel.Activity = JsonConvert.SerializeObject(activity);
                        userViewModel.userModel = await _user.UpdateMembershipAsync(userViewModel.userModel);
                    }
                    if (userModel.IsTask3Completed)
                    {
                        userViewModel.userModel.IsTask3Completed = true;
                        userViewModel.userModel.PendingTask -= 1;
                        userViewModel.userModel.TotalPointScored += 4;
                        userViewModel.userModel.TotalTaskCompleted += 1;
                        userViewModel.userModel.LastModified = DateTime.Now;
                        activity.Add(userViewModel.userModel.LastModified.ToShortDateString(), userViewModel.userModel.TotalTaskCompleted);
                        userViewModel.userModel.Activity = JsonConvert.SerializeObject(activity);
                        userViewModel.userModel = await _user.UpdateMembershipAsync(userViewModel.userModel);
                    }
                    if (userModel.IsTask4Completed)
                    {
                        userViewModel.userModel.IsTask4Completed = true;
                        userViewModel.userModel.PendingTask -= 1;
                        userViewModel.userModel.TotalPointScored += 5;
                        userViewModel.userModel.TotalTaskCompleted += 1;
                        userViewModel.userModel.LastModified = DateTime.Now;
                        activity.Add(userViewModel.userModel.LastModified.ToShortDateString(), userViewModel.userModel.TotalTaskCompleted);
                        userViewModel.userModel.Activity = JsonConvert.SerializeObject(activity);
                        userViewModel.userModel = await _user.UpdateMembershipAsync(userViewModel.userModel);
                    }
                    //if (userModel.IsTask5Completed)
                    //{
                    //    userViewModel.userModel.IsTask5Completed = true;
                    //    userViewModel.userModel.PendingTask -= 1;
                    //    userViewModel.userModel.TotalPointScored += 6;
                    //    userViewModel.userModel.TotalTaskCompleted += 1;
                    //    userViewModel.userModel.LastModified = DateTime.Now;
                    //    activity.Add(userViewModel.userModel.LastModified.ToShortDateString(), userViewModel.userModel.TotalTaskCompleted);
                    //    userViewModel.userModel.Activity = JsonConvert.SerializeObject(activity);
                    //    userViewModel.userModel = await _user.UpdateMembershipAsync(userViewModel.userModel);
                    //}
                    //Keycheck  if existing
                }
                else
                {
                    activity.Add(userViewModel.userModel.LastModified.ToShortDateString(), userViewModel.userModel.TotalTaskCompleted);
                    userViewModel.userModel = await _user.CreateUserAsync(userViewModel.userModel);

                }
                if (userViewModel.userModel.IsTask1Completed && userViewModel.userModel.IsTask2Completed
                    && userViewModel.userModel.IsTask3Completed && userViewModel.userModel.IsTask4Completed && userViewModel.userModel.IsTask5Completed)
                {
                    userViewModel.userModel.LevelId += 1;
                    userViewModel.userModel.LevelCompletedTask = 0;
                    userViewModel.userModel.PendingTask = 5;
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
                    userViewModel.ChallengeTitle.Add("Turn of Light when you go out");
                    userViewModel.ChallengeTitle.Add("Turn of tap when you go complete washing");
                    return View(userViewModel);
                }
                }
                
            return View(userViewModel);
        }
    }
}

//Json(new { newUrl = Url.Action("Index", "Home") })