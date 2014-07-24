using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVE.ISXEVE;

namespace EVEBotis.StateLogic
{
    public class CloakLogic
    {
        /// <summary>
        /// Activates the cloak.
        /// </summary>
        /// <param name="myClient"></param>
        /// <param name="myEve"></param>
        public static bool ActivateCloak(Me myClient, EVE.ISXEVE.EVE myEve)
        {
            var myShip = myClient.Ship;

            var theModules = myShip.GetModules();

            Console.WriteLine((String.Format("Module number: {0}", theModules.Count)));

            foreach (var theModule in theModules)
            {
                if (theModule.ToItem.Name.Contains("Cloak"))
                {
                    Console.WriteLine(String.Format("Activating: {0}", theModule.IsActive));

                    if (!theModule.IsActive)
                    {
                        theModule.Activate();

                    }
                    Console.WriteLine("" + theModule.IsActive);

                    return theModule.IsActive;
                }
            }

            return false;
        }
    }
}
