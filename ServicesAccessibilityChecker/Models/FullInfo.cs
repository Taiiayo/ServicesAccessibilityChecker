using ServicesAccessibilityChecker.Context;
using System.Linq;

namespace ServicesAccessibilityChecker.Models
{
    public class FullInfo
    {
        public object ReturnFullInfo(int serviceId)
        {
            using (ServicesDbContext dbContext = new ServicesDbContext())
            {
                if (serviceId == 0)
                {
                    return dbContext.Refdatas.Last();
                }
                else if (serviceId == 1)
                {
                    return dbContext.Ibonuses.Last();
                }
                else
                {
                    return dbContext.Catalogs.Last();
                }
            }
        }
    }
}
