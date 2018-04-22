using System.Linq;
using System.Threading;
using NorthwindLibrary;

namespace CachingSolutionsSamples.Managers
{
	public class CustomersManager : BaseManager<Customer>
    {
        public CustomersManager(IEntitiesCache<Customer> cache)
            : base(cache)
        {

        }

        protected override void GetDataFromDB()
		{
		    using (var dbContext = GetNorthwindContext())
		    {
		        Entities = dbContext.Customers.ToList();
		        Cache.Set(Thread.CurrentPrincipal.Identity.Name, Entities);
            }
		}

        protected override void AddItemToDbContext(Northwind dbContext, Customer newItem)
        {
            dbContext.Customers.Add(newItem);
        }
    }
}
