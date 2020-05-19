using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProtectGaia.Models
{
    public class ChallengeModel
    {
        public int ChallengeId { get; set; }
        public int LevelId { get; set; }
        public string ChallengeTitle { get; set; }
        public string Sector { get; set; }
        public int CarbonScore { get; set; }
        public ChallengeModel()
        {
        }
    }
}
