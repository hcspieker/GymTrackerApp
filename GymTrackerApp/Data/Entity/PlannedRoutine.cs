namespace GymTrackerApp.Data.Entity
{
    public class PlannedRoutine
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public PlannedRoutineCategory Category { get; set; }

        public List<PlannedWorkout> PlannedWorkouts { get; set; } = new();
    }
}
