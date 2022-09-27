using System;
using System.Collections.ObjectModel;

namespace MyMusicPlayer.Models
{
	public class PlaylistJson
	{
		public PlaylistJson()
		{
		}

		public ObservableCollection<Playlist> PlaylistList { get; set; }
	}
}

