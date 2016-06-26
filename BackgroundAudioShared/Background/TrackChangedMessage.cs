using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MusicPimp.Background
{
    [DataContract]
    public class TrackChangedMessage
    {
        public TrackChangedMessage(Uri trackId)
        {
            TrackId = trackId;
        }

        [DataMember]
        public Uri TrackId;
    }
}
