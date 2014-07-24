using System.Collections.ObjectModel;
using System.Windows.Input;
using MicroMvvm;

namespace Example6
{
    class AlbumViewModel
    {
        #region Members
        private SongDatabase _database = new SongDatabase();
        ObservableCollection<SongViewModel> _songs = new ObservableCollection<SongViewModel>();
        int _count = 0;
        #endregion

        #region Properties
        public ObservableCollection<SongViewModel> Songs
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
                _songs.Add(new SongViewModel { Song = new Song { ArtistName = _database.GetRandomArtistName, SongTitle = _database.GetRandomSongTitle } });
            }
        }
        #endregion

        #region Commands
        void UpdateAlbumArtistsExecute()
        {
            if (_songs == null)
                return;

            ++_count;
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

            _songs.Add(new SongViewModel { Song = new Song { ArtistName = _database.GetRandomArtistName, SongTitle = _database.GetRandomSongTitle } });
        }

        bool CanAddAlbumArtistExecute()
        {
            return true;
        }

        public ICommand AddAlbumArtist { get { return new RelayCommand(AddAlbumArtistExecute, CanAddAlbumArtistExecute); } }

        void UpdateSongTitlesExecute()
        {
            if (_songs == null)
                return;

            ++_count;
            foreach (var song in _songs)
            {
                song.SongTitle = _database.GetRandomSongTitle;
            }
        }

        bool CanUpdateSongTitlesExecute()
        {
            return true;
        }

        public ICommand UpdateSongTitles { get { return new RelayCommand(UpdateSongTitlesExecute, CanUpdateSongTitlesExecute); } }

        #endregion
    }
}
