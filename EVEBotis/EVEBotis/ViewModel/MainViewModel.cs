#region

using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Gate2Camp.ViewModels;
using ILoveEVE.Core;
using ILoveEVE.Core.Model;

#endregion

namespace EVEBotis.ViewModel
{
  /// <summary>
  ///   This class contains properties that the main View can data bind to.
  ///   <para>
  ///     Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
  ///   </para>
  ///   <para>
  ///     You can also use Blend to data bind with the tool's support.
  ///   </para>
  ///   <para>
  ///     See http://www.galasoft.ch/mvvm
  ///   </para>
  /// </summary>
  public class MainViewModel : ViewModelBase
  {
    /// <summary>
    ///   The _gate2 camp view model
    /// </summary>
    private static readonly Gate2CampViewModel Gate2CampViewModel = new Gate2CampViewModel();

    /// <summary>
    ///   The _application title
    /// </summary>
    private string _applicationTitle = string.Empty;

    /// <summary>
    ///   The _current view model
    /// </summary>
    private ViewModelBase _currentViewModel;

    /// <summary>
    ///   The _player data service
    /// </summary>
    private IDataService<Player> _playerDataService;

    /// <summary>
    ///   Initializes a new instance of the MainViewModel class.
    /// </summary>
    public MainViewModel(IDataService<Player> playerDataService)
    {
      InitialisePlayerDataService(playerDataService);

      CurrentViewModel = Gate2CampViewModel;
      Gate2CampViewCommand = new RelayCommand(ExecuteGate2CampViewCommand);
    }

    /// <summary>
    ///   Gets or sets the application title.
    /// </summary>
    /// <value>
    ///   The application title.
    /// </value>
    public string ApplicationTitle
    {
      get { return _applicationTitle; }
      set
      {
        _applicationTitle = "EVEBotis - " + value;
        RaisePropertyChanged();
      }
    }

    /// <summary>
    ///   Gets or sets the current view model.
    /// </summary>
    /// <value>
    ///   The current view model.
    /// </value>
    public ViewModelBase CurrentViewModel
    {
      get { return _currentViewModel; }
      set
      {
        if (_currentViewModel == value)
          return;
        _currentViewModel = value;
        RaisePropertyChanged();
      }
    }

    /// <summary>
    ///   Gets the gate2 camp view command.
    /// </summary>
    /// <value>
    ///   The gate2 camp view command.
    /// </value>
    public ICommand Gate2CampViewCommand { get; private set; }

    public ICommand MtuDealerViewCommand { get; private set; }

    /// <summary>
    ///   Initialises the player data service.
    /// </summary>
    /// <param name="playerDataService">The player data service.</param>
    private void InitialisePlayerDataService(IDataService<Player> playerDataService)
    {
      _playerDataService = playerDataService;
      _playerDataService.GetData((player, error) =>
      {
        if (error != null || string.IsNullOrEmpty(player.Name))
        {
          ApplicationTitle = "Not in game";
        }
        else
        {
          ApplicationTitle = player.Name;
        }
      });
    }

    /// <summary>
    ///   Executes the gate2 camp view command.
    /// </summary>
    private void ExecuteGate2CampViewCommand()
    {
      CurrentViewModel = Gate2CampViewModel;
    }


  }
}