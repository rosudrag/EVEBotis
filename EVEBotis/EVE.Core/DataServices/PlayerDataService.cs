using System;
using EVE.ISXEVE;
using ILoveEVE.Core.Model;
using InnerSpaceAPI;
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
        InnerSpace.Echo("Unable to retrieve char name");
        InnerSpace.Echo(e.ToString());
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

      Frame.Wait(true);

      var me = new Me();

      player.Name = me.Name;

      InnerSpace.Echo(me.Name);

      Frame.Unlock();

      return player;
    }
  }
}