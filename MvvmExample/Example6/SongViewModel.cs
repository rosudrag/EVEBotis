using System.ComponentModel;
using System.Windows.Input;
using MicroMvvm;

namespace Example6
{
    /// <summary>
    /// This class is a view model of a song.
    /// </summary>
    public class SongViewModel : ObservableObject
    {
        #region Construction
        /// <summary>
        /// Constructs the default instance of a SongViewModel
        /// </summary>
        public SongViewModel()
        {
            _song = new Song { ArtistName = "Unknown", SongTitle = "Unknown" };
        }
        #endregion

        #region Members
        Song _song;
        int _count = 0;
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
            set 
            {
                if (Song.ArtistName != value)
                {
                    Song.ArtistName = value;
                    RaisePropertyChanged("ArtistName");
                }
            }
        }

        public string SongTitle
        {
            get { return Song.SongTitle; }
            set
            {
                if (Song.SongTitle != value)
                {
                    Song.SongTitle = value;
                    RaisePropertyChanged("SongTitle");
                }
            }
        } 
        #endregion

        #region Commands
        void UpdateArtistNameExecute()
        {
            ++_count;
            ArtistName = string.Format("Elvis ({0})", _count);
        }

        bool CanUpdateArtistNameExecute()
        {
            return true;
        }

        public ICommand UpdateArtistName { get { return new RelayCommand(UpdateArtistNameExecute, CanUpdateArtistNameExecute); } }
        #endregion
    }
}
