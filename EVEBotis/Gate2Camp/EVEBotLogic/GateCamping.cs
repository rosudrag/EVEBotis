using System.Collections.ObjectModel;
using System.Timers;
using Gate2Camp.EVEBotLogic.BusinessLogic;
using Gate2Camp.EVEBotLogic.Common;
using Gate2Camp.ViewModels;
using MicroMvvm;

namespace Gate2Camp.EVEBotLogic
{
    /// <summary>
    /// </summary>
    public class GateCamping : ObservableObject
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GateCamping" /> class.
        /// </summary>
        public GateCamping()
        {
            Entities = new ObservableCollection<EntityViewModel>();
            GateCampingBotBusinessLogic = new GateCampingBotBusinessLogic(BotState.Idle);

            InitRefreshEntitiesTimer();
        }

        /// <summary>
        ///     Gets or sets the gate camping bot business logic.
        /// </summary>
        /// <value>
        ///     The gate camping bot business logic.
        /// </value>
        private GateCampingBotBusinessLogic GateCampingBotBusinessLogic { get; set; }

        /// <summary>
        /// Gets or sets the refresh entities timer.
        /// </summary>
        /// <value>
        /// The refresh entities timer.
        /// </value>
        private Timer RefreshEntitiesTimer { get; set; }


        /// <summary>
        ///     Gets a value indicating whether [active].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [active]; otherwise, <c>false</c>.
        /// </value>
        public bool Active
        {
            get { return GateCampingBotBusinessLogic.CurrentBotState != BotState.Idle; }
        }

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
        public string ActivePilot { get; set; }

        /// <summary>
        /// Initializes the refresh entities timer.
        /// </summary>
        private void InitRefreshEntitiesTimer()
        {
            RefreshEntitiesTimer = new Timer(1000);
            RefreshEntitiesTimer.Elapsed += OnTimedEvent;
            RefreshEntitiesTimer.Enabled = true;
        }

        /// <summary>
        ///     Called when [timed event].
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
        ///     Runs the specified bot state.
        /// </summary>
        /// <param name="botState">State of the bot.</param>
        public void Run(BotState botState)
        {
            GateCampingBotBusinessLogic.CurrentBotState = botState;

            RaisePropertyChanged("Active");
        }
    }
}