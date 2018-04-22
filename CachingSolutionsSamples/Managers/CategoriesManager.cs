using System.Linq;
using System.Threading;
using NorthwindLibrary;

namespace CachingSolutionsSamples.Managers
{
	public class CategoriesManager : BaseManager<Category>
    {
        public CategoriesManager(IEntitiesCache<Category> cache)
            : base(cache)
        {

        }

        protected override void GetDataFromDB()
		{
		    using (var dbContext = GetNorthwindContext())
		    {
		        Entities = dbContext.Categories.ToList();
		        Cache.Set(Thread.CurrentPrincipal.Identity.Name, Entities);
            }
		}

        protected override void AddItemToDbContext(Northwind dbContext, Category newItem)
        {
            dbContext.Categories.Add(newItem);
        }
    }
}
