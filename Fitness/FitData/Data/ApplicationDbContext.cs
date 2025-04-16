using FitCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FitData.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

          

            // 1:M Trainee And NutritionPlans
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(a => a.NutritionPlans)
                .WithOne(p => p.Trainee)
                .HasForeignKey(p => p.TraineeId)
                .OnDelete(DeleteBehavior.Restrict);


            // 1:M Nutritionist And CreatedNutritionPlans
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(a => a.CreatedNutritionPlans)
                .WithOne(p => p.Nutritionist)
                .HasForeignKey(p => p.NutritionistId)
                .OnDelete(DeleteBehavior.Restrict);

            // M:M Trainee And VideoReview and Video
            modelBuilder.Entity<VideoReview>()
                .HasIndex(vr => new { vr.VideoId, vr.TraineeId })
                .IsUnique();

            modelBuilder.Entity<VideoReview>()
                .HasOne(vr => vr.Video)
                .WithMany(v => v.VideoReview)
                .HasForeignKey(vr => vr.VideoId);

            modelBuilder.Entity<VideoReview>()
                .HasOne(vr => vr.Trainee)
                .WithMany(t => t.VideoReview)
                .HasForeignKey(vr => vr.TraineeId);

            modelBuilder.Entity<TraineerReview>()
                .HasIndex(vr => new { vr.TrainerId, vr.TraineeId })
                .IsUnique();


            modelBuilder.Entity<TraineerReview>()
                .HasOne(vr => vr.Trainee)
                .WithMany(v => v.GivenReviewsTrainee)
                .HasForeignKey(vr => vr.TraineeId);

            modelBuilder.Entity<TraineerReview>()
                .HasOne(vr => vr.Trainer)
                .WithMany(t => t.ReceivedReviewsTrainer)
                .HasForeignKey(vr => vr.TrainerId)
                .OnDelete(DeleteBehavior.NoAction);





            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Level> Levels { get; set; }
        public DbSet<NutritionPlans> nutritionPlans { get; set; }
        public DbSet<Video> Video { get; set; }
        public DbSet<VideoReview> VideoReview { get; set; }
        public DbSet<TraineerReview> TraineerReview { get; set; }
    }
}
