using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVE.ISXEVE;
using EVEBotis.StateLogic;
using InnerSpaceAPI;
using LavishScriptAPI;
using LavishVMAPI;

namespace EVEBotis
{
    class Program
    {
        /// <summary>
        /// Gets or sets my client.
        /// </summary>
        /// <value>
        /// My client.
        /// </value>
        private static Me MyMe { get; set; }

        /// <summary>
        /// Gets or sets my eve.
        /// </summary>
        /// <value>
        /// My eve.
        /// </value>
        private static EVE.ISXEVE.EVE MyEVE { get; set; }

        private static BotState CurrentBotState { get; set; }

        static void Main(string[] args)
        {
            CurrentBotState = BotState.SetupTractorUnits;

            LavishScript.Events.AttachEventTarget(LavishScript.Events.RegisterEvent("OnFrame"), DoThisOnFrame);

            using (new FrameLock(true))
            {
                Frame.Unlock();
                Frame.Wait(true);
            }

            //var myeve = new EVE.ISXEVE.EVE();

            //try
            //{
            //    using (new FrameLock(true))
            //    {
            //        var myEve = new EVE.ISXEVE.EVE();

            //        var me = new Me();

            //        myEve.Execute(ExecuteCommand.OpenPeopleAndPlaces);

            //        myeve.RefreshBookmarks();
            //        var bookmarks = myEve.GetBookmarks();
            //        bookmarks = bookmarks.Where(x =>x.SolarSystemID == me.SolarSystemID && x.Label.Contains("Mobile Tractor Unit")).ToList();
            //        bookmarks = bookmarks.OrderBy(x => x.Label).ToList();

            //        foreach (var bookmark in bookmarks)
            //        {
            //            Console.WriteLine(bookmark.Label);
            //        }

            //        bookmarks[0].WarpTo(50);

            //    }
            //}
            //catch
            //{
            //}

            Console.ReadLine();

        }

        /// <summary>
        /// Does the this on frame.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="LSEventArgs" /> instance containing the event data.</param>
        private static void DoThisOnFrame(object sender, LSEventArgs e)
        {
            DoInit();

            switch (CurrentBotState)
            {
                case BotState.Cloakup:
                    if (CloakLogic.ActivateCloak(MyMe, MyEVE))
                    {
                        CurrentBotState = BotState.Nothing;
                    }
                    break;
                case BotState.SetupTractorUnits:
                    if (TractorUnitSetup.SetupTractorUnits(MyMe,MyEVE))
                    {
                        CurrentBotState = BotState.Nothing;
                    }
                    break;
                case BotState.Nothing:
                    break;
            }
        }

        /// <summary>
        /// Does the initialize.
        /// </summary>
        private static void DoInit()
        {
            MyMe = new Me();
            MyEVE = new EVE.ISXEVE.EVE();
        }
    }
}
