namespace GymTrackerApp.Data.Entity
{
    public class PlannedWorkout
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public int PlannedRoutineId { get; set; }
        public PlannedRoutine? PlannedRoutine { get; set; }
        public List<PlannedExercise> PlannedExercises { get; set; } = new();
    }
}
