using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace MyMusicPlayer.Models
{
	public class PlaylistListJson
	{
        private readonly static string _appDirectroyPath = Path.Combine(FileSystem.Current.AppDataDirectory, "Playlists.json");
        private InnerPlaylistList _inner;

        public event EventHandler<Playlist> NewPlaylistAddedEvent;
        public event EventHandler<Playlist> PlaylistRemovedEvent;


        class InnerPlaylistList
        {
            public ObservableCollection<Playlist> PlaylistList { get; set; }

        }

        public PlaylistListJson()
		{
			InitializePlaylists();
            Playlists.CollectionChanged += Playlists_CollectionChanged;
		}

        public void UpdatePlaylists(Playlist playlist)
        {
            Playlists.Add(playlist);
        }

        private void Playlists_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var p = (Playlist)e.NewItems[0];
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                OnNewPlaylistAdded(p);
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                OnPlaylistRemoved(p);
            }

            UpdatePlaylistJson();
        }

        public ObservableCollection<Playlist> Playlists => _inner.PlaylistList;

        protected virtual void OnNewPlaylistAdded(Playlist e)
        {
            EventHandler<Playlist> handler = NewPlaylistAddedEvent;
            handler?.Invoke(this, e);
        }

        protected virtual void OnPlaylistRemoved(Playlist e)
        {
            EventHandler<Playlist> handler = PlaylistRemovedEvent;
            handler?.Invoke(this, e);
        }

        private void InitializePlaylists()
        {
            if (File.Exists(_appDirectroyPath))
            {
                var jsonString = ReadPlaylistJson(_appDirectroyPath);
                _inner = JsonConvert.DeserializeObject<InnerPlaylistList>(jsonString);
            }
            else
            {
                _inner = new InnerPlaylistList();
                _inner.PlaylistList = new ObservableCollection<Playlist>();
                File.WriteAllText(_appDirectroyPath, JsonConvert.SerializeObject(_inner));
            }
        }

        private string ReadPlaylistJson(string path)
        {
            var reader = new StreamReader(_appDirectroyPath);
            return reader.ReadToEnd();
        }

        private void UpdatePlaylistJson()
        {
            File.WriteAllText(_appDirectroyPath, JsonConvert.SerializeObject(_inner));
        }
	}
}

