using Cimbalino.Phone.Toolkit.Services;
using PropertyChanged;
using UniContact.Model;

namespace UniContact.Services
{
    [ImplementPropertyChanged]
    public class AuthenticationService
    {
        private IApplicationSettingsService _settingsService;

        public InstagramTokenResponse InstagramTokenResponse { get; set; }
        public string FourSquareAccessToken { get; set; }

        public bool IsInstagramAuthenticated
        {
            get { return InstagramTokenResponse != null && !string.IsNullOrEmpty(InstagramTokenResponse.AccessToken); }
        }

        public bool IsFourSquareAuthenticated
        {
            get { return !string.IsNullOrEmpty(FourSquareAccessToken); }
        }

        public void StartService(IApplicationSettingsService settingsService)
        {
            _settingsService = settingsService;
            GetUserSettings();
        }
        
        public void SaveInstagramUser(InstagramTokenResponse userResponse)
        {
            InstagramTokenResponse = userResponse;
            _settingsService.Set(Constants.Settings.InstagramUser, InstagramTokenResponse);
            _settingsService.Save();
        }

        public void RemoveInstagramUser()
        {
            InstagramTokenResponse = null;
            _settingsService.Reset(Constants.Settings.InstagramUser);
            _settingsService.Save();
        }

        public void SaveFourSquareToken(string token)
        {
            FourSquareAccessToken = token;
            _settingsService.Set(Constants.Settings.FourSquareUser, FourSquareAccessToken);
            _settingsService.Save();
        }

        public void RemoveFourSquareUser()
        {
            FourSquareAccessToken = null;
            _settingsService.Reset(Constants.Settings.FourSquareUser);
            _settingsService.Save();
        }

        private void GetUserSettings()
        {
            InstagramTokenResponse = _settingsService.Get<InstagramTokenResponse>(Constants.Settings.InstagramUser);
            FourSquareAccessToken = _settingsService.Get<string>(Constants.Settings.FourSquareUser);
        }
    }
}
