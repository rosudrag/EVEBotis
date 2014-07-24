using EVE.ISXEVE;
using EVEBotis.ShipLogic;

namespace EVEBotis
{
    /// <summary>
    /// Logic for setting up tractor units in current solar system.
    /// 
    /// 
    /// </summary>
    public static class TractorUnitSetup
    {
        public static bool SetupTractorUnits(Me myClient, EVE.ISXEVE.EVE myEve)
        {
            ShipLogic.ShipLogic.FindShipState(myClient, myEve);

            return false;
        }
    }
}