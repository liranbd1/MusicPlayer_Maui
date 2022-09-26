using System;
namespace MyMusicPlayer.Models
{
    public class SongsFolder : IEquatable<SongsFolder>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public bool Equals(SongsFolder other)
        {
            return this.Name == other.Name;
        }
    }
}

