using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NorthwindLibrary;

namespace CachingSolutionsSamples.Managers
{
    public abstract class BaseManager<TEntity>
    {
        protected readonly IEntitiesCache<TEntity> Cache;

        protected IEnumerable<TEntity> Entities { get; set; }

        protected BaseManager(IEntitiesCache<TEntity> cache)
        {
            Cache = cache;
        }

        public IEnumerable<TEntity> GetEntites()
        {
            GetCachedData();
            GetDataFromDB();
            return Entities;
        }

        public void AddItem(TEntity item)
        {
            Console.WriteLine($"Adding of {typeof(TEntity).Name} item");
            using (var dbContext = GetNorthwindContext())
            {
                AddItemToDbContext(dbContext, item);
                dbContext.SaveChanges();
            }
        }

        protected abstract void GetDataFromDB();
        protected abstract void AddItemToDbContext(Northwind dbContext, TEntity newItem);

        protected Northwind GetNorthwindContext()
        {
            var northwind = new Northwind();
            northwind.Configuration.LazyLoadingEnabled = false;
            northwind.Configuration.ProxyCreationEnabled = false;
            return northwind;
        }

        private void GetCachedData()
        {
            Console.WriteLine("Get Categories");

            var user = Thread.CurrentPrincipal.Identity.Name;
            var entities = Cache.Get(user);

            if (entities != null) Entities = entities;
            Console.WriteLine("From DB");
        }
    }
}
