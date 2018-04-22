using NorthwindLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachingSolutionsSamples
{
	public interface IEntitiesCache<TEntity>
	{
	    TimeSpan? ExpirationTime { get; set; }
		IEnumerable<TEntity> Get(string forUser);
		void Set(string forUser, IEnumerable<TEntity> entities);
	}
}
