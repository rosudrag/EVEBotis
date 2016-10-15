using EVE.ISXEVE;
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