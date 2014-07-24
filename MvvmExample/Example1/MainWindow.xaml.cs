using System.Windows;


namespace Example1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //  our view model
        SongViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            //  We have declared the view model instance declaratively in the xaml.
            _viewModel = (SongViewModel)base.DataContext;
        }

        private void ButtonUpdateArtist_Click(object sender, RoutedEventArgs e)
        {
            //  the gui WILL NOT update.
            _viewModel.ArtistName = "Elvis";
        }
    }
}
