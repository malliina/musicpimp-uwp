using MusicPimp.Pages;
using MusicPimp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Networking.PushNotifications;
using Windows.UI.Core;

namespace MusicPimp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Debug.WriteLine("Creating MainViewModel");
            Channel = new NotifyTaskCompletion<PushNotificationChannel>(loadChannelUri());
        }

        private NotifyTaskCompletion<PushNotificationChannel> channel;
        public NotifyTaskCompletion<PushNotificationChannel> Channel
        {
            get { return channel; }
            set {
                if (SetProperty(ref channel, value))
                {
                    OnPropertyChanged("ChannelUri");
                }
            }
        }

        public string ChannelUri
        {
            get { return Channel?.Result?.Uri ?? ""; }
        }

        private async Task<PushNotificationChannel> loadChannelUri()
        {
            return await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
        }

        public void OnButtonClicked()
        {
            if(Input.Length == 0)
            {
                Feedback = "No input";
            } else
            {
                Feedback = "You entered: " + Input;
            }
        }

        public void OpenLibrary()
        {
            PageNavigationService.Instance.NavigateWithParam(typeof(Library), "boom");
        }

        private string feedback = "Waiting...";
        public string Feedback
        {
            get { return feedback; }
            protected set { SetProperty(ref feedback, value); }
        }

        private string input = "";
        public string Input
        {
            get { return input; }
            set { SetProperty(ref input, value); }
        }

        public async Task OnUiThreadAsync(Action uiCode)
        {
            var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => uiCode());
        }
    }
}
