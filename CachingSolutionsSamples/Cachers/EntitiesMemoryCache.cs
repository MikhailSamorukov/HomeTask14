using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindLibrary;
using System.Runtime.Caching;

namespace CachingSolutionsSamples
{
	internal class EntitiesMemoryCache<TEntity> : IEntitiesCache<TEntity>
	{
	    private readonly ObjectCache _cache = MemoryCache.Default;
	    private readonly string _prefix = $"Cache_{typeof(TEntity).Name}";

	    public TimeSpan? ExpirationTime { get; set; }

	    public IEnumerable<TEntity> Get(string forUser)
		{
			return (IEnumerable<TEntity>) _cache.Get(_prefix + forUser);
		}

		public void Set(string forUser, IEnumerable<TEntity> entities)
		{
		    var expirationTime = ExpirationTime != null ? DateTime.Now.Add((TimeSpan)ExpirationTime) : ObjectCache.InfiniteAbsoluteExpiration;

            _cache.Set(_prefix + forUser, entities, expirationTime);
		}
	}
}
