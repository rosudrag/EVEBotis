using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EVE.Cache;
using EVE.Core.Model;
using EVE.ISXEVE;
using Gate2Camp.EVEBotLogic.Common;
using Gate2Camp.ViewModels;
using InnerSpaceAPI;

namespace Gate2Camp.EVEBotLogic.BusinessLogic
{
    public class EntityRepository
    {
        /// <summary>
        ///     Gets the local grid entities.
        /// </summary>
        /// <param name="myMe"></param>
        /// <param name="myEVE">My eve.</param>
        /// <returns></returns>
        public static IEnumerable<EntityViewModel> GetLocalGridEntities(Character myMe, EVE.ISXEVE.EVE myEVE)
        {
            try
            {
                IEnumerable<Entity> entities = myEVE.QueryEntities().Where(x => x.IsPC).ToList();

                var oEntities = new List<EntityViewModel>();

                foreach (Entity entity in entities)
                {                  
                    int standings = EntityHelper.ComputeStandings(myMe, entity);

                    var newEntity = new EntityViewModel {Entity = entity, EntityStandings = standings};

                    var cachedEntity = EntityCache.Get(newEntity);

                    if (cachedEntity == null)
                    {
                        EntityCache.Add(newEntity);
                    }
                    else
                    {
                        if (cachedEntity.EntityStandings != newEntity.EntityStandings)
                        {
                            if (newEntity.EntityStandings > cachedEntity.EntityStandings)
                            {
                                EntityCache.Remove(newEntity);
                                EntityCache.Add(newEntity);
                            }

                            if (newEntity.EntityStandings == 0 && cachedEntity.EntityStandings != 0)
                            {
                                newEntity.EntityStandings = cachedEntity.EntityStandings;
                            }
                        }
                    }

                    oEntities.Add(newEntity);
                }

                return oEntities;
            }
            catch (Exception e)
            {
                InnerSpace.Echo("GET LOCAL GRID ENTITIES ERROR :" + e.Message);

                return new List<EntityViewModel>();
            }
        }
    }
}