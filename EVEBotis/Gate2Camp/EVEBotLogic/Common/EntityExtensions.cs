using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVE.ISXEVE.TopLevelObjects;
using LavishScriptAPI;

namespace Gate2Camp.EVEBotLogic.Common
{
  public static class EntityExtensions
  {
    public static bool IsFleetMember(this Entity entity)
    {
      var isInvalid = LavishScriptObject.IsNullOrInvalid(entity.Owner.ToFleetMember);
      var inFleet = !isInvalid;
      return inFleet;
    }
  }
}
