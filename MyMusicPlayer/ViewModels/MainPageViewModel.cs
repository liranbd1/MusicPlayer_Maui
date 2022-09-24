using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MyMusicPlayer.ViewModels
{
	public class MainPageViewModel : INotifyPropertyChanged
	{
		private int _clickCounter;
		private string _buttonText;

        public event PropertyChangedEventHandler PropertyChanged;
		public ICommand CounterButtonClicked { get; private set; }

        public MainPageViewModel()
		{
			ButtonText = "Click Me"; 
			ClickCounter = 0;
			CounterButtonClicked = new Command(() => ClickCounter++);
		}

		public string ButtonText
		{
			get => _buttonText;
			set
			{
				_buttonText = value;
				OnPropertyChanged();
			}
		}

		public int ClickCounter
		{
			get => _clickCounter;
			set
			{
				_clickCounter = value;
				OnPropertyChanged();
				ButtonText = UpdateButtonText(ClickCounter);
				if (_clickCounter == 5)
				{
					((Command)CounterButtonClicked).ChangeCanExecute();
					Thread.Sleep(5000);
                    ((Command)CounterButtonClicked).ChangeCanExecute();
                }
            }
		}

		private string UpdateButtonText(int clicks)
		{
			if (clicks == 1)
			{
				return $"Clicked {clicks} time";
			}
			else
			{
				return $"Clicked {clicks} times";
			}
		}

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

