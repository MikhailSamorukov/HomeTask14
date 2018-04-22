using System.Linq;
using System.Threading;
using NorthwindLibrary;

namespace CachingSolutionsSamples.Managers
{
	public class OrdersManager : BaseManager<Order>
    {
        public OrdersManager(IEntitiesCache<Order> cache)
            : base(cache)
        {

        }

        protected override void GetDataFromDB()
		{
		    using (var dbContext = GetNorthwindContext())
		    {
		        Entities = dbContext.Orders.ToList();
		        Cache.Set(Thread.CurrentPrincipal.Identity.Name, Entities);
            }
		}

        protected override void AddItemToDbContext(Northwind dbContext, Order newItem)
        {
            dbContext.Orders.Add(newItem);
        }
    }
}
