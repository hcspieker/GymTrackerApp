using GymTrackerApp.Data.Entity;

namespace GymTrackerApp.Models
{
    public class DropDownElement
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public DropDownElement(PlannedRoutine routine)
        {
            Id = routine.Id;
            Title = routine.Title;
        }

        public DropDownElement(PlannedWorkout workout)
        {
            Id = workout.Id;
            Title = workout.Title;
        }
    }
}
