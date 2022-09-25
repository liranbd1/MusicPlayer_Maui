using System;
using System.Collections.ObjectModel;

namespace MyMusicPlayer.Models
{
	public class FolderListJson
	{
        public ObservableCollection<SongsFolder> FoldersList { get; set; }

        public FolderListJson()
		{
			
		}
	}
}

