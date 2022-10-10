using System;
using Plugin.Maui.Audio;

namespace MyMusicPlayer.Models
{
    public class MusicPlayer
    {
        private readonly IAudioManager _audioManager;
        private IAudioPlayer _currentPlayer;
        private int _currentSongIndex;
        private double _currentSongPosition;
        private SongFile _currentSong;

        public MusicPlayer(IAudioManager audioManager)
        {
            _audioManager = audioManager;
            _currentSongIndex = 0;
        }

        public double Duration => _currentPlayer.Duration;

        public void PlayPlaylist(Playlist playlist)
        {
            foreach(var song in playlist.SongsFile)
            {
                PlaySong(song);

                while (_currentPlayer.IsPlaying)
                {
                    if (_currentPlayer.CurrentPosition >= _currentPlayer.Duration)
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Play or resume the music player
        /// </summary>
        /// <param name="song"></param>
        public async void PlaySong(SongFile song)
        {
            if (_currentPlayer != null)
            {
                if (song.Equals(_currentSong))
                {
                    _currentPlayer.Play();
                    _currentPlayer.Seek(_currentSongPosition);
                }
                else
                {
                    _currentPlayer.Stop();
                    _currentPlayer.Dispose();
                }
            }

            else
            {
                _currentSong = song;
                _currentPlayer = _audioManager.CreatePlayer(_currentSong.Path);
                _currentPlayer.Play();
            }
        }

        public void PauseSong()
        {
            if (_currentPlayer == null)
            {
                return;
            }

            _currentSongPosition = _currentPlayer.CurrentPosition;
            _currentPlayer.Pause();
        }
    }
}

