using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindLibrary;
using StackExchange.Redis;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace CachingSolutionsSamples
{
    internal class EntitiesRedisCache<TEntity> : IEntitiesCache<TEntity>
	{
	    public TimeSpan? ExpirationTime { get; set; }

        private readonly ConnectionMultiplexer _redisConnection;
	    private readonly string _prefix = $"Cache_{typeof(TEntity).Name}";

	    private readonly DataContractSerializer _serializer = new DataContractSerializer(
			typeof(IEnumerable<TEntity>));

		public EntitiesRedisCache(string hostName)
		{
			_redisConnection = ConnectionMultiplexer.Connect(hostName);
		}

	    public IEnumerable<TEntity> Get(string forUser)
		{
			var db = _redisConnection.GetDatabase();
			byte[] cachData = db.StringGet(_prefix + forUser);
			if (cachData == null)
				return null;

			return (IEnumerable<TEntity>)_serializer
				.ReadObject(new MemoryStream(cachData));
		}

		public void Set(string forUser, IEnumerable<TEntity> entities)
		{
			var db = _redisConnection.GetDatabase();
			var key = _prefix + forUser;

			if (entities == null)
			{
				db.StringSet(key, RedisValue.Null);
			}
		    else
		    {
		        var stream = new MemoryStream();
		        _serializer.WriteObject(stream, entities);

		        if (ExpirationTime == null)
		            db.StringSet(key, stream.ToArray());
		        else
		            db.StringSet(key, stream.ToArray(), ExpirationTime);
		    }
		}
	}
}
