using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using Fibonachi.Interfaces;

namespace Fibonachi
{
    public class FibonachiCounter
    {
        private readonly ICacher _cache;

        public FibonachiCounter(ICacher cache)
        {
            _cache = cache;
        }

        public List<int> GetResult(int countNumbers)
        {
            for (var i = 0; i < countNumbers; i++)
            {
                var result = i < 1 ? 1 : _cache.TryGetValue(i - 1) + _cache.TryGetValue(i - 2);
                _cache.Add(i, result);
            }

            return _cache.GetResult(countNumbers);
        }
    }
}
