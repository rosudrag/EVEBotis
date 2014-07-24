using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EVE.ISXEVE;
using InnerSpaceAPI;
using LavishScriptAPI;

namespace GateCAmp
{
    public static class MainGateCamp
    {
        public enum BotState
        {
            Camping,
            Idle,
            Unknown
        }

        private static EVE.ISXEVE.EVE EVE { get; set; }

        private static Me Me { get; set; }

        public static BotState CurrentState { get; set; }

        public static ObservableCollection<Entity> EntityList { get; set; }

        public static void Run()
        {
            LavishScript.Events.AttachEventTarget(LavishScript.Events.RegisterEvent("OnFrame"), DoThisOnFrame);
        }

        private static void DoThisOnFrame(object sender, LSEventArgs e)
        {
            try
            {
                EVE = new EVE.ISXEVE.EVE();
                Me = new Me();

                switch (CurrentState)
                {
                    case BotState.Camping:
                        RunCamping();
                        break;
                    case BotState.Idle:
                        break;
                    case BotState.Unknown:
                        break;

                }
            }
            catch (Exception ex)
            {
                InnerSpace.Echo(ex.Message);
            }
            
        }

        /// <summary>
        /// Runs the camping.
        /// </summary>
        private static void RunCamping()
        {
            var entities = EVE.QueryEntities();

            var pcEntities = entities.Where(x => x.IsPC).Where(x => Me.ToEntity.DistanceTo(x.ID) < 100000);
            
            EntityList.Clear();

            foreach (var pcEntity in pcEntities)
            {
                EntityList.Add(pcEntity);
            }
        }

        public static void Init()
        {
            EntityList = new ObservableCollection<Entity>();
            CurrentState = BotState.Idle;

            GuiInterface.Init();
        }
    }
}
