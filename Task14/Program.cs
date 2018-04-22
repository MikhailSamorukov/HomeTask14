using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fibonachi;
using Fibonachi.Cachers;

namespace Task14
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите кол-во чисел");
            try
            {
                var numbersCount = Convert.ToInt32(Console.ReadLine());
                FibonachiCounter fib = new FibonachiCounter(new RedisCache());
                foreach (var item in fib.GetResult(numbersCount))
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadLine();
        }
    }
}
