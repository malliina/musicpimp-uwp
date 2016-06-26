using MusicPimp.Audio;
using MusicPimp.Pages;
using MusicPimp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPimp.ViewModels
{
    public class FolderVM : LoadingViewModel
    {
        public static readonly int NoSelectionIndex = -1;
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

        private int selectedIndex = NoSelectionIndex;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { SetProperty(ref selectedIndex, value); }
        }

        private MusicItem selectedItem;
        public MusicItem SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (SetProperty(ref selectedItem, value))
                {
                    OnSelection(value);
                }
            }
        }

        public FolderVM(bool dummy)
        {
            if (dummy)
            {
                var items = new ObservableCollection<MusicItem> {
                    new MusicItem() { Name = "hey", Artist ="you" }
                };
                Items = items;
            }
        }

        public async Task Load(string id)
        {
            var musicItems = await LibraryManager.Instance.Active.Reload(id);
            Items = new ObservableCollection<MusicItem>(musicItems);
        }

        public void OnSelection(MusicItem item)
        {
            if(item != null)
            {
                if (item.IsDir)
                {
                    var dict = new Dictionary<string, string>()
                {
                    { "folder", item.Id }
                };
                    PageNavigationService.Instance.NavigateWithQuery(typeof(MainPage), dict);
                }
                else
                {
                    Debug.WriteLine($"Selected {item.Name}");
                }
            }
        }
    }
}
