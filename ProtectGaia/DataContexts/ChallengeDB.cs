using System;
using Microsoft.EntityFrameworkCore;
using ProtectGaia.Models;

namespace ProtectGaia.DataContexts
{
    public class ChallengeDB : DbContext
    {
        public ChallengeDB(DbContextOptions<ChallengeDB> options)
            : base(options)
        {
             
    }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
                .HasKey(o => new { o.UserEmail, o.LevelId });

            modelBuilder.Entity<ChallengeModel>(eb =>
            {
                eb.HasNoKey();
            });

            }

        public DbSet<UserModel> User { get; set; }
        public DbSet<ChallengeModel> ChallengeData { get; set; }
    }
}
