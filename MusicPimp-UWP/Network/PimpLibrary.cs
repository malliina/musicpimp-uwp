using MusicPimp.Audio;
using MusicPimp.Models;
using MusicPimp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MusicPimp.Network
{
    public class PimpLibrary: IDisposable
    {
        public HttpClient Client { get; private set; }
        public string RootFolderKey { get; protected set; }
        public PimpLibrary(Uri baseAddress, string username, string password)
        : this(BuildHttpClient(baseAddress, username, password)) {
        }

        public PimpLibrary(HttpClient client)
        {
            Client = client;
            RootFolderKey = string.Empty;
        }

        private static HttpClient BuildHttpClient(Uri baseAddress, string username, string password)
        {
            var client = new HttpClient();
            client.BaseAddress = baseAddress;
            var headers = client.DefaultRequestHeaders;
            headers.Authorization = HttpUtil.BasicAuthHeaderValue(username, password);
            headers.Accept.ParseAdd("application/vnd.musicpimp.v18+json");
            return client;
        }

        public Task<VersionResponse> PingAuth()
        {
            return GetJson<VersionResponse>("/pingauth");
        }

        public Task<IEnumerable<MusicItem>> ReloadRoot()
        {
            return Reload(RootFolderKey);
        }

        public async Task<IEnumerable<MusicItem>> Reload(string id)
        {
            var uri = id == RootFolderKey ? "/folders" : $"/folders/{id}";
            var serverResponse = await GetJson<FoldersPimpResponse>(uri);
            return Itemize(serverResponse);
        }

        public async Task<IEnumerable<MusicItem>> Search(string term)
        {
            var jsonResponse = await GetJson<IEnumerable<PimpTrack>>($"/search?term={term}&limit=100");
            return jsonResponse
                .Select(AudioConversions.PimpTrackToMusicItem)
                .ToList();
        }

        private async Task<T> GetJson<T>(string uri)
        {
            var contentAsString = await GetString(uri);
            return JsonConvert.DeserializeObject<T>(contentAsString);
        }

        private async Task<string> GetString(string uri)
        {
            var response = await Client.GetAsync(uri);
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            } else
            {
                return await Task.FromException<string>(new ResponseException(response));
            }
        }

        /// <summary>
        /// maps a json response to a list of music items
        /// </summary>
        /// <param name="response">the fetched data</param>
        private List<MusicItem> Itemize(FoldersPimpResponse response)
        {
            var ret = new List<MusicItem>();
            foreach (var folder in response.folders)
            {
                ret.Add(AudioConversions.FolderToMusicItem(folder));
            }
            foreach (var track in response.tracks)
            {
                ret.Add(AudioConversions.PimpTrackToMusicItem(track));
            }
            return ret;
        }

        public void Dispose()
        {
            Client.Dispose();
        }
    }
}
