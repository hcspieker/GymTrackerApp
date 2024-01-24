namespace GymTrackerApp.Models
{
    public class PickerItem<T> where T : struct, IConvertible
    {
        public T Value { get; set; }
        public string DisplayName { get; set; }

        public PickerItem(T value)
        {
            if (!typeof(T).IsEnum)
                throw new NotImplementedException($"'{typeof(T)}' is not an enum");

            Value = value;
            DisplayName = value.ToString() ?? string.Empty;
        }
    }
}
