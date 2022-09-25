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
        private readonly IFolderPicker _folderPicker;
        private readonly FolderListJson _folderListJsonObj;
        private readonly string _folderListJsonPath;

        public ICommand BrowseFolderCommand { get; private set; }
        public ICommand AddSongsFolderCommand { get; private set; }
        public ICommand RemoveFolderFromList { get; private set; }

        public SettingsViewModel(IFolderPicker folderPicker)
        {
            _folderListJsonPath = Path.Combine(FileSystem.Current.AppDataDirectory, "FolderSongs.json");
            if (!File.Exists(_folderListJsonPath))
            {
                _folderListJsonObj = new FolderListJson();
                _folderListJsonObj.FoldersList = new ObservableCollection<SongsFolder>();
                File.WriteAllText(_folderListJsonPath, JsonConvert.SerializeObject(_folderListJsonObj));
            }
            else
            {
                var reader = new StreamReader(_folderListJsonPath);
                var jsonValue = reader.ReadToEnd();
                _folderListJsonObj = JsonConvert.DeserializeObject<FolderListJson>(jsonValue);
            }

            BrowseFolderCommand = new Command(() => OnBrowseButtonClicked());
            AddSongsFolderCommand = new Command(() => OnAddFolderButtonClicked());
            RemoveFolderFromList = new Command(() =>  OnFolderDelete(""));//key => OnFolderDelete(key));
            ((Command)AddSongsFolderCommand).ChangeCanExecute();
            _folderPicker = folderPicker;

        }

        public ObservableCollection<SongsFolder> FoldersList => _folderListJsonObj.FoldersList;

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


        private void OnFolderDelete(object folderToRemove)
        {
            SongFolderPath = "Deleted";//folderToRemove.ToString();
        }

        private async void OnAddFolderButtonClicked()
        {
            var newFolder = new SongsFolder()
            {
                Name = SongFolderPath,
                Description = SongFolderDescription
            };
            
            FoldersList.Add(newFolder);
            File.WriteAllText(_folderListJsonPath, JsonConvert.SerializeObject(_folderListJsonObj));

            await Shell.Current.DisplayAlert("Added!", "Music files will now sync from this folder", "OK");
            OnPropertyChanged(nameof(FoldersList));
            SongFolderDescription = "";
            SongFolderPath = "";
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

    