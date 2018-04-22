using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonachi.Interfaces
{
    public interface ICacher
    {
        bool Contains(int key);
        int TryGetValue(int key);
        void Add(int key, int value);
        int this[int index] { get; }
        List<int> GetResult(int lastIndex);
    }
}
