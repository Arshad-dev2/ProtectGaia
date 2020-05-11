using System;
using System.ComponentModel.DataAnnotations;
namespace ProtectGaia.Models
{
    public class TaskModel
    {

		public string ChallengeId { get; set; }
		public string ChallengeTitle { get; set; }
		public bool IsCompleted { get; set; }
		public TaskModel()
        {
        }
    }
}
