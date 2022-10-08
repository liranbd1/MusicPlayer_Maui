using MyMusicPlayer.Models;
using MyMusicPlayer.ViewModels;
using MyMusicPlayer.Views;

namespace MyMusicPlayer;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});


        // Dependecy Injection for the correct IFolderPicker implemintation depending on OS
		// Making sure that the FolderPicker is a singleton object.
#if Windows
		builder.Services.AddSingleton<IFolderPicker, Platforms.Windows.FolderPicker>();
		
#elif __MACCATALYST__
		builder.Services.AddSingleton<IFolderPicker, Platforms.MacCatalyst.FolderPicker>();
#endif
        builder.Services.AddSingleton<FolderListJson>();
		builder.Services.AddSingleton<PlaylistListJson>();
        builder.Services.AddSingleton<SettingsPage>();
		builder.Services.AddSingleton<SettingsViewModel>();
		builder.Services.AddSingleton<SongsListHandler>();
		builder.Services.AddSingleton<MediaPlayerPage>();
		builder.Services.AddSingleton<PlaylistsPage>();
		return builder.Build();

    }
}

