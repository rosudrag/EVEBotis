using System.Collections.Generic;
using System.Linq;
using EVE.Core.Model;
using EVE.ISXEVE;
using EVE.ISXEVE.DataTypes;
using EVE.ISXEVE.TopLevelObjects;
using Gate2Camp.ViewModels;
using LavishScriptAPI;

namespace Gate2Camp.EVEBotLogic.Common
{
    public static class EntityHelper
    {
        /// <summary>
        ///     Computes the standings.
        ///     Between -10 and 10
        ///     11 = self
        /// </summary>
        /// <param name="myMe"></param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public static int ComputeStandings(Character myMe, Entity entity)
        {
            long eCharId = entity.CharID;

            if (eCharId == myMe.CharID)
            {
                return 11;
            }

            if (entity.IsOwnedByAllianceMember || entity.IsOwnedByCorpMember ||
                !LavishScriptObject.IsNullOrInvalid(entity.Owner.ToFleetMember))
            {
                return 10;
            }

            Standing standing = myMe.StandingTo(entity.OwnerID, entity.Owner.Corp.ID, entity.Owner.AllianceID);

            var standingsImportant = new List<int>
            {
                standing.AllianceToAlliance,
                standing.AllianceToCorp,
                standing.AllianceToPilot,
                standing.CorpToAlliance,
                standing.CorpToCorp,
                standing.CorpToPilot,
                standing.MeToAlliance,
                standing.MeToCorp,
                standing.MeToPilot
            };

            var standingsPersonal = new List<int>
            {
                standing.MeToAlliance,
                standing.MeToCorp,
                standing.MeToPilot
            };

            int finishStandings = standingsPersonal.Min() < 0 ? standingsPersonal.Min() : standingsImportant.Max();

            return finishStandings;
        }

        /// <summary>
        ///     Finds the closest neut.
        /// </summary>
        /// <param name="neuts">The targetted neuts.</param>
        /// <returns></returns>
        public static EntityViewModel FindClosestEntity(IEnumerable<EntityViewModel> neuts)
        {
            double maxDistance = 999999;

            EntityViewModel result = null;

            foreach (EntityViewModel neut in neuts)
            {
                double distance = neut.EntityDistanceTo;

                if (distance < maxDistance)
                {
                    maxDistance = distance;
                    result = neut;
                }
            }

            return result;
        }
    }
}