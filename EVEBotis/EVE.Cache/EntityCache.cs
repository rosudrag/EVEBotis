using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using EVE.Core.Model;
using EVE.ISXEVE;

namespace EVE.Cache
{
    /// <summary>
    /// 
    /// </summary>
    public static class EntityCache
    {
        /// <summary>
        /// Gets or sets the cache.
        /// </summary>
        /// <value>
        /// The cache.
        /// </value>
        private static readonly MemoryCache Cache = new MemoryCache("EntityCache");


        /// <summary>
        /// Gets the global policy.
        /// </summary>
        /// <value>
        /// The global policy.
        /// </value>
        private static CacheItemPolicy GlobalPolicy
        {
            get
            {
                var policy = new CacheItemPolicy {AbsoluteExpiration = new DateTimeOffset()};

                return policy;
            }
        }

        /// <summary>
        /// Adds the specified evm.
        /// </summary>
        /// <param name="evm">The evm.</param>
        public static void Add(EntityViewModel evm)
        {
            Cache.Add(evm.EntityName, evm, GlobalPolicy);
        }

        /// <summary>
        /// Gets the specified evm.
        /// </summary>
        /// <param name="evm">The evm.</param>
        /// <returns></returns>
        public static EntityViewModel Get(EntityViewModel evm)
        {
            if (!Cache.Contains(evm.EntityName))
            {
                return null;
            }
            return (EntityViewModel) Cache.Get(evm.EntityName);
        }

        /// <summary>
        /// Removes the specified evm.
        /// </summary>
        /// <param name="evm">The evm.</param>
        /// <returns></returns>
        public static bool Remove(EntityViewModel evm)
        {
            if (Cache.Contains(evm.EntityName))
            {
                var objectToRemove = Cache.Remove(evm.EntityName);

                return objectToRemove != null;
            }
            return false;
        }
    }
}
