using System;
namespace MyMusicPlayer.Models
{
	public interface IFolderPicker
	{
		Task<string> PickFolder();
	}
}

