using MyMusicPlayer.Models;
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

#if Windows
		builder.Services.AddTransient<IFolderPicker, Platforms.Windows.FolderPicker>();
		
#elif __MACCATALYST__
		builder.Services.AddTransient<IFolderPicker, Platforms.MacCatalyst.FolderPicker>();
#endif

		builder.Services.AddTransient<SettingsPage>();
        return builder.Build();

    }
}

