using MusicPimp.Common;
using MusicPimp.Network;
using MusicPimp.Pages;
using MusicPimp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Networking.PushNotifications;
using Windows.UI.Core;

namespace MusicPimp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        // The navigation parameters
        public IDictionary<string, string> Context { get; set; }
        public ICommand DoIt { get; private set; }
        public FolderVM Folder { get; private set; }
        public MainViewModel()
        {
            Channel = new NotifyTaskCompletion<PushNotificationChannel>(loadChannelUri());
            DoIt = DelegateCommand.FromAsyncHandler(OnDoItClicked);
            Context = new Dictionary<string, string>();
            Folder = new FolderVM(false);
        }

        public async Task Initialize(IDictionary<string, string> context)
        {
            Context = context;
            var folder = context["folder"];
            if(folder != null)
            {
                await Folder.Load(folder);
            }
        }

        private NotifyTaskCompletion<PushNotificationChannel> channel;
        public NotifyTaskCompletion<PushNotificationChannel> Channel
        {
            get { return channel; }
            set
            {
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
            if (Input.Length == 0)
            {
                Feedback = "No input";
            }
            else
            {
                Feedback = $"You entered: {Input}";
            }
        }

        public async Task OnDoItClicked()
        {
            DoItFeedback = "Loading...";
            var library = new PimpLibrary(new Uri("http://localhost:8456", UriKind.Absolute), "admin", "test");
            try
            {
                var version = await library.PingAuth();
                DoItFeedback = $"Version: {version.version}";
            }
            catch (ResponseException re)
            {
                DoItFeedback = re.Message;
            }
            catch (Exception e)
            {
                DoItFeedback = $"Unable to ping. {e.Message}";
            }
        }

        public void OpenLibrary()
        {
            //PageNavigationService.Instance.NavigateWithParam(typeof(Library), "boom");
        }

        private string doItFeedback = "Waiting for a click...";
        public string DoItFeedback
        {
            get { return doItFeedback; }
            protected set { SetProperty(ref doItFeedback, value); }
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
