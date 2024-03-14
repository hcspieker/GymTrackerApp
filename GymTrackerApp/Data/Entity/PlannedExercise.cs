namespace GymTrackerApp.Data.Entity
{
    public class PlannedExercise
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int AmountOfWarmupSets { get; set; }
        public int AmountOfWorkSets { get; set; }
        public int RepsPerWorkSet { get; set; }

        public int PlannedWorkoutId { get; set; }
        public PlannedWorkout? PlannedWorkout { get; set; }
    }
}
