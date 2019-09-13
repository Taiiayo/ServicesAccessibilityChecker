using RestSharp;
using ServicesAccessibilityChecker.Context;
using System;
using System.Diagnostics;
using System.Linq;

namespace ServicesAccessibilityChecker.Scheduling
{
    public class Repository
    {
        public void AddToDb(int id, IRestResponse response, Stopwatch stopwatch)
        {
            using (ServicesDbContext dbContext = new ServicesDbContext())
            {
                if (id == 0)
                {
                    Refdata refdata = new Refdata()
                    {
                        CreatedDate = DateTime.UtcNow,
                        IsAvailable = response.IsSuccessful,
                        ResponseDuration = stopwatch.ElapsedMilliseconds,
                        LastHourErrors = dbContext.Refdatas.Count(r => r.CreatedDate > DateTime.UtcNow.AddMinutes(-60) && r.IsAvailable == false),
                        LastDayErrors = dbContext.Refdatas.Count(r => r.CreatedDate > DateTime.UtcNow.AddDays(-1) && r.IsAvailable == false)                      
                    };

                    dbContext.Refdatas.Add(refdata);
                    dbContext.SaveChanges();
                }
                else if (id == 1)
                {
                    Ibonus ibonus = new Ibonus()
                    {
                        CreatedDate = DateTime.UtcNow,
                        IsAvailable = response.IsSuccessful,
                        ResponseDuration = stopwatch.ElapsedMilliseconds,
                        LastHourErrors = dbContext.Refdatas.Count(r => r.CreatedDate > DateTime.UtcNow.AddMinutes(-60) && r.IsAvailable == false),
                        LastDayErrors = dbContext.Refdatas.Count(r => r.CreatedDate > DateTime.UtcNow.AddDays(-1) && r.IsAvailable == false)
                    };
                    dbContext.Ibonuses.Add(ibonus);
                    dbContext.SaveChanges();
                }
                else
                {
                    Catalog catalog = new Catalog()
                    {
                        CreatedDate = DateTime.UtcNow,
                        IsAvailable = response.IsSuccessful,
                        ResponseDuration = stopwatch.ElapsedMilliseconds,
                        LastHourErrors = dbContext.Refdatas.Count(r => r.CreatedDate > DateTime.UtcNow.AddMinutes(-60) && r.IsAvailable == false),
                        LastDayErrors = dbContext.Refdatas.Count(r => r.CreatedDate > DateTime.UtcNow.AddDays(-1) && r.IsAvailable == false)
                    };

                    dbContext.Catalogs.Add(catalog);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}
