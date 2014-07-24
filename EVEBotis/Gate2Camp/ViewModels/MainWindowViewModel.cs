using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Gate2Camp.EVEBotLogic;
using Gate2Camp.EVEBotLogic.Common;
using InnerSpaceAPI;
using MicroMvvm;

namespace Gate2Camp.ViewModels
{
    public class MainWindowViewModel
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MainWindowViewModel" /> class.
        /// </summary>
        public MainWindowViewModel()
        {
            GateCamping = new GateCamping();
        }

        /// <summary>
        /// Gets or sets the gate camping.
        /// </summary>
        /// <value>
        /// The gate camping.
        /// </value>
        public GateCamping GateCamping { get; set; }

        /// <summary>
        ///     Gets the refresh grid entities continously.
        /// </summary>
        /// <value>
        ///     The refresh grid entities continously.
        /// </value>
        public ICommand RunGateCamp
        {
            get { return new RelayCommand(RunGateCampExecute, CanRefreshEntities); }
        }

        public ICommand StopGateCamp
        {
            get { return new RelayCommand(StopGateCampExecute, CanStopGateCamp); }
        }

        /// <summary>
        ///     Determines whether this instance [can refresh entities].
        /// </summary>
        /// <returns></returns>
        private bool CanRefreshEntities()
        {
            return true;
        }

        private void StopGateCampExecute()
        {
            GateCamping.Run(BotState.Idle);
        }

        private bool CanStopGateCamp()
        {
            return true;
        }

        /// <summary>
        ///     Refreshes the grid entities continously execute.
        /// </summary>
        private void RunGateCampExecute()
        {
            GateCamping.Run(BotState.Active);
        }

    }
}