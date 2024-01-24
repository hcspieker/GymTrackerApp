using CommunityToolkit.Mvvm.ComponentModel;

namespace GymTrackerApp.Models
{
    public abstract class BaseCreateModel : ObservableObject
    {
        public Guid TemporaryId { get; }

        public BaseCreateModel()
        {
            TemporaryId = Guid.NewGuid();
        }
    }
}
