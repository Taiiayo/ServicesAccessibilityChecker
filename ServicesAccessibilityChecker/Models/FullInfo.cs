using Newtonsoft.Json.Linq;
using ServicesAccessibilityChecker.Context;
using ServicesAccessibilityChecker.Models.Rm;
using System;
using System.IO;
using System.Linq;

namespace ServicesAccessibilityChecker.Models
{
    public class FullInfo
    {
        private JToken Config => JToken.Parse(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "appsettings.json")));
        private int RefdataBestTime => int.Parse(Config["ResponseBestTime"]["MaxRefdataResponseDuration"].ToString());
        private int IbonusBestTime => int.Parse(Config["ResponseBestTime"]["MaxIbonusResponseDuration"].ToString());
        private int CatalogBestTime => int.Parse(Config["ResponseBestTime"]["MaxCatalogResponseDuration"].ToString());
         StatusRm ReturnFullInfo(int serviceId)
        {
            using (ServicesDbContext dbContext = new ServicesDbContext())
            {
                if (serviceId == 0)
                {
                    Refdata refdata = dbContext.Refdatas.Last();
                    StatusRm statusRm = new StatusRm()
                    {
                        IsAvailable = refdata.IsAvailable,
                        LastHourAvgResponseDuration = dbContext.Refdatas.Where(r => r.CreatedDate > DateTime.UtcNow.AddMinutes(-60)).Select(x => x.ResponseDuration).Average(),
                        LastDayAvgResponseDuration = dbContext.Refdatas.Where(r => r.CreatedDate > DateTime.UtcNow.AddDays(-1)).Select(x => x.ResponseDuration).Average(),
                        ResponseDuration = refdata.ResponseDuration,
                        LastDayErrors = refdata.LastDayErrors,
                        LastHourErrors = refdata.LastHourErrors,
                        BestResponseTime = RefdataBestTime,
                        AvgResponseDuration = dbContext.Refdatas.Select(x => x.ResponseDuration).Average(),
                        LastHourMaxResponseDuration = dbContext.Refdatas.Select(x => x.ResponseDuration).Max(),
                        LastDayMaxResponseDuration = dbContext.Refdatas.Select(x => x.ResponseDuration).Max()
                    };
                    statusRm.LastHourResponseDeviationTime = statusRm.LastHourAvgResponseDuration - RefdataBestTime;
                    statusRm.LastDayResponseDeviationTime = statusRm.LastDayAvgResponseDuration - RefdataBestTime;
                    return statusRm;
                }
                else if (serviceId == 1)
                {
                    Ibonus ibonus = dbContext.Ibonuses.Last();
                    StatusRm statusRm = new StatusRm()
                    {
                        IsAvailable = ibonus.IsAvailable,
                        LastHourAvgResponseDuration = dbContext.Ibonuses.Where(r => r.CreatedDate > DateTime.UtcNow.AddMinutes(-60)).Select(x => x.ResponseDuration).Average(),
                        LastDayAvgResponseDuration = dbContext.Ibonuses.Where(r => r.CreatedDate > DateTime.UtcNow.AddDays(-1)).Select(x => x.ResponseDuration).Average(),
                        ResponseDuration = ibonus.ResponseDuration,
                        LastDayErrors = ibonus.LastDayErrors,
                        LastHourErrors = ibonus.LastHourErrors,
                        BestResponseTime = IbonusBestTime,
                        AvgResponseDuration = dbContext.Ibonuses.Select(x => x.ResponseDuration).Average(),
                        LastHourMaxResponseDuration = dbContext.Ibonuses.Select(x => x.ResponseDuration).Max(),
                        LastDayMaxResponseDuration = dbContext.Ibonuses.Select(x => x.ResponseDuration).Max()
                    };
                    statusRm.LastHourResponseDeviationTime = statusRm.LastHourAvgResponseDuration - IbonusBestTime;
                    statusRm.LastDayResponseDeviationTime = statusRm.LastDayAvgResponseDuration - IbonusBestTime;
                    return statusRm;
                }
                else
                {
                    Catalog catalog = dbContext.Catalogs.Last();
                    StatusRm statusRm = new StatusRm()
                    {
                        IsAvailable = catalog.IsAvailable,
                        LastHourAvgResponseDuration = dbContext.Catalogs.Where(r => r.CreatedDate > DateTime.UtcNow.AddMinutes(-60)).Select(x => x.ResponseDuration).Average(),
                        LastDayAvgResponseDuration = dbContext.Catalogs.Where(r => r.CreatedDate > DateTime.UtcNow.AddDays(-1)).Select(x => x.ResponseDuration).Average(),
                        ResponseDuration = catalog.ResponseDuration,
                        LastDayErrors = catalog.LastDayErrors,
                        LastHourErrors = catalog.LastHourErrors,
                        BestResponseTime = CatalogBestTime,
                        AvgResponseDuration = dbContext.Catalogs.Select(x => x.ResponseDuration).Average(),
                        LastHourMaxResponseDuration = dbContext.Catalogs.Select(x => x.ResponseDuration).Max(),
                        LastDayMaxResponseDuration = dbContext.Catalogs.Select(x => x.ResponseDuration).Max()
                    };
                    statusRm.LastHourResponseDeviationTime = statusRm.LastHourAvgResponseDuration - CatalogBestTime;
                    statusRm.LastDayResponseDeviationTime = statusRm.LastDayAvgResponseDuration - CatalogBestTime;
                    return statusRm;
                }
            }
        }
    }
}
