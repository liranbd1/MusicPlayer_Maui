using System;
using Plugin.Maui.Audio;

namespace MyMusicPlayer.Models
{
    public class MusicPlayer
    {
        private readonly IAudioManager _audioManager;
        private IAudioPlayer _currentPlayer;

        public MusicPlayer(IAudioManager audioManager)
        {
            _audioManager = audioManager;
        }

        public void PlayPlaylist(Playlist playlist)
        {
            foreach(var song in playlist.SongsFile)
            {
                PlaySong(song);

                while (_currentPlayer.IsPlaying)
                {
                    if (_currentPlayer.CurrentPosition < _currentPlayer.Duration)
                    {
                        break;
                    }
                }
            }
        }

        public void PlaySong(SongFile song)
        {
            if (_currentPlayer != null && _currentPlayer.IsPlaying)
            {
                _currentPlayer.Stop();
                _currentPlayer.Dispose();
            }

            _currentPlayer = _audioManager.CreatePlayer(song.Path);
            _currentPlayer.Play();
        }

        public void PauseSong()
        {
            if (_currentPlayer == null)
            {
                return;
            }

            _currentPlayer.Pause();
        }
    }
}

