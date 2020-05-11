using System;
using System.Collections.Generic;

namespace ProtectGaia.Models
{
    public class UserViewModel
    {
		public UserModel userModel { get; set; }
		public List<string> ChallengeTitle { get; set; }
        public UserViewModel()
        {
            userModel = new UserModel();
            ChallengeTitle = new List<string>();
        }
    }
}
