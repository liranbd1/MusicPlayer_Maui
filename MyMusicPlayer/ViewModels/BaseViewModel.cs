using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyMusicPlayer.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public BaseViewModel()
		{
		}

        protected void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

