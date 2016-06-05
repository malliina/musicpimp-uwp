using MusicPimp.Audio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPimp.ViewModels
{
    public class FolderVM : LoadingViewModel
    {
        public FolderVM(bool dummy)
        {
            if (dummy)
            {
                var items = new ObservableCollection<MusicItem> {
                    new MusicItem() {Name = "hey", Artist ="you" }
                };
                Items = items;
            }
        }

        public async Task Load(string id)
        {
            var musicItems = await LibraryManager.Instance.Active.Reload(id);
            Items = new ObservableCollection<MusicItem>(musicItems);
        }

        private string folder;
        public string Folder
        {
            get { return folder; }
            set { SetProperty(ref folder, value); }
        }

        public ObservableCollection<MusicItem> items;
        public ObservableCollection<MusicItem> Items
        {
            get { return items; }
            set { SetProperty(ref items, value); }
        }
    }
}
