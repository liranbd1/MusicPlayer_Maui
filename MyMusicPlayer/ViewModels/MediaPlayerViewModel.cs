using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MyMusicPlayer.Models;
using Plugin.Maui.Audio;

namespace MyMusicPlayer.ViewModels
{
	public class MediaPlayerViewModel : INotifyPropertyChanged
	{
		private PlaylistListJson _playlists;
        private Playlist _selectedPlaylist;
        private ObservableCollection<SongFile> _selectedSongsList;
        private ObservableCollection<SongFile> InitialSongsList;
        private MusicPlayer _player;

        public MediaPlayerViewModel(PlaylistListJson playlistList, MusicPlayer player)
        {
            _player = player;
            _playlists = playlistList;
            OnPropertyChanged(nameof(Playlists));
            if (Playlists.Count == 0)
            {
                InitialSongsList = new ObservableCollection<SongFile>()
                {
                    new SongFile()
                    {
                        Name = "Create a playlist and add songs",
                        LengthSec = 99
                    }
                };

            }
            else
            {
                SelectedPlaylist = Playlists[0];
                SelectedPlaylistSongs = SelectedPlaylist.SongsFile;
            }

            PlayPlaylistCommand = new Command(() => _player.PlayPlaylist(SelectedPlaylist));
        }

        public ICommand PlayPlaylistCommand { get; set; } 

        public ICommand PlaySongCommand { get; set; }

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
                if (_selectedPlaylist != value)
                {
                    _selectedPlaylist = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

