using System.Collections.ObjectModel;
using System.Windows.Input;
using MicroMvvm;

namespace Example5
{
    class AlbumViewModel
    {
        #region Members
        private SongDatabase _database = new SongDatabase();
        ObservableCollection<Song> _songs = new ObservableCollection<Song>();
        #endregion

        #region Properties
        public ObservableCollection<Song> Songs
        {
            get
            {
                return _songs;
            }
            set
            {
                _songs = value;
            }
        }
        #endregion

        #region Construction
        public AlbumViewModel()
        {
            for (int i = 0; i < 3; ++i)
            {
                _songs.Add(new Song { ArtistName = _database.GetRandomArtistName, SongTitle = _database.GetRandomSongTitle });
            }
        }
        #endregion

        #region Commands
        void UpdateAlbumArtistsExecute()
        {
            if (_songs == null)
                return;

            foreach (var song in _songs)
            {
                song.ArtistName = _database.GetRandomArtistName;
            }
        }

        bool CanUpdateAlbumArtistsExecute()
        {
            return true;
        }

        public ICommand UpdateAlbumArtists { get { return new RelayCommand(UpdateAlbumArtistsExecute, CanUpdateAlbumArtistsExecute); } }


        void AddAlbumArtistExecute()
        {
            if (_songs == null)
                return;

            _songs.Add(new Song { ArtistName = _database.GetRandomArtistName, SongTitle = _database.GetRandomSongTitle });
        }

        bool CanAddAlbumArtistExecute()
        {
            return true;
        }

        public ICommand AddAlbumArtist { get { return new RelayCommand(AddAlbumArtistExecute, CanAddAlbumArtistExecute); } }
        #endregion
    }
}
