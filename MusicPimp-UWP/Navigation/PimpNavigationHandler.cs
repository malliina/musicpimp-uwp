using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace MusicPimp.Services
{
    public class PimpNavigationHandler : INavigationHandler
    {
        private Frame frame;

        public PimpNavigationHandler(Frame rootFrame)
        {
            frame = rootFrame;
        }

        public bool Navigate(Type pageType)
        {
            return NavigateWithParams(pageType, new Dictionary<string, string>());
        }
        
        public bool NavigateWithParam(Type pageType, string navParam)
        {
            return frame.Navigate(pageType, navParam);
        }

        public bool NavigateWithParams(Type pageType, IDictionary<string, string> navParams)
        {
            return frame.Navigate(pageType, navParams);
        }
    }
}
