using EVE.ISXEVE;
using MicroMvvm;

namespace Gate2Camp.ViewModels
{
    public class EntityViewModel : ObservableObject
    {
        /// <summary>
        ///     The entity
        /// </summary>
        private Entity entity;

        /// <summary>
        ///     Gets or sets the entity.
        /// </summary>
        /// <value>
        ///     The entity.
        /// </value>
        public Entity Entity
        {
            get { return entity; }
            set
            {
                entity = value;
            }
        }

        /// <summary>
        ///     Gets or sets the name of the entity.
        /// </summary>
        /// <value>
        ///     The name of the entity.
        /// </value>
        public string EntityName
        {
            get { return Entity.Name; }
        }

        public string EntityGroup
        {
            get { return entity.Group; }
        }

        public string EntityCloaked
        {
            get { return entity.IsCloaked ? "Cloaked" : "NotCloaked"; }
        }

        public double EntityDistanceTo
        {
            get { return entity.Distance; }
        }

        public int EntityStandings { get; set; }
    }
}