#region

using System;
using System.Collections.ObjectModel;
using System.Linq;
using EVE.Core.Model;
using EVE.ISXEVE;
using EVE.ISXEVE.DataTypes;
using EVE.ISXEVE.Extensions;
using Gate2Camp.EVEBotLogic.Common;
using InnerSpaceAPI;
using LavishScriptAPI;
using LavishVMAPI;

#endregion

namespace Gate2Camp.EVEBotLogic.BusinessLogic
{
    public class GateCampingBotBusinessLogic : IBotBusinessLogic
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GateCampingBotBusinessLogic" /> class.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="engageRules"></param>
        public GateCampingBotBusinessLogic(BotState state, EngageRules engageRules)
        {
            CurrentBotState = state;
            EngageRules = engageRules;

            Entities = new ObservableCollection<EntityViewModel>();

            AttachOnFrame();

            OneTimeSetup();
        }

        /// <summary>
        ///     Gets or sets my eve.
        /// </summary>
        /// <value>
        ///     My eve.
        /// </value>
        private EVE.ISXEVE.TopLevelObjects.EVE MyEve { get; set; }

        /// <summary>
        ///     Gets or sets me.
        /// </summary>
        /// <value>
        ///     MyMe.
        /// </value>
        private Character MyMe { get; set; }

        /// <summary>
        ///     Gets or sets the state of the current bot.
        /// </summary>
        /// <value>
        ///     The state of the current bot.
        /// </value>
        public BotState CurrentBotState { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [do tackle].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [do tackle]; otherwise, <c>false</c>.
        /// </value>
        public EngageRules EngageRules{ get; set; }

        /// <summary>
        ///     Gets or sets the entities.
        /// </summary>
        /// <value>
        ///     The entities.
        /// </value>
        public ObservableCollection<EntityViewModel> Entities { get; set; }

        /// <summary>
        ///     Gets or sets the active pilot.
        /// </summary>
        /// <value>
        ///     The active pilot.
        /// </value>
        private string ActivePilot { get; set; }

        /// <summary>
        ///     Attaches the on frame.
        /// </summary>
        public void AttachOnFrame()
        {
            LavishScript.Events.AttachEventTarget(LavishScript.Events.RegisterEvent("OnFrame"), DoThisOnFrame);
        }

        /// <summary>
        ///     Does the this on frame.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="LSEventArgs" /> instance containing the event data.</param>
        private void DoThisOnFrame(object sender, LSEventArgs e)
        {
            try
            {
                var ext = new Extension();
                MyEve = ext.EVE();
                MyMe = ext.Me;

                DoWork(MyMe, MyEve);

                //DEBUG
                //InnerSpace.Echo("GO BRAWL: " + EngageRules.GoBrawl);
                //InnerSpace.Echo("Use Prop: " + EngageRules.UsePropulsion);
                //InnerSpace.Echo("Max Range: " + EngageRules.MaxRange);
            }
            catch (Exception exp)
            {
                InnerSpace.Echo(exp.Message);
            }
        }

        /// <summary>
        ///     Does the work.
        /// </summary>
        /// <param name="myMe">My me.</param>
        /// <param name="myEVE">My eve.</param>
        private void DoWork(Character myMe, EVE.ISXEVE.TopLevelObjects.EVE myEVE)
        {
            if (CurrentBotState == BotState.Active)
            {
                try
                {
                    var allEntities = EntityRepository.GetLocalGridEntities(myMe, myEVE);

                    var engageableTargets = CombatHelper.FindEngageableTargets(myMe, myEVE, allEntities, EngageRules).ToList();

                    Entities = new ObservableCollection<EntityViewModel>(engageableTargets);

                    CombatHelper.Engage(myMe, myEVE, engageableTargets, EngageRules);
                }

                catch (Exception exc)
                {
                    InnerSpace.Echo("DO WORK ERROR: " + exc.Message);
                }
            }
        }


        /// <summary>
        ///     Called when [time setup].
        /// </summary>
        private void OneTimeSetup()
        {
            Frame.Wait(true);

            MyEve = new EVE.ISXEVE.TopLevelObjects.EVE();
            MyMe = new Me();

            MyEve.RefreshStandings();
            MyEve.RefreshBookmarks();

            ActivePilot = MyMe.Name;

            Frame.Unlock();
        }

        /// <summary>
        ///     Refreshes the and get active pilot.
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