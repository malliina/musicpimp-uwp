using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPimp.ViewModels
{
    public class LibraryVM : ViewModelBase
    {
        private string motd = "No messages today!";
        public string Motd
        {
            get { return motd; }
            set { SetProperty(ref motd, value); }
        }

        public void Update(string input)
        {
            Motd = input;
        }
    }
}
