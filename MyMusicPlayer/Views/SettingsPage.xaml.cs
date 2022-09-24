using MyMusicPlayer.Models;
using MyMusicPlayer.ViewModels;

namespace MyMusicPlayer.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(IFolderPicker folderPicker)
	{
		InitializeComponent();
		this.BindingContext = new SettingsViewModel(folderPicker);
	}
}
