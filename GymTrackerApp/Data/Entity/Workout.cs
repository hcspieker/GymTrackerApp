namespace GymTrackerApp.Data.Entity
{
    public class Workout
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public int? PlannedWorkoutId { get; set; }
        public PlannedWorkout? PlannedWorkout { get; set; }
        public List<Exercise> Exercises { get; set; } = new();
    }
}
