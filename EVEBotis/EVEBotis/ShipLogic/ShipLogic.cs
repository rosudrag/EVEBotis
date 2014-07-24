using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using EVE.ISXEVE;

namespace EVEBotis.ShipLogic
{

    public static class ShipLogic
    {
        public static ShipState FindShipState(Me myClient, EVE.ISXEVE.EVE myEve)
        {
            if (myClient.InStation)
            {
                return ShipState.Station;
            }
            
            if (myClient.InSpace)
            {
                myEve.QueryEntities();

                var solarSystemId = myClient.SolarSystemID;

                var location = Universe.ByID(solarSystemId);

                Console.WriteLine(location.Name);
            }

            return ShipState.Unknown;
        }
    }
}
