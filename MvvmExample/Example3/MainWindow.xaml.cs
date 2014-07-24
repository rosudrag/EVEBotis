using System.Windows;

namespace Example3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //  Notice that now we don't use control-specific events, we don't need
        //  to reference the viewmodel.
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
