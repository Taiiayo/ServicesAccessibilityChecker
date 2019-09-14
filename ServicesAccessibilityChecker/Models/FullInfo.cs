using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServicesAccessibilityChecker.Context;
using ServicesAccessibilityChecker.Models.Rm;
using System;
using System.Linq;

namespace ServicesAccessibilityChecker.Models
{
    public class FullInfo : IFullInfo
    {
        private readonly ILogger<FullInfo> _logger;
        private readonly IConfiguration _config;

        public FullInfo(
            ILogger<FullInfo> logger,
            IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public string ReturnFullInfo(int serviceId)
        {   //todo использую три таблицы с одинаковыми данными, своего рода шардинг предполагала, 
            //можно было бы сделать одну таблицу, тогда код уменьшился бы в три раза
            
            try
            {
                using (ServicesDbContext dbContext = new ServicesDbContext())
                {
                    if (serviceId == 0)
                    {
                        double RefdataBestTime = _config.GetSection("ResponseBestTime:MaxRefdataResponseDuration").Get<double>();
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
                            LastHourMaxResponseDuration = dbContext.Refdatas.Where(r => r.CreatedDate > DateTime.UtcNow.AddMinutes(-60)).Select(x => x.ResponseDuration).Max(),
                            LastDayMaxResponseDuration = dbContext.Refdatas.Where(r => r.CreatedDate > DateTime.UtcNow.AddDays(-1)).Select(x => x.ResponseDuration).Max()
                        };
                        statusRm.LastHourResponseDeviationTime = statusRm.LastHourAvgResponseDuration - RefdataBestTime;
                        statusRm.LastDayResponseDeviationTime = statusRm.LastDayAvgResponseDuration - RefdataBestTime;
                        string serializedDto = JsonConvert.SerializeObject(statusRm);
                        return serializedDto;
                    }
                    else if (serviceId == 1)
                    {
                        double IbonusBestTime = _config.GetSection("ResponseBestTime:MaxIbonusResponseDuration").Get<double>();
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
                            LastHourMaxResponseDuration = dbContext.Ibonuses.Where(r => r.CreatedDate > DateTime.UtcNow.AddMinutes(-60)).Select(x => x.ResponseDuration).Max(),
                            LastDayMaxResponseDuration = dbContext.Ibonuses.Where(r => r.CreatedDate > DateTime.UtcNow.AddDays(-1)).Select(x => x.ResponseDuration).Max()
                        };
                        statusRm.LastHourResponseDeviationTime = statusRm.LastHourAvgResponseDuration - IbonusBestTime;
                        statusRm.LastDayResponseDeviationTime = statusRm.LastDayAvgResponseDuration - IbonusBestTime;
                        string serializedDto = JsonConvert.SerializeObject(statusRm);
                        return serializedDto;
                    }
                    else
                    {
                        double CatalogBestTime = _config.GetSection("ResponseBestTime:MaxCatalogResponseDuration").Get<double>();
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
                            LastHourMaxResponseDuration = dbContext.Catalogs.Where(r => r.CreatedDate > DateTime.UtcNow.AddMinutes(-60)).Select(x => x.ResponseDuration).Max(),
                            LastDayMaxResponseDuration = dbContext.Catalogs.Where(r => r.CreatedDate > DateTime.UtcNow.AddDays(-1)).Select(x => x.ResponseDuration).Max()
                        };
                        statusRm.LastHourResponseDeviationTime = statusRm.LastHourAvgResponseDuration - CatalogBestTime;
                        statusRm.LastDayResponseDeviationTime = statusRm.LastDayAvgResponseDuration - CatalogBestTime;
                        string serializedDto = JsonConvert.SerializeObject(statusRm);
                        return serializedDto;
                    }
                }
            }
            catch
            {
                _logger.LogError("Wasn't able to get objects from databse");
                return string.Empty;
            }
        }
    }
}
