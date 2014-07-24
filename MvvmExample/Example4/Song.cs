
namespace Example4
{
    /// <summary>
    /// Model of a 'song'.
    /// </summary>
    public class Song
    {
        #region Members
        string _artistName;
        string _songTitle;
        #endregion


        #region Properties
        /// <summary>
        /// The artist name.
        /// </summary>
        public string ArtistName
        {
            get { return _artistName; }
            set { _artistName = value; }
        }

        /// <summary>
        /// The song title.
        /// </summary>
        public string SongTitle
        {
            get { return _songTitle; }
            set { _songTitle = value; }
        }

        #endregion
    }
}
