using System;
using System.Collections.ObjectModel;

namespace MyMusicPlayer.Models
{
	public class Playlist
	{
		public Playlist()
		{
		}
        public string Name { get; set; }
        public ObservableCollection<SongFile> SongsFile { get; set; }
	}
}

