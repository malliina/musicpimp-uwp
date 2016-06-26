using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MusicPimp.Background
{
    [DataContract]
    public class UpdatePlaylistMessage
    {
        public UpdatePlaylistMessage(List<SongModel> songs)
        {
            this.Songs = songs;
        }

        [DataMember]
        public List<SongModel> Songs;

    }
}
