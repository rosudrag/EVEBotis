#region

using System;
using System.Collections.ObjectModel;
using System.Linq;
using Core.Common;
using EVE.ISXEVE;
using Gate2Camp.EVEBotLogic.Common;
using ILoveEVE.Core;
using ILoveEVE.Core.Model;
using InnerSpaceAPI;
using LavishScriptAPI;
using LavishVMAPI;

#endregion

namespace Gate2Camp.EVEBotLogic.BusinessLogic
{
  public class GateCampingBotBusinessLogic : IBotBusinessLogic
  {
    private readonly EveDebugLogger logger = new EveDebugLogger();

    /// <summary>
    ///   Initializes a new instance of the <see cref="GateCampingBotBusinessLogic" /> class.
    /// </summary>
    /// <param name="state">The state.</param>
    /// <param name="engageRules"></param>
    public GateCampingBotBusinessLogic(BotState state, EngageRules engageRules)
    {
      CurrentBotState = state;
      EngageRules = engageRules;
      FrameActionScheduler = new FrameActionsScheduler(15);

      Entities = new ObservableCollection<EntityViewModel>();

      OneTimeSetup();
      AttachOnFrame();
    }

    public IFrameActionsScheduler FrameActionScheduler { get; set; }

    /// <summary>
    ///   Gets or sets my eve.
    /// </summary>
    /// <value>
    ///   My eve.
    /// </value>
    private EVE.ISXEVE.EVE MyEve { get; set; }

    /// <summary>
    ///   Gets or sets me.
    /// </summary>
    /// <value>
    ///   MyMe.
    /// </value>
    private Character MyMe { get; set; }

    /// <summary>
    ///   Gets or sets the state of the current bot.
    /// </summary>
    /// <value>
    ///   The state of the current bot.
    /// </value>
    public BotState CurrentBotState { get; set; }

    /// <summary>
    ///   Gets or sets a value indicating whether [do tackle].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [do tackle]; otherwise, <c>false</c>.
    /// </value>
    public EngageRules EngageRules { get; set; }

    /// <summary>
    ///   Gets or sets the entities.
    /// </summary>
    /// <value>
    ///   The entities.
    /// </value>
    public ObservableCollection<EntityViewModel> Entities { get; set; }

    /// <summary>
    ///   Gets or sets the active pilot.
    /// </summary>
    /// <value>
    ///   The active pilot.
    /// </value>
    private string ActivePilot { get; set; }

    /// <summary>
    ///   Attaches the on frame.
    /// </summary>
    public void AttachOnFrame()
    {
      LavishScript.Events.AttachEventTarget(LavishScript.Events.RegisterEvent("OnFrame"), DoThisOnFrame);
    }

    /// <summary>
    ///   Does the this on frame.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="LSEventArgs" /> instance containing the event data.</param>
    private void DoThisOnFrame(object sender, LSEventArgs e)
    {
      if (FrameActionScheduler.TryExecute())
      {
        try
        {
          var ext = new Extension();
          MyEve = ext.EVE();
          MyMe = ext.Me;

          DoWork(MyMe, MyEve);
        }
        catch (Exception exp)
        {
          InnerSpace.Echo(exp.Message);
        }
      }
    }

    /// <summary>
    ///   Does the work.
    /// </summary>
    /// <param name="myMe">My me.</param>
    /// <param name="myEVE">My eve.</param>
    private void DoWork(Character myMe, EVE.ISXEVE.EVE myEVE)
    {
      if (CurrentBotState == BotState.Active)
      {
        try
        {
          var allEntities = EntityRepository.GetLocalGridEntities(myMe, myEVE).ToArray();

          var engageableTargets = CombatHelper.FindEngageableTargets(myMe, myEVE, allEntities, EngageRules).ToList();

          Entities = new ObservableCollection<EntityViewModel>(allEntities);

          foreach (var engageableTarget in engageableTargets)
          {
            logger.Log(engageableTarget.EntityName + " " + " Corp: " + engageableTarget.Entity.Owner.Corp.Name + " " + engageableTarget.Entity.Owner.Corp.ID +
                       " AllyId: " + engageableTarget.Entity.Owner.AllianceID + " isFleet" + engageableTarget.Entity.FleetTag);
          }

          CombatHelper.Engage(myMe, myEVE, engageableTargets, EngageRules);
        }

        catch (Exception exc)
        {
          InnerSpace.Echo("DO WORK ERROR: " + exc.Message);
        }
      }
    }


    /// <summary>
    ///   Called when [time setup].
    /// </summary>
    private void OneTimeSetup()
    {
      Frame.Wait(true);

      MyEve = new EVE.ISXEVE.EVE();
      MyMe = new Me();

      MyEve.RefreshStandings();
      MyEve.RefreshBookmarks();

      ActivePilot = MyMe.Name;

      Frame.Unlock();
    }

    /// <summary>
    ///   Refreshes the and get active pilot.
    /// </summary>
    /// <returns></returns>
    public string RefreshAndGetActivePilot()
    {
      Frame.Wait(true);

      ActivePilot = new Me().Name;

      Frame.Unlock();

      return string.IsNullOrEmpty(ActivePilot) ? "Unknown Pilot Name" : ActivePilot;
    }
  }
}