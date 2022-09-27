using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace MyMusicPlayer.Models
{
	public class SongsListHandler
	{
		public event EventHandler SongsListChanged;

		private FolderListJson _folderList;

		public SongsListHandler(FolderListJson folderList)
		{
			_folderList = folderList;
			AllSongs = new ObservableCollection<SongFile>();
			AllSongs.CollectionChanged += OnSongsListChanged;
			_folderList.NewFolderAddedEvent += OnNewFolderAdded;
			_folderList.FolderRemovedEvent += OnFolderRemoved;
			InitializeSongsList(_folderList.FoldersList);
		}

        private void OnSongsListChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
			EventHandler handler = SongsListChanged;
			handler?.Invoke(this, new EventArgs());
		}

		public ObservableCollection<SongFile> AllSongs { get; private set; }

		//protected virtual void OnSongsListChanged()
		//{
		//	EventHandler handler = SongsListChanged;
		//	handler?.Invoke(this, new EventArgs());
		//}

		private async void InitializeSongsList(IList<SongsFolder> songsFolders)
		{
			foreach (var folder in songsFolders)
			{
				AddSongsFromFolder(folder.Name);
			}
		}

		private async Task AddSongsFromFolder(string folderPath)
		{
			try
			{
				var files = new DirectoryInfo(folderPath).GetFiles();
            }
			catch(Exception e)
			{
				Console.Write(e);
			}
            foreach (var file in Directory.GetFiles(folderPath))
            {
                AllSongs.Add(new SongFile()
                {
                    Path = file,
                    Name = GetFileName(file),
					BasePath = folderPath
                });
            }
        }

        private async void OnNewFolderAdded(object sender, string e)
        {
			AddSongsFromFolder(e);
        }

		private string GetFileName(string filePath)
		{
			string slash = @"\";
#if Windows
	slash = @"\";
#elif __MACCATALYST__
	slash = @"/";
#endif
			string fileNameWithType = filePath.Split(slash).Last<string>();
			string fileName = fileNameWithType.Split(".")[0];
			return fileName;
		}

        private void OnFolderRemoved(object sender, string e)
        {
            foreach (var song in AllSongs)
            {
                if (song.BasePath == e)
                {
                    AllSongs.Remove(song);
                }
            }
        }
    }
}

