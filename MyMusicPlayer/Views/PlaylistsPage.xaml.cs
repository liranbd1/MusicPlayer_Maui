using MyMusicPlayer.Models;
using MyMusicPlayer.ViewModels;

namespace MyMusicPlayer.Views;

public partial class PlaylistsPage : ContentPage
{
	public PlaylistsPage(SongsListHandler songsListHandler, PlaylistListJson playlistList)
	{
		InitializeComponent();
		this.BindingContext = new PlaylistsViewModel(songsListHandler, playlistList);
	}
}
