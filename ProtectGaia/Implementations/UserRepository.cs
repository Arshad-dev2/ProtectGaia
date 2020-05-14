using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProtectGaia.DataContexts;
using ProtectGaia.Interfaces;
using ProtectGaia.Models;

namespace ProtectGaia.Implementations
{
    public class UserRepository:IUser
    {
        private readonly ChallengeDB _dbContext;

        public UserRepository(ChallengeDB _dbContext)
        {
            this._dbContext = _dbContext;

        }

        public async Task<UserModel> CreateUserAsync(UserModel userModel)
        {
            _dbContext.User.Add(userModel);
            await _dbContext.SaveChangesAsync();
            return userModel;
        }

        public async Task<List<UserModel>> GetAllMembershipsAsync()
        {
            return await _dbContext.User.ToListAsync();
        }

        public async Task<UserModel> GetUserAsync(string userEmail)
        {
            return await _dbContext.User.Where(m => m.UserEmail.ToLower().Equals(userEmail)).OrderByDescending(z=>z.LevelId).FirstOrDefaultAsync();
        }
        public bool GetUserByLevelAsync(string userEmail,int LevelId)
        {
            return  _dbContext.User.Any(m => m.UserEmail.ToLower().Equals(userEmail) && m.LevelId==LevelId);
        }
        public async Task<UserModel> UpdateMembershipAsync(UserModel userModel)
        {
            var user = _dbContext.Update(userModel);
            await _dbContext.SaveChangesAsync();
            return userModel;
        }

        public  bool UserExists(string userEmail)
        {
            return _dbContext.User.Any(x => x.UserEmail.ToLower().Equals(userEmail));
        }
    }
}
