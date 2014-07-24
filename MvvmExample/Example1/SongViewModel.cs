
namespace Example1
{
    /// <summary>
    /// This class is a view model of a song.
    /// </summary>
    public class SongViewModel
    {
        #region Construction
        /// <summary>
        /// Constructs the default instance of a SongViewModel
        /// </summary>
        public SongViewModel()
        {
            _song = new Song { ArtistName = "UnknownXXX", SongTitle = "Unknown" };
        }
        #endregion

        #region Members
        Song _song; 
        #endregion

        #region Properties
        public Song Song
        {
            get
            {
                return _song;
            }
            set
            {
                _song = value;
            }
        }

        public string ArtistName
        {
            get { return Song.ArtistName; }
            set { Song.ArtistName = value; }
        } 
        #endregion
    }
}
