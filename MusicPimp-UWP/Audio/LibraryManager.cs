using MusicPimp.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPimp.Audio
{
    public class LibraryManager
    {
        private static LibraryManager instance = null;
        public static LibraryManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new LibraryManager();
                return instance;
            }
        }

        public PimpLibrary Active { get; private set; }

        public LibraryManager()
        {
            Active = new PimpLibrary(new Uri("http://localhost:8456", UriKind.Absolute), "admin", "test");
        }
    }
}
