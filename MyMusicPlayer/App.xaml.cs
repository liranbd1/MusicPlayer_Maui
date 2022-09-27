using MyMusicPlayer.Views;

namespace MyMusicPlayer;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}

