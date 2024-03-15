using CommunityToolkit.Mvvm.ComponentModel;
using GymTrackerApp.Data.Entity;
using System.Globalization;

namespace GymTrackerApp.Models
{
    public partial class ExecuteSetModel : BaseModel
    {
        [ObservableProperty]
        private int repetitions;

        [ObservableProperty]
        private decimal? weight;

        public ExecuteSetModel() : this(0, null)
        {

        }

        public ExecuteSetModel(int repetitions, decimal? weight)
        {
            Repetitions = repetitions;
            Weight = weight;
        }

        public ExecuteSetModel(string set)
        {
            if (set.Contains('x'))
            {
                var parts = set.Split('x');
                Repetitions = int.Parse(parts[0]);
                Weight = decimal.Parse(parts[1], CultureInfo.InvariantCulture);
            }
            else
            {
                Repetitions = int.Parse(set);
            }
        }

        public override string ToString()
        {
            if (Weight.HasValue)
                return $"{Repetitions}x{Weight}";

            return Repetitions.ToString();
        }

        public ExerciseSet ConvertToEty(ExerciseSetType setType)
        {
            return new ExerciseSet
            {
                SetType = setType,
                Repetitions = Repetitions,
                Weight = Weight
            };
        }
    }
}
