using System;
namespace MyMusicPlayer.Models
{
    public class SongFile : IEquatable<SongFile>
    {
        public SongFile()
        {
        }

        public string Name { get; set; }
        public int LengthSec { get; set; }
        public string Path { get; set; }
        public string BasePath { get; set; }

        public bool Equals(SongFile other)
        {
            return this.Path == other.Path;
        }
    }
}

