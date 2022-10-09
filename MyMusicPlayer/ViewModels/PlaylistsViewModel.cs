using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MyMusicPlayer.Models;
using MyMusicPlayer.Views;

namespace MyMusicPlayer.ViewModels
{
    public class PlaylistsViewModel : INotifyPropertyChanged
    {
        private SongFile _selectedSong;
        private SongsListHandler _songsListHandler;
        private PlaylistListJson _playlistList;
        private Playlist _selectedPlaylist;
        private ObservableCollection<Playlist> _testOne;
        private string _newPlaylistName = "";

        public PlaylistsViewModel(SongsListHandler songsListHandler, PlaylistListJson playlistList)
        {
            _testOne = new ObservableCollection<Playlist>()
            {
                new Playlist()
                {
                    Name = "My Test Playlist",
                    SongsFile = new List<SongFile>()
                }
            };
            _songsListHandler = songsListHandler;
            _playlistList = playlistList;
            CreateNewPlaylistCommand = new Command(() => CreateNewPlaylist());
            OnPropertyChanged(nameof(Playlists));
            OnPropertyChanged(nameof(SongsList));

            _songsListHandler.SongsListChanged += OnSongsListChanged;
            _playlistList.NewPlaylistAddedEvent += OnPlaylistAdded;
            _playlistList.PlaylistRemovedEvent += OnPlaylistRemoved;
        }

        #region Public Methods
        public string NewPlaylistName
        {
            get => _newPlaylistName;
            set
            {
                if (value != _newPlaylistName)
                {
                    _newPlaylistName = value;
                    OnPropertyChanged();
                }
            }
        }

        public Playlist SelectedPlaylist
        {
            get => _selectedPlaylist;

            set
            {
                _selectedPlaylist = value;
                OnPropertyChanged();
            }
        }

        public SongFile SelectedSong
        {
            get => _selectedSong;

            set
            {
                _selectedSong = value;
                OnPropertyChanged();
            }
        }

        public ICommand CreateNewPlaylistCommand { get; set; }

        public ObservableCollection<Playlist> Playlists => _playlistList.Playlists;

        public ObservableCollection<SongFile> SongsList => _songsListHandler.AllSongs;

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        private void OnPlaylistRemoved(object sender, Playlist e)
        {
            OnPropertyChanged(nameof(Playlists));
        }

        private void OnPlaylistAdded(object sender, Playlist e)
        {
            OnPropertyChanged(nameof(Playlists));
            SelectedPlaylist = e;
            OnPropertyChanged(nameof(SelectedPlaylist));
        }

        private void OnSongsListChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(SongsList));
        }

        private async void CreateNewPlaylist()
        {
            if (NewPlaylistName == "")
            {
                return;
            }

            var newPlaylist = new Playlist()
            {
                Name = NewPlaylistName,
                SongsFile = new List<SongFile>()
            };
            Playlists.Add(newPlaylist);
            NewPlaylistName = "";
        }

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
