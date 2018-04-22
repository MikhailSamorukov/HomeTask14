using System.Collections.Generic;
using Fibonachi.Interfaces;
using StackExchange.Redis;
using ServiceStack.Redis;

namespace Fibonachi.Cachers
{
    public class RedisCache : ICacher
    {
        private readonly IDatabase _cache;

        public RedisCache()
        {
            var redisConnection = ConnectionMultiplexer.Connect("localhost");
            _cache = redisConnection.GetDatabase();
        }

        public bool Contains(int key)
        {
            var result = _cache.StringGet(key.ToString());
            return result != RedisValue.Null;
        }

        public int TryGetValue(int key)
        {
            return Contains(key) ? this[key] : 0;
        }

        public void Add(int key, int value)
        {
            _cache.StringSet(key.ToString(), value);
        }

        public int this[int index] => (int)_cache.StringGet(index.ToString());

        public List<int> GetResult(int lastIndex)
        {
            var result = new List<int>();
            for (var i = 0; i < lastIndex; i++)
            {
                result.Add(this[i]);
            }

            return result;
        }
    }
}