namespace GymTrackerApp.Data.Entity
{
    public enum PlannedRoutineCategory
    {
        Unknown = 0,
        Endurance = 1 << 0,
        Hypertrophy = 2 << 1,
        Strength = 3 << 2
    }
}
