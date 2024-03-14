namespace GymTrackerApp.Data.Entity
{
    public class ExerciseSet
    {
        public int Id { get; set; }
        public ExerciseSetType SetType { get; set; }
        public int Repetitions { get; set; }
        public decimal? Weight { get; set; }

        public int ExerciseId { get; set; }
        public Exercise? Exercise { get; set; }
    }
}
