using System.Collections.Generic;
using Cimbalino.Phone.Toolkit.Services;
using ScottIsAFool.WindowsPhone.ViewModel;
using UniContact.Model;

namespace UniContact.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            ServiceProviders = new List<ServiceProvider>
            {
                new ServiceProvider
                {
                    BackgroundColour = "#2A5B83",
                    Name = "Instagram",
                    NavigationUri = "/Views/Auth/InstagramAuthView.xaml"
                },
                new ServiceProvider
                {
                    BackgroundColour = "#1DAFEC",
                    Name= "FourSquare",
                    NavigationUri= "/Views/Auth/FourSquareAuthView.xaml"
                }
            };
        }

        public List<ServiceProvider> ServiceProviders { get; set; }
    }
}