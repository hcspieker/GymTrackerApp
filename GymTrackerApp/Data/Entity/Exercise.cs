namespace GymTrackerApp.Data.Entity
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int WorkoutId { get; set; }
        public Workout? Workout { get; set; }
        public List<ExerciseSet> ExerciseSets { get; set; } = new();
    }
}
