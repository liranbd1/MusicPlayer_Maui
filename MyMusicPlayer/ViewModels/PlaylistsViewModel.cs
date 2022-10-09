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
        private string _newPlaylistName = "";

        public PlaylistsViewModel(SongsListHandler songsListHandler, PlaylistListJson playlistList)
        {
            
            _songsListHandler = songsListHandler;
            _playlistList = playlistList;
            CreateNewPlaylistCommand = new Command(async () => await CreateNewPlaylist());
            AddSongToPlaylistCommand = new Command(async () => await AddSongToPlaylist());
            OnPropertyChanged(nameof(Playlists));
            OnPropertyChanged(nameof(SongsList));

            _songsListHandler.SongsListChanged += OnSongsListChanged;
            _playlistList.NewPlaylistAddedEvent += OnPlaylistAdded;
            _playlistList.PlaylistRemovedEvent += OnPlaylistRemoved;
        }

        private async Task AddSongToPlaylist()
        {
            int playlistIdx = Playlists.IndexOf(SelectedPlaylist);
            _playlistList.AddSongToPlaylist(SelectedSong, playlistIdx);
            OnPropertyChanged(nameof(SelectedPlaylist));
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
                if (_selectedPlaylist != value)
                {
                    _selectedPlaylist = value;
                    OnPropertyChanged();
                }
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

        public ICommand AddSongToPlaylistCommand { get; set; }

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
        }

        private void OnSongsListChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(SongsList));
        }

        private async Task CreateNewPlaylist()
        {
            if (NewPlaylistName == "")
            {
                return;
            }

            var newPlaylist = new Playlist()
            {
                Name = NewPlaylistName,
                SongsFile = new ObservableCollection<SongFile>()
            };

            _playlistList.UpdatePlaylists(newPlaylist);
            NewPlaylistName = "";
        }

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
