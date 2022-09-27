using MyMusicPlayer.Models;
using MyMusicPlayer.ViewModels;

namespace MyMusicPlayer.Views;

public partial class MediaPlayerPage : ContentPage
{
	public MediaPlayerPage(SongsListHandler songsListHandler)
	{
		//App.Current.MainPage = new AppShell();
		InitializeComponent();
		this.BindingContext = new MediaPlayerViewModel(songsListHandler);
	}
}
