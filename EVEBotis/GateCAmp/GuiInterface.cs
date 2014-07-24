using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnerSpaceAPI;

namespace GateCAmp
{
    public static class GuiInterface
    {
        public static List<string> EntityNameList { get; set; }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public static void Init()
        {
            MainGateCamp.EntityList.CollectionChanged += UpdateEntityNameList;
        }

        /// <summary>
        /// Updates the entity name list.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private static void UpdateEntityNameList(object sender, NotifyCollectionChangedEventArgs e)
        {
            var entityNames = MainGateCamp.EntityList.Select(x => x.Name);

            EntityNameList = entityNames.ToList();

            foreach (var entityName in entityNames)
            {
                InnerSpace.Echo(entityName);
            }
        }
    }
}
