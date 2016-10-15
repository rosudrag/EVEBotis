#region

using System.Collections.ObjectModel;
using System.Timers;
using GalaSoft.MvvmLight;
using Gate2Camp.EVEBotLogic.BusinessLogic;
using Gate2Camp.EVEBotLogic.Common;
using ILoveEVE.Core.Model;

#endregion

namespace Gate2Camp.EVEBotLogic
{
  /// <summary>
  /// </summary>
  public class GateCamping : ObservableObject
  {
    /// <summary>
    ///   Initializes a new instance of the <see cref="GateCamping" /> class.
    /// </summary>
    public GateCamping()
    {
      EngageRules = new EngageRules(100000, true, true, false);

      Entities = new ObservableCollection<EntityViewModel>();
      GateCampingBotBusinessLogic = new GateCampingBotBusinessLogic(BotState.Idle, EngageRules);

      InitRefreshEntitiesTimer();
    }

    /// <summary>
    ///   Gets or sets the gate camping bot business logic.
    /// </summary>
    /// <value>
    ///   The gate camping bot business logic.
    /// </value>
    private GateCampingBotBusinessLogic GateCampingBotBusinessLogic { get; set; }

    /// <summary>
    ///   Gets or sets the refresh entities timer.
    /// </summary>
    /// <value>
    ///   The refresh entities timer.
    /// </value>
    private Timer RefreshEntitiesTimer { get; set; }


    /// <summary>
    ///   Gets a value indicating whether [active].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [active]; otherwise, <c>false</c>.
    /// </value>
    public bool Active
    {
      get { return GateCampingBotBusinessLogic.CurrentBotState != BotState.Idle; }
    }

    /// <summary>
    ///   Gets or sets the entities.
    /// </summary>
    /// <value>
    ///   The entities.
    /// </value>
    public ObservableCollection<EntityViewModel> Entities { get; set; }

    /// <summary>
    ///   Gets or sets a value indicating whether [use propulsion].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [use propulsion]; otherwise, <c>false</c>.
    /// </value>
    public bool UsePropulsion
    {
      get { return EngageRules.UsePropulsion != null && EngageRules.UsePropulsion.Value; }
      set
      {
        EngageRules.UsePropulsion = value;
        RaisePropertyChanged("EngageRules");
      }
    }

    /// <summary>
    ///   Gets or sets the engage rules.
    /// </summary>
    /// <value>
    ///   The engage rules.
    /// </value>
    public EngageRules EngageRules { get; set; }

    /// <summary>
    ///   Sets a value indicating whether [go brawl].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [go brawl]; otherwise, <c>false</c>.
    /// </value>
    public bool GoBrawl
    {
      get { return EngageRules.GoBrawl != null && EngageRules.GoBrawl.Value; }
      set
      {
        EngageRules.GoBrawl = value;
        RaisePropertyChanged("EngageRules");
      }
    }

    public bool UseRepairer
    {
      get { return EngageRules.UseRepairer != null && EngageRules.UseRepairer.Value; }
      set
      {
        EngageRules.UseRepairer = value;
        RaisePropertyChanged("EngageRules");
      }
    }

    /// <summary>
    ///   Sets the maximum range.
    /// </summary>
    /// <value>
    ///   The maximum range.
    /// </value>
    public double MaxRange
    {
      get
      {
        if (EngageRules.MaxRange != null)
        {
          return EngageRules.MaxRange.Value;
        }
        return 0;
      }
      set
      {
        EngageRules.MaxRange = value;
        RaisePropertyChanged("EngageRules");
      }
    }

    /// <summary>
    ///   Initializes the refresh entities timer.
    /// </summary>
    private void InitRefreshEntitiesTimer()
    {
      RefreshEntitiesTimer = new Timer(1000);
      RefreshEntitiesTimer.Elapsed += OnTimedEvent;
      RefreshEntitiesTimer.Enabled = true;
    }

    /// <summary>
    ///   Called when [timed event].
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="ElapsedEventArgs" /> instance containing the event data.</param>
    private void OnTimedEvent(object sender, ElapsedEventArgs e)
    {
      if (Active)
      {
        Entities = GateCampingBotBusinessLogic.Entities;
        RaisePropertyChanged("Entities");
      }
    }


    /// <summary>
    ///   Runs the specified bot state.
    /// </summary>
    /// <param name="botState">State of the bot.</param>
    public void Run(BotState botState)
    {
      GateCampingBotBusinessLogic.CurrentBotState = botState;

      RaisePropertyChanged("Active");
    }
  }
}