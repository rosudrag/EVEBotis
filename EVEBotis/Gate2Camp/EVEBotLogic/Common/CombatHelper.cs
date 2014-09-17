#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EVE.Core.Model;
using EVE.ISXEVE;
using Gate2Camp.ViewModels;
using Xceed.Wpf.DataGrid.Converters;
using EVE = EVE.ISXEVE.EVE;

#endregion

namespace Gate2Camp.EVEBotLogic.Common
{
    public static class CombatHelper
    {
        private const int DefaultEngageRange = 100000;

        private const bool DefaultGoBrawl = true;

        private const bool DefaultActivatePropulsion = true;

        /// <summary>
        /// Does the tackle and returns the current valid engageable targets
        /// </summary>
        /// <param name="myMe">My me.</param>
        /// <param name="myEve">My eve.</param>
        /// <param name="entities">The entities.</param>
        /// <param name="engageRules">The engage rules.</param>
        public static void Engage(Character myMe, global::EVE.ISXEVE.EVE myEve, IEnumerable<EntityViewModel> entities, EngageRules engageRules)
        {
            IEnumerable<EntityViewModel> targettedNeuts = TargetNeuts(entities).ToList();

            //Tackle closest targetted neut 
            EntityViewModel closestTargetedNeut = null;
            if (targettedNeuts.Any())
            {
                closestTargetedNeut = EntityHelper.FindClosestEntity(targettedNeuts);
            }

            if (closestTargetedNeut != null)
            {
                if (engageRules.GoBrawl ?? DefaultGoBrawl)
                {
                    closestTargetedNeut.Entity.MakeActiveTarget();
                    closestTargetedNeut.Entity.Orbit(500);
                }


                ActivateModules(myMe, myEve, closestTargetedNeut, engageRules);
            }
            else
            {
                var closestNeutNotTargetted = EntityHelper.FindClosestEntity(entities);

                if (closestNeutNotTargetted != null)
                {
                    ActivateModules(myMe, myEve, closestNeutNotTargetted, engageRules);

                    if (engageRules.GoBrawl ?? DefaultGoBrawl)
                    {
                        closestNeutNotTargetted.Entity.Approach();
                        myEve.Execute(ExecuteCommand.CmdAccelerate);
                    }
                }
            }
            return;
        }


        /// <summary>
        ///     Activates the tackle modules.
        /// </summary>
        /// <param name="myMe">My me.</param>
        /// <param name="myEve">My eve.</param>
        /// <param name="enemy">The enemy.</param>
        /// <param name="engageRules"></param>
        private static void ActivateModules(Character myMe, global::EVE.ISXEVE.EVE myEve, EntityViewModel enemy,
            EngageRules engageRules)
        {
            var modules = myMe.Ship.GetModules();

            foreach (var module in modules)
            {
                var mItem = module.ToItem;

                var name = mItem.Name.ToLower();

                if (name.Contains("sensor booster"))
                {
                    module.Activate();
                }
                else if (name.Contains("booster"))
                {
                    //skip
                }
                else if (name.Contains("repairer)"))
                {
                    //skip
                }
                else if (name.Contains("cloak"))
                {
                    module.Deactivate();
                }
                else if (name.Contains("cyno"))
                {
                    //skip
                }
                else if (name.Contains("bomb"))
                {
                    //skip
                }
                else if (name.Contains("microwarp"))
                {
                    if (engageRules.UsePropulsion ?? DefaultActivatePropulsion)
                    {
                        module.Activate();
                    }
                }
                else if (name.Contains("afterburner"))
                {
                    if (engageRules.UsePropulsion ?? DefaultActivatePropulsion)
                    {
                        module.Activate();
                    }
                }
                else
                {
                    module.Activate();
                }
            }

            //engage drones last because modules might improve their effectiveness
            var myDrones = myMe.GetActiveDrones().Select(x => x.ID).ToList();

            myEve.DronesEngageMyTarget(myDrones);
        }

        /// <summary>
        ///     Targets the neuts.
        /// </summary>
        /// <param name="neuts">The neuts.</param>
        /// <returns></returns>
        private static IEnumerable<EntityViewModel> TargetNeuts(IEnumerable<EntityViewModel> neuts)
        {
            foreach (var neut in neuts)
            {
                var entity = neut.Entity;

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

        /// <summary>
        /// Finds the engageable targets.
        /// </summary>
        /// <param name="myMe">My me.</param>
        /// <param name="myEVE">My eve.</param>
        /// <param name="entities">The entities.</param>
        /// <param name="engageRules">The engage rules.</param>
        /// <returns></returns>
        public static IEnumerable<EntityViewModel> FindEngageableTargets(Character myMe, global::EVE.ISXEVE.EVE myEVE, IEnumerable<EntityViewModel> entities, EngageRules engageRules)
        {
            var allNeutrals = entities.Where(
                                        x =>
                                            x.Entity.IsValid &&
                                            x.EntityStandings <= 0 &&
                                            x.EntityDistanceTo <= (engageRules.MaxRange ?? DefaultEngageRange)
                                        );

            if (!allNeutrals.Any())
            {
                return new List<EntityViewModel>();
            }

            //sort by capsule
            allNeutrals = allNeutrals.OrderBy(x => x.EntityGroup);

            var limitedTargets = allNeutrals.Take((int) myMe.MaxLockedTargets);

            return limitedTargets;
        }
    }
}