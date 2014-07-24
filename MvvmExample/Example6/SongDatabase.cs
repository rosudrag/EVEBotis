using System;

namespace Example6
{
    public class SongDatabase
    {
        #region Members
        Random _random = new Random();
        string[] _artists = { "Metallica", "Elvis Presley", "Madonna", "The Beatles", "The Rolling Stones", "Abba" };
        string[] _songTitles = { "Islands in the Stream", "Imagine", "Living on a Prayer", "Enter Sandman", "A Little Less Conversation", "Wonderful World" };
        #endregion

        #region Properties
        public string GetRandomArtistName
        {
            get { return _artists[_random.Next(_artists.Length)]; }
        }

        public string GetRandomSongTitle
        {
            get { return _songTitles[_random.Next(_songTitles.Length)]; }
        } 
        #endregion
    }
}
