using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MyMusicPlayer.Models;
using Plugin.Maui.Audio;

namespace MyMusicPlayer.ViewModels
{
	public class MediaPlayerViewModel : BaseViewModel
	{
		private PlaylistListJson _playlists;
        private Playlist _selectedPlaylist;
        private ObservableCollection<SongFile> _selectedSongsList;
        private ObservableCollection<SongFile> _initialSongsList;
        private ObservableCollection<Playlist> _initialPlaylist;
        private MusicPlayer _player;
        private SongFile _selectedSong;

        public MediaPlayerViewModel(PlaylistListJson playlistList, MusicPlayer player)
        {
            _player = player;
            _playlists = playlistList;
            OnPropertyChanged(nameof(Playlists));
            if (Playlists.Count == 0)
            {
                _initialSongsList = new ObservableCollection<SongFile>()
                {
                    new SongFile()
                    {
                        Name = "Please add songs",
                        LengthSec = 99
                    }
                };

                _initialPlaylist = new ObservableCollection<Playlist>()
                {
                    new Playlist()
                    {
                        Name = "Initial Playlist, please create one of your own",
                        SongsFile = _initialSongsList
                    }
                };

                SelectedPlaylist = _initialPlaylist[0];
            }
            else
            {
                SelectedPlaylist = Playlists[0];
            }

            SelectedPlaylistSongs = SelectedPlaylist.SongsFile;
            PlayPlaylistCommand = new Command(() => _player.PlayPlaylist(SelectedPlaylist));
            PauseCommand = new Command(() => _player.PauseSong());
            PlaySongCommand = new Command(() => Bob());
            OnPropertyChanged(nameof(Playlists));
            OnPropertyChanged(nameof(SelectedPlaylist));
        }

        private void Bob()
        {
            _player.PlaySong(SelectedSongToPlay);
        }


        public ICommand PlayPlaylistCommand { get; set; } 

        public ICommand PlaySongCommand { get; set; }

        public ICommand PauseCommand { get; set; }

        public ObservableCollection<Playlist> Playlists => _playlists.Playlists;

        public ObservableCollection<SongFile> SelectedPlaylistSongs
        {
            get => _selectedSongsList;

            set
            {
                if (_selectedSongsList != value)
                {
                    _selectedSongsList = value;
                    OnPropertyChanged();
                }
            }
        }

        public Playlist SelectedPlaylist
        {
            get => _selectedPlaylist;

            set
            {
                if (_selectedPlaylist != value && value != null)
                {
                    _selectedPlaylist = value;
                    OnPropertyChanged();
                }
            }
        }

        public SongFile SelectedSongToPlay
        {
            get => _selectedSong;

            set
            {
                _selectedSong = value;
                OnPropertyChanged();
            }
        }
    }
}

