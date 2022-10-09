using MyMusicPlayer.Models;
using MyMusicPlayer.ViewModels;

namespace MyMusicPlayer.Views;

public partial class MediaPlayerPage : ContentPage
{
	public MediaPlayerPage(PlaylistListJson playlistLists, MusicPlayer player)
	{
		//App.Current.MainPage = new AppShell();
		InitializeComponent();
		this.BindingContext = new MediaPlayerViewModel(playlistLists, player);
	}
}
