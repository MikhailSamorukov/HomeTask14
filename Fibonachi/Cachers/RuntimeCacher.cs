using System.Collections.Generic;
using System.Linq;
using Fibonachi.Interfaces;
using System.Runtime.Caching;

namespace Fibonachi.Cachers
{
    public class RuntimeCacher : ICacher
    {
        private readonly MemoryCache _cache;

        public RuntimeCacher()
        {
            _cache = new MemoryCache("FibonachiCacher");
        }

        public bool Contains(int key)
        {
            return _cache.Contains(key.ToString());
        }

        public int TryGetValue(int key)
        {
            return Contains(key) ? this[key] : 0;
        }

        public void Add(int key, int value)
        {
            _cache.Add(key.ToString(), value, ObjectCache.InfiniteAbsoluteExpiration);
        }

        public int this[int index] => (int) _cache[index.ToString()];

        public List<int> GetResult(int lastIndex)
        {
            return _cache.OrderBy(i=>i.Value).Take(lastIndex).Select(i => (int) i.Value).ToList();
        }
    }
}
