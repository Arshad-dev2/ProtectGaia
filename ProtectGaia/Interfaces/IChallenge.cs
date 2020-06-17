using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProtectGaia.Models;

namespace ProtectGaia.Interfaces
{
    public interface IChallenge
    {
        Task<ChallengeModel> CreateChallengeAsync(ChallengeModel challengeModel);
        Task<List<ChallengeModel>> GetChallengesByChallengeIdAsync(int challengeId);
        List<ChallengeModel> GetChallengesByLevelIdAsync(int LevelId);
        Task<ChallengeModel> UpdateMembershipAsync(ChallengeModel challengeModel);
        Task<List<ChallengeModel>> GetAllChallengesAsync();
        int TotalTaskInLevel(int LevelId);
        bool ChallengesExists(int ChallengeId);
    }
}
