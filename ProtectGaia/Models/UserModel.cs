using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProtectGaia.Models
{
    public class UserModel
    {
        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string UserEmail { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }
        public string UserImageUrl { get; set; }
        [Column(Order = 2)]
        public int LevelId { get; set; }
        public int TotalTaskCompleted { get; set; }
        public int LevelCompletedTask { get; set; }
        public int PendingTask { get; set; }
        public int TotalPointScored { get; set; }
        public int CarbonScore { get; set; }
        public string Activity { get; set; }
        public DateTime LastModified { get; set; }
        [Display(Name = "Task 1")]
        public bool IsTask1Completed { get; set; }
        [Display(Name = "Task 2")]
        public bool IsTask2Completed { get; set; }
        [Display(Name = "Task 3")]
        public bool IsTask3Completed { get; set; }
        [Display(Name = "Task 4")]
        public bool IsTask4Completed { get; set; }
        [Display(Name = "Task 5")]
        public bool IsTask5Completed { get; set; }
        public UserModel()
        {
        }
    }
}

