using PropertyChanged;

namespace UniContact.Model
{
    [ImplementPropertyChanged]
    public class AppSettings
    {
        public AppSettings()
        {
            DownloadDisplayPictures = true;
        }

        public bool DownloadDisplayPictures { get; set; }
    }
}
