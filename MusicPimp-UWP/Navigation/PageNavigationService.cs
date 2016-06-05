using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPimp.Services
{
    public class PageNavigationService
    {
        private static PageNavigationService instance = null;
        public static PageNavigationService Instance
        {
            get
            {
                if (instance == null)
                    instance = new PageNavigationService();
                return instance;
            }
        }

        public event Func<Type, bool> OnNavigateToPage;
        public event Func<Type, string, bool> OnNavigateWithParam;
        public event Func<Type, IDictionary<string, string>, bool> OnNavigateWithParams;

        protected PageNavigationService()
        {

        }

        public void Register(INavigationHandler handler)
        {
            OnNavigateToPage += handler.Navigate;
            OnNavigateWithParam += handler.NavigateWithParam;
            OnNavigateWithParams += handler.NavigateWithParams;
        }

        public void NavigateToPage(Type pageType)
        {
            OnNavigateToPage?.Invoke(pageType);
        }
        public void NavigateWithParam(Type pageType, string navParam)
        {
            OnNavigateWithParam?.Invoke(pageType, navParam);
        }
        public void NavigateWithQuery(Type pageType, IDictionary<string, string> navParams)
        {
            OnNavigateWithParams?.Invoke(pageType, navParams);
        }
    }
}
