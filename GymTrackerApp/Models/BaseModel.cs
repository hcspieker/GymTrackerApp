using CommunityToolkit.Mvvm.ComponentModel;

namespace GymTrackerApp.Models
{
    public abstract class BaseModel : ObservableObject
    {
        public Guid TemporaryId { get; }

        public BaseModel()
        {
            TemporaryId = Guid.NewGuid();
        }
    }
}
