using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI.Core;

namespace MusicPimp.Util
{
    public class Utils
    {
        public static IAsyncAction OnUiThreadAsync(Action uiCode)
        {
            var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
            return dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => uiCode());
        }
    }
}
