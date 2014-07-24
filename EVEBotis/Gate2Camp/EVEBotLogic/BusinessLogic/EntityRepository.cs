using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        public static ObservableCollection<EntityViewModel> GetLocalGridEntities(Me myMe, EVE.ISXEVE.EVE myEVE)
        {
            try
            {
                IEnumerable<Entity> entities = myEVE.QueryEntities().Where(x => x.IsPC);

                var oEntities = new ObservableCollection<EntityViewModel>();

                foreach (Entity entity in entities)
                {
                    int standings = EntityHelper.ComputeStandings(myMe, entity);

                    oEntities.Add(new EntityViewModel {Entity = entity, EntityStandings = standings});
                }

                return oEntities;
            }
            catch (Exception e)
            {
                InnerSpace.Echo("GET LOCAL GRID ENTITIES ERROR :" + e.Message);

                return new ObservableCollection<EntityViewModel>();
            }
        }
    }
}