using MyMusicPlayer.Models;
using MyMusicPlayer.ViewModels;

namespace MyMusicPlayer.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(IFolderPicker folderPicker, FolderListJson folderList)
	{
		InitializeComponent();
		this.BindingContext = new SettingsViewModel(folderPicker, folderList);
	}

    void SwipeItem_Clicked(System.Object sender, System.EventArgs e)
    {
		Shell.Current.DisplayAlert("Something", "Is killing", "Me");
    }
}
