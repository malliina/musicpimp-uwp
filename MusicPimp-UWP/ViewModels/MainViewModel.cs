using MusicPimp.Pages;
using MusicPimp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPimp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Debug.WriteLine("Creating MainViewModel");
        }
        public void OnButtonClicked()
        {
            if(Input.Length == 0)
            {
                Feedback = "No input";
            } else
            {
                Feedback = "You entered: " + Input;
            }
        }

        public void OpenLibrary()
        {
            PageNavigationService.Instance.NavigateWithParam(typeof(Library), "boom");
        }

        private string feedback = "Waiting...";
        public string Feedback
        {
            get { return feedback; }
            protected set { SetProperty(ref feedback, value); }
        }

        private string input = "";
        public string Input
        {
            get { return input; }
            set { SetProperty(ref input, value); }
        }
    }
}
