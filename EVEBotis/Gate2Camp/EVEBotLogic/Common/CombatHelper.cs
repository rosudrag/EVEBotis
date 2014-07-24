using System.Collections.Generic;
using System.Linq;
using EVE.ISXEVE;
using EVE.ISXEVE.Interfaces;
using Gate2Camp.ViewModels;

namespace Gate2Camp.EVEBotLogic.Common
{
    public static class CombatHelper
    {
        /// <summary>
        /// Does the tackle.
        /// </summary>
        /// <param name="myMe">My me.</param>
        /// <param name="myEve">My eve.</param>
        /// <param name="entities">The entities.</param>
        public static void DoTackle(Me myMe, EVE.ISXEVE.EVE myEve, IEnumerable<EntityViewModel> entities)
        {
            IEnumerable<EntityViewModel> neuts =
                entities.Where(x => x.EntityStandings <= 0 && x.EntityDistanceTo <= 100000);

            if (!neuts.Any())
            {
                return;
            }

            IEnumerable<EntityViewModel> targettedNeuts = TargetNeuts(neuts);

            //Tackle closest targetted neut 
            EntityViewModel closestTargetedNeut = EntityHelper.FindClosestEntity(targettedNeuts);

            if (closestTargetedNeut != null)
            {
                closestTargetedNeut.Entity.MakeActiveTarget();
                closestTargetedNeut.Entity.Orbit(500);

                ActivateTackleModules(myMe, myEve, closestTargetedNeut);
            }
            else
            {
                EntityViewModel closestNeutNotTargetted = EntityHelper.FindClosestEntity(neuts);

                if (closestNeutNotTargetted != null)
                {
                    ActivateTackleModules(myMe, myEve, closestNeutNotTargetted);

                    closestNeutNotTargetted.Entity.Approach();
                    myEve.Execute(ExecuteCommand.CmdAccelerate);
                }
            }
        }

        /// <summary>
        /// Activates the tackle modules.
        /// </summary>
        /// <param name="myMe">My me.</param>
        /// <param name="myEve">My eve.</param>
        /// <param name="enemy">The enemy.</param>
        private static void ActivateTackleModules(Me myMe, EVE.ISXEVE.EVE myEve, EntityViewModel enemy)
        {
            List<IModule> modules = myMe.Ship.GetModules();

            foreach (IModule module in modules)
            {
                IItem mItem = module.ToItem;

                string name = mItem.Name.ToLower();

                if (name.Contains("booster"))
                {
                }
                else if (name.Contains("cloak"))
                {
                    module.Deactivate();
                }
                else if (name.Contains("cyno"))
                {
                }
                else
                {
                    module.Activate();
                }
            }

            List<long> myDrones = myMe.GetActiveDrones().Select(x => x.ID).ToList();

            myEve.DronesEngageMyTarget(myDrones);
        }

        /// <summary>
        /// Targets the neuts.
        /// </summary>
        /// <param name="neuts">The neuts.</param>
        /// <returns></returns>
        private static IEnumerable<EntityViewModel> TargetNeuts(IEnumerable<EntityViewModel> neuts)
        {
            foreach (EntityViewModel neut in neuts)
            {
                Entity entity = neut.Entity;

                if (!entity.IsLockedTarget && !entity.BeingTargeted)
                {
                    entity.LockTarget();
                }

                if (entity.IsLockedTarget)
                {
                    yield return neut;
                }
            }
        }
    }
}