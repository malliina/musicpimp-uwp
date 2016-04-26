using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MusicPimp.Pages
{
    public abstract class BasePage: Page
    {

        protected BasePage() {
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SetupBackButton();
            base.OnNavigatedTo(e);
        }

        private void SetupBackButton()
        {
            Frame rootFrame = Window.Current.Content as Frame;
            var backButtonVisibility = rootFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = backButtonVisibility;
        }
    }
}
