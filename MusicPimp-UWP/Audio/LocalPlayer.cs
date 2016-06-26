using MusicPimp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPimp.Audio
{
    public class LocalPlayer
    {
        private static LocalPlayer instance = null;
        public static LocalPlayer Instance
        {
            get
            {
                if (instance == null)
                    instance = new LocalPlayer();
                return instance;
            }
        }
        public void Play(MusicItem track)
        {

        }
    }
}
