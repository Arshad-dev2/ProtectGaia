using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProtectGaia.DataContexts;
using ProtectGaia.Interfaces;
using ProtectGaia.Models;

namespace ProtectGaia.Implementations
{
    public class ChallengeRepository : IChallenge
    {
        private readonly ChallengeDB _dbContext;


        public ChallengeRepository(ChallengeDB _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public bool ChallengesExists(string ChallengeId)
        {
            return _dbContext.ChallengeData.Any();

        }
        public int TotalTaskInLevel(int LevelId)
        {
            return _dbContext.ChallengeData.Count(x => x.LevelId == LevelId);
        }
        public Task<ChallengeModel> CreateChallengeAsync(ChallengeModel challengeModel)
        {
            throw new NotImplementedException();
        }

        public Task<List<ChallengeModel>> GetAllChallengesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<ChallengeModel>> GetChallengesByChallengeIdAsync(string challengeId)
        {
            throw new NotImplementedException();
        }

        public  List<ChallengeModel> GetChallengesByLevelIdAsync(int LevelId)
        {
            return _dbContext.ChallengeData.Where(x => x.LevelId == LevelId).ToList();
        }

        public Task<ChallengeModel> UpdateMembershipAsync(ChallengeModel challengeModel)
        {
            throw new NotImplementedException();
        }
    }
}
