using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyMusicPlayer.Models;

namespace MyMusicPlayer.ViewModels
{
	public class MediaPlayerViewModel : INotifyPropertyChanged
	{
		private SongsListHandler _songsListHandler;

		public MediaPlayerViewModel(SongsListHandler songsListHandler)
		{
			_songsListHandler = songsListHandler;
            OnPropertyChanged(nameof(SongsList));
            //SongsList.CollectionChanged += OnSongsListChanged;

            _songsListHandler.SongsListChanged += OnSongsListChanged;
		}

        private void OnSongsListChanged(object sender, EventArgs e)
        {
			OnPropertyChanged(nameof(SongsList));
        }

        public ObservableCollection<SongFile> SongsList => _songsListHandler.AllSongs;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

