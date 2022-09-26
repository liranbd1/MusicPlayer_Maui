using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MyMusicPlayer.Models;
using Newtonsoft.Json;

namespace MyMusicPlayer.ViewModels
{
    public class SettingsViewModel:INotifyPropertyChanged
    {
        private string _songFolderPath;
        private string _songFolerDescription;
        private SongsFolder _selectedSongsFolder;
        private readonly IFolderPicker _folderPicker;
        private readonly FolderListJson _folderListJsonObj;

        public ICommand BrowseFolderCommand { get; private set; }
        public ICommand AddSongsFolderCommand { get; private set; }
        public ICommand RemoveFolderFromListCommand { get; private set; }
        public ICommand SelectedFolderChangeCommand { get; private set; }

        public SettingsViewModel(IFolderPicker folderPicker, FolderListJson folderList)
        {
            _folderListJsonObj = folderList;
            _folderPicker = folderPicker;
            BrowseFolderCommand = new Command(() => OnBrowseButtonClicked());
            AddSongsFolderCommand = new Command(() => OnAddFolderButtonClicked());
            SelectedFolderChangeCommand = new Command(() => OnFolderSelectedChange());
            RemoveFolderFromListCommand = new Command(() =>  OnFolderDelete());//key => OnFolderDelete(key));
        }

        private void OnFolderSelectedChange()
        {
            SongFolderPath = SelectedSongsFolder.Name;
        }

        public SongsFolder SelectedSongsFolder
        {
            get => _selectedSongsFolder;
            set
            {
                if(_selectedSongsFolder != value)
                {
                    _selectedSongsFolder = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// List of folders saved in the app directory
        /// </summary>
        public ObservableCollection<SongsFolder> FoldersList => _folderListJsonObj.FoldersList;

        /// <summary>
        /// User Input - Song Description
        /// </summary>
        public string SongFolderDescription
        {
            get => _songFolerDescription;
            set
            {
                if (_songFolerDescription != value)
                {
                    _songFolerDescription = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// User input - Song Path
        /// </summary>
        public string SongFolderPath
        {
            get => _songFolderPath;
            set
            {
                if (_songFolderPath != value)
                {
                    _songFolderPath = value;
                    OnPropertyChanged();
                    ((Command)AddSongsFolderCommand).ChangeCanExecute();
                }
            }
        }

        /// <summary>
        /// Removing folder from Json
        /// </summary>
        
        private async void OnFolderDelete()
        {
            if (FoldersList.Contains(SelectedSongsFolder))
            {
                _folderListJsonObj.UpdateFolderList(SelectedSongsFolder, false);
            }

            await Shell.Current.DisplayAlert("Removed!", "Folder successfuly removed", "OK");
            OnPropertyChanged(nameof(FoldersList));
            SongFolderDescription = "";
            SongFolderPath = "";
        }

        /// <summary>
        /// Handle Add button click and add the folder to the json
        /// </summary>
        private async void OnAddFolderButtonClicked()
        {
            var newFolder = new SongsFolder()
            {
                Name = SongFolderPath,
                Description = SongFolderDescription
            };

            if (FoldersList.Contains(newFolder))
            {
                await Shell.Current.DisplayAlert("Folder Exists",
                    "The music player already sync with this folder",
                    "OK");

                return;
            }

            _folderListJsonObj.UpdateFolderList(newFolder);
            await Shell.Current.DisplayAlert("Added!", "Music files will now sync from this folder", "OK");
            OnPropertyChanged(nameof(FoldersList));
            SongFolderDescription = "";
            SongFolderPath = "";
        }

        /// <summary>
        /// Open Folder picker
        /// </summary>
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

    