using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindLibrary;
using System.Linq;
using System.Threading;
using CachingSolutionsSamples.Managers;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CachingSolutionsSamples
{
	[TestFixture]
	public class CacheTests
	{
		[Test, Category("Integration")]
		public void MemoryCache()
		{
			var categoryManager = new CategoriesManager(new EntitiesMemoryCache<Category>());
		    Assert.IsTrue(categoryManager.GetEntites().Any());
		}

		[Test, Category("Integration")]
		public void RedisCache()
		{
			var categoryManager = new CategoriesManager(new EntitiesRedisCache<Category>("localhost"));
            Assert.IsTrue(categoryManager.GetEntites().Any());
		}

	    [Test, Category("Integration")]
	    public void MemoryCacheTimeDuration()
	    {
	        var categoryManager =
	            new CategoriesManager(new EntitiesMemoryCache<Category> {ExpirationTime = new TimeSpan(0, 0, 0, 10)});
	        var categoryCount = categoryManager.GetEntites().Count();

	        categoryManager.AddItem(new Category {CategoryName = "test"});

	        Thread.Sleep(11000);

	        Assert.IsTrue(++categoryCount == categoryManager.GetEntites().Count());
	    }

	    [Test, Category("Integration")]
	    public void RedisCacheTimeDuration()
	    {
	        var categoryManager = new CategoriesManager(new EntitiesRedisCache<Category>("localhost") { ExpirationTime = new TimeSpan(0, 0, 0, 10) });
	        var categoryCount = categoryManager.GetEntites().Count();

	        categoryManager.AddItem(new Category { CategoryName = "test" });

	        Thread.Sleep(11000);

	        Assert.IsTrue(++categoryCount == categoryManager.GetEntites().Count());
        }

        [Test, Category("Manual")]
	    public void RunManager()
	    {
	        var ordersMemoryManager = new OrdersManager(new EntitiesMemoryCache<Order>());
            var ordersRedisManager = new OrdersManager(new EntitiesRedisCache<Order>("localhost"));
	        var categoryMemoryManager = new CategoriesManager(new EntitiesMemoryCache<Category>());
	        var categoryRedisManager = new CategoriesManager(new EntitiesRedisCache<Category>("localhost"));
	        var customersMemoryManager = new CustomersManager(new EntitiesMemoryCache<Customer>());
	        var customersRedisManager = new CustomersManager(new EntitiesRedisCache<Customer>("localhost"));
        }
    }
}
