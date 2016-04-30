using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using EVE.Core.Model;
using EVE.ISXEVE;
using EVE.ISXEVE.DataTypes;
using LavishVMAPI;

namespace EVE.Core.DataServices
{
    public class PlayerDataService : IDataService<Player>
    {
        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void GetData(Action<Player, Exception> callback)
        {
            try
            {
                var item = RetrieveEVEPlayer();
                callback(item, null);
            }
            catch (Exception e)
            {
                callback(null, e);
            }
        }

        /// <summary>
        /// Retrieves the eve player.
        /// </summary>
        /// <returns></returns>
        private Player RetrieveEVEPlayer()
        {
            var player = new Player();

            Frame.Wait(true);

            var me = new Me();
            var isxeve = new EVE.ISXEVE.TopLevelObjects.EVE();

            player.Name = me.Name;

            Frame.Unlock();

            return player;
        }
    }
}
