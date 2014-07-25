#region

using System;
using System.Collections.ObjectModel;
using EVE.ISXEVE;
using Gate2Camp.EVEBotLogic.Common;
using Gate2Camp.ViewModels;
using InnerSpaceAPI;
using LavishScriptAPI;
using LavishVMAPI;

#endregion

namespace Gate2Camp.EVEBotLogic.BusinessLogic
{
    public class GateCampingBotBusinessLogic : IBotBusinessLogic
    {
        /// <summary>
        ///     The _one time setup
        /// </summary>
        private bool _oneTimeSetup;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GateCampingBotBusinessLogic" /> class.
        /// </summary>
        /// <param name="state">The state.</param>
        public GateCampingBotBusinessLogic(BotState state)
        {
            CurrentBotState = state;
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
        private EVE.ISXEVE.EVE MyEve { get; set; }

        /// <summary>
        ///     Gets or sets me.
        /// </summary>
        /// <value>
        ///     MyMe.
        /// </value>
        private Me MyMe { get; set; }

        /// <summary>
        ///     Gets or sets the state of the current bot.
        /// </summary>
        /// <value>
        ///     The state of the current bot.
        /// </value>
        public BotState CurrentBotState { get; set; }

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
                MyEve = new EVE.ISXEVE.EVE();
                MyMe = new Me();

                DoWork(MyMe, MyEve);
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
        private void DoWork(Me myMe, EVE.ISXEVE.EVE myEVE)
        {
            if (CurrentBotState == BotState.Active)
            {
                try
                {
                    Entities = EntityRepository.GetLocalGridEntities(myMe, myEVE);

                    CombatHelper.DoTackle(myMe, myEVE, Entities);
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

            MyEve = new EVE.ISXEVE.EVE();
            MyMe = new Me();

            MyEve.RefreshStandings();
            MyEve.RefreshBookmarks();

            ActivePilot = MyMe.Name;

            _oneTimeSetup = true;

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