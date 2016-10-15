using System;
using EVE.ISXEVE;
using ILoveEVE.Core.Model;
using LavishVMAPI;

namespace ILoveEVE.Core.DataServices
{
  public class PlayerDataService : IDataService<Player>
  {
    /// <summary>
    ///   Gets the data.
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
    ///   Retrieves the eve player.
    /// </summary>
    /// <returns></returns>
    private Player RetrieveEVEPlayer()
    {
      var player = new Player();

      using (new FrameLock())
      {
        var me = new Me();

        player.Name = me.Name;
      }

      return player;
    }
  }
}