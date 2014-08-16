using PropertyChanged;

namespace UniContact.Model
{
    [ImplementPropertyChanged]
    public class ServiceProvider
    {
        public string Name { get; set; }
        public string BackgroundColour { get; set; }
        public string NavigationUri { get; set; }
    }
}
