using GymTrackerApp.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace GymTrackerApp.Data
{
    public class GymTrackerContext : DbContext
    {
        public DbSet<PlannedRoutine> PlannedRoutines { get; set; }
        public DbSet<PlannedWorkout> PlannedWorkouts { get; set; }
        public DbSet<PlannedExercise> PlannedExercises { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseSet> ExerciseSets { get; set; }

        public string DbFile { get; }

        public GymTrackerContext()
        {
            DbFile = Path.Combine(FileSystem.AppDataDirectory, "gymtracker.db");
            SQLitePCL.Batteries_V2.Init();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Filename={DbFile}");
    }
}
