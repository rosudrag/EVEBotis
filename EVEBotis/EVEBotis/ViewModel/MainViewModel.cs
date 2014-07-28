#region

using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Gate2Camp.ViewModels;

#endregion

namespace EVEBotis.ViewModel
{
    /// <summary>
    ///     This class contains properties that the main View can data bind to.
    ///     <para>
    ///         Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    ///     </para>
    ///     <para>
    ///         You can also use Blend to data bind with the tool's support.
    ///     </para>
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// The _gate2 camp view model
        /// </summary>
        private static readonly Gate2CampViewModel _gate2CampViewModel = new Gate2CampViewModel();
        /// <summary>
        /// The _current view model
        /// </summary>
        private ViewModelBase _currentViewModel;

        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            CurrentViewModel = _gate2CampViewModel;
            Gate2CampViewCommand = new RelayCommand(ExecuteGate2CampViewCommand);
        }

        /// <summary>
        /// Gets or sets the current view model.
        /// </summary>
        /// <value>
        /// The current view model.
        /// </value>
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                if (_currentViewModel == value)
                    return;
                _currentViewModel = value;
                RaisePropertyChanged("CurrentViewModel");
            }
        }

        /// <summary>
        /// Gets the gate2 camp view command.
        /// </summary>
        /// <value>
        /// The gate2 camp view command.
        /// </value>
        public ICommand Gate2CampViewCommand { get; private set; }

        /// <summary>
        /// Executes the gate2 camp view command.
        /// </summary>
        private void ExecuteGate2CampViewCommand()
        {
            CurrentViewModel = _gate2CampViewModel;
        }
    }
}