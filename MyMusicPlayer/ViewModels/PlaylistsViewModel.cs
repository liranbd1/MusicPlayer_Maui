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

        private async void CreateNewPlaylist()
        {
            /*
             * This will open a new Form window
             * The form will hold metadata about the playlist
             * After the window will close we will pass the newealy created
             * playlist to a AddPlaylist method in the PlaylistListJson object
             */
            var setupPage = new Window(new PlaylistSetupPage());
            Application.Current.OpenWindow(setupPage);
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

        public ObservableCollection<Playlist> Playlists => _testOne; //_playlistList.Playlists;

        public ObservableCollection<SongFile> SongsList => _songsListHandler.AllSongs;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPlaylistRemoved(object sender, Playlist e)
        {
            
            throw new NotImplementedException();
        }

        private void OnPlaylistAdded(object sender, Playlist e)
        {
            throw new NotImplementedException();
        }

        private void OnSongsListChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(SongsList));
        }

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

