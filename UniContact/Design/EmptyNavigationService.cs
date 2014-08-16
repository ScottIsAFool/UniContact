using System;
using System.Collections.Generic;
using System.Windows.Navigation;
using Cimbalino.Phone.Toolkit.Services;

namespace UniContact.Design
{
    public class EmptyNavigationService : INavigationService
    {
        public void NavigateTo(string source)
        {
        }

        public void NavigateTo(Uri source)
        {
        }

        public void GoBack()
        {
        }

        public void RemoveBackEntry()
        {
        }

        public Uri CurrentSource { get; private set; }
        public IDictionary<string, string> QueryString { get; private set; }
        public bool CanGoBack { get; private set; }
        public event NavigatingCancelEventHandler Navigating;
        public event NavigatedEventHandler Navigated;
    }
}
