using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MusicPimp.Background
{
    // https://raw.githubusercontent.com/Microsoft/Windows-universal-samples/master/Samples/BackgroundAudio/cs/BackgroundAudioShared/AppState.cs
    public enum AppState
    {
        Unknown,
        Active,
        Suspended
    }

    [DataContract]
    public class AppResumedMessage
    {
        public AppResumedMessage()
        {
            Timestamp = DateTime.Now;
        }

        public AppResumedMessage(DateTime timestamp)
        {
            Timestamp = timestamp;
        }

        [DataMember]
        public DateTime Timestamp;
    }

    [DataContract]
    public class AppSuspendedMessage
    {
        public AppSuspendedMessage()
        {
            Timestamp = DateTime.Now;
        }

        public AppSuspendedMessage(DateTime timestamp)
        {
            Timestamp = timestamp;
        }

        [DataMember]
        public DateTime Timestamp;
    }

}
