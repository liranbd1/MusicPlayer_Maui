using System;
using System.Collections.ObjectModel;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace MyMusicPlayer.Models
{
	public class FolderListJson
	{
        public event EventHandler<string> NewFolderAddedEvent;
        public event EventHandler<string> FolderRemovedEvent;

        class InnerFolderList
        {
            public ObservableCollection<SongsFolder> FoldersList { get; set; }
        }

        private readonly static string _appDirectroyPath = Path.Combine(FileSystem.Current.AppDataDirectory, "FolderSongs.json");
        private InnerFolderList _innerFoldersList;

        public FolderListJson()
		{
            InitializeFolderList();
        }

        public ObservableCollection<SongsFolder> FoldersList => _innerFoldersList.FoldersList;

        public void UpdateFolderList(SongsFolder folderPath, bool toAdd = true)
        {
            if (toAdd)
            {
                AddFolder(folderPath);
            }
            else
            {
                RemoveFolder(folderPath);
            }

            File.WriteAllText(_appDirectroyPath, JsonConvert.SerializeObject(_innerFoldersList));
        }

        protected virtual void OnNewFolderAdded(string e)
        {
            EventHandler<string> handler = NewFolderAddedEvent;
            handler?.Invoke(this, e);
        }

        protected virtual void OnFolderRemoved(string e)
        {
            EventHandler<string> handler = FolderRemovedEvent;
            handler?.Invoke(this, e);
        }

        private void InitializeFolderList()
        {
            if (File.Exists(_appDirectroyPath))
            {
                var jsonString = ReadFolderSongsJson(_appDirectroyPath);
                _innerFoldersList = JsonConvert.DeserializeObject<InnerFolderList>(jsonString);
            }
            else
            {
                _innerFoldersList = new InnerFolderList();
                _innerFoldersList.FoldersList = new ObservableCollection<SongsFolder>();
                File.WriteAllText(_appDirectroyPath, JsonConvert.SerializeObject(_innerFoldersList));
            }
        }

        private string ReadFolderSongsJson(string path)
        {
            var reader = new StreamReader(_appDirectroyPath);
            return reader.ReadToEnd();
        }

        private void AddFolder(SongsFolder folder)
        {
            FoldersList.Add(folder);
            OnNewFolderAdded(folder.Name);
        }

        private void RemoveFolder(SongsFolder folder)
        {
            FoldersList.Remove(folder);
        }
	}
}

