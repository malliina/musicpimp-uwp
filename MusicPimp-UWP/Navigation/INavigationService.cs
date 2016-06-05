using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPimp.Services
{
    public interface INavigationHandler
    {
        bool Navigate(Type pageType);
        bool NavigateWithParam(Type pageType, string navParam);
        bool NavigateWithParams(Type pageType, IDictionary<string, string> navParams);
    }
}
