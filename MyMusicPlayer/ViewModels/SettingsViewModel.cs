using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MyMusicPlayer.Models;

namespace MyMusicPlayer.ViewModels
{
    public class SettingsViewModel:INotifyPropertyChanged
    {
        private string _songFolderPath;
        private IFolderPicker _folderPicker;

        public ICommand BrowseFolderCommand { get; private set; }

        public SettingsViewModel(IFolderPicker folderPicker)
        {
            _folderPicker = folderPicker;
            BrowseFolderCommand = new Command(() => OnBrowseButtonClicked());
        }


        public string SongFolderPath
        {
            get => _songFolderPath;
            set
            {
                if (_songFolderPath != value)
                {
                    _songFolderPath = value;
                    OnPropertyChanged();
                }
            }
        }


        private async void OnBrowseButtonClicked()
        {
            var pickedFolder = await _folderPicker.PickFolder();

            SongFolderPath = pickedFolder;
        }

        
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

