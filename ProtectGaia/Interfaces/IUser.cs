using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProtectGaia.Models;

namespace ProtectGaia.Interfaces
{

    public interface IUser
    {
        Task<UserModel> CreateUserAsync(UserModel userModel);
        Task<UserModel> GetUserAsync(string userEmail);
        Task<UserModel> UpdateMembershipAsync(UserModel userModel);
        Task<List<UserModel>> GetAllMembershipsAsync();
        bool GetUserByLevelAsync(string userEmail, int LevelId);
        bool UserExists(string userEmail);
    }
}
