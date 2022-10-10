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

        private static bool _didSongEnd;
        private static bool _didUserRequestNextSong;
        private static bool _didUserRequestPrevSong;
        private static bool _isPaused;

        public MusicPlayer(IAudioManager audioManager)
        {
            _isPaused = false;
            _audioManager = audioManager;
            _currentSongIndex = 0;
        }

        public double Duration => _currentPlayer.Duration;

        public async void PlayPlaylist(Playlist playlist)
        {
            _isPaused = false;
            await Task.Run(() => { 
                foreach(var song in playlist.SongsFile)
                {
                    while (_isPaused) { }

                    PlaySong(song);

                    while (_currentPlayer.IsPlaying)
                    {
                        if (_didSongEnd)
                        {
                            _didSongEnd = false;
                            break;
                        }

                        if (_didUserRequestNextSong)
                        {
                            _didUserRequestNextSong = false; //True will come from a user button.
                            break;
                        }

                        if (_didUserRequestPrevSong)
                        {
                            _didUserRequestPrevSong = false; //True will come from a user button.
                            break;
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Play or resume the music player
        /// </summary>
        /// <param name="song"></param>
        public async void PlaySong(SongFile song)
        {
            _isPaused = false;
            if (_currentPlayer != null)
            {
                //This is the resume scenario
                if (song.Equals(_currentSong))
                {
                    _currentPlayer.Play();
                    _currentPlayer.Seek(_currentSongPosition);
                }
                //Playing a new song
                else
                {
                    _currentPlayer.Stop();
                    _currentPlayer.PlaybackEnded -= _currentPlayer_PlaybackEnded;
                    _currentPlayer.Dispose();
                    PlayNewSong(song);
                }
            }

            else
            {
                PlayNewSong(song);
            }

            _currentPlayer.PlaybackEnded += _currentPlayer_PlaybackEnded;
        }

        private void _currentPlayer_PlaybackEnded(object sender, EventArgs e)
        {
            _didSongEnd = true;
        }

        public void PauseSong()
        {
            if (_currentPlayer == null)
            {
                return;
            }

            _currentSongPosition = _currentPlayer.CurrentPosition;
            _currentPlayer.Pause();
            _currentPlayer.PlaybackEnded -= _currentPlayer_PlaybackEnded;
            _isPaused = true;

        }

        public void NextSong()
        {
            _didUserRequestNextSong = true;
        }

        public void PrevSong()
        {
            _didUserRequestPrevSong = true;
        }

        private void PlayNewSong(SongFile song)
        {
            _currentSong = song;
            _currentPlayer = _audioManager.CreatePlayer(_currentSong.Path);
            _currentPlayer.Play();
        }
    }
}

