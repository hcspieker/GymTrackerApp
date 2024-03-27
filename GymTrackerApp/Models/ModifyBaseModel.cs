using CommunityToolkit.Mvvm.ComponentModel;

namespace GymTrackerApp.Models
{
    public abstract class ModifyBaseModel : ObservableObject
    {
        public readonly int Id;

        public bool IsVisible => ModelState != ModelState.Deleted;

        private ModelState modelState;
        public ModelState ModelState
        {
            get => modelState;
            set
            {
                modelState = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsVisible));
            }
        }

        protected ModifyBaseModel(int id)
        {
            Id = id;
        }
    }
}
