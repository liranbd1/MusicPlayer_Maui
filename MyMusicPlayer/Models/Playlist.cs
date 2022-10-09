using System;
namespace MyMusicPlayer.Models
{
	public class Playlist
	{
		public Playlist()
		{
		}
        public string Name { get; set; }
        public List<SongFile> SongsFile { get; set; }
	}
}

