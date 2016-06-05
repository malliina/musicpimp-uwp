using MusicPimp.Models;
using MusicPimp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MusicPimp.Audio
{
    public class AudioConversions
    {
        public static MusicItem PimpTrackToMusicItem(PimpTrack track)
        {
            return new MusicItem()
            {
                Id = track.id,
                Name = track.title,
                Artist = track.artist,
                Album = track.album,
                Path = track.path,
                Duration = TimeSpan.FromSeconds(track.duration),
                IsDir = false,
                Size = track.size,
                Source = track.url,
            };
        }
        public static MusicItem FolderToMusicItem(PimpFolder folder)
        {
            return new MusicItem()
            {
                Id = folder.id,
                Name = folder.title,
                Path = folder.path,
                Source = folder.url,
                IsDir = true
            };
        }
    }
}
