﻿using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using ServicesAccessibilityChecker.Context;
using ServicesAccessibilityChecker.Models.Rm;
using System;
using System.Linq;

namespace ServicesAccessibilityChecker.Scheduling
{
    public class Repository
    {
        private readonly ILogger<Repository> _logger;

        public Repository(ILogger<Repository> logger)
        {
            _logger = logger;
        }

        public string SaveResponse(int i, double mseconds, IRestResponse response)
        {
            bool addedSuccessfully = AddToDb(i, response, mseconds);
            if (!addedSuccessfully)
            {
                return string.Empty;
            }

            StatusRm statusRm = new StatusRm()
            {
                IsAvailable = response.IsSuccessful,
                ServiceName = response.Content,
                ResponseDuration = mseconds,
            };
            return JsonConvert.SerializeObject(statusRm);
        }

        private bool AddToDb(int id, IRestResponse response, double mseconds)
        {
            //todo использую три таблицы с одинаковыми данными, своего рода шардинг предполагала, 
            //можно было бы сделать одну таблицу, тогда код уменьшился бы в три раза
            try
            {
                using (ServicesDbContext dbContext = new ServicesDbContext())
                {
                    if (id == 0)
                    {
                        Refdata refdata = new Refdata()
                        {
                            CreatedDate = DateTime.UtcNow,
                            IsAvailable = response.IsSuccessful,
                            ResponseDuration = mseconds,
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
                            ResponseDuration = mseconds,
                            LastHourErrors = dbContext.Ibonuses.Count(r => r.CreatedDate > DateTime.UtcNow.AddMinutes(-60) && r.IsAvailable == false),
                            LastDayErrors = dbContext.Ibonuses.Count(r => r.CreatedDate > DateTime.UtcNow.AddDays(-1) && r.IsAvailable == false)
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
                            ResponseDuration = mseconds,
                            LastHourErrors = dbContext.Catalogs.Count(r => r.CreatedDate > DateTime.UtcNow.AddMinutes(-60) && r.IsAvailable == false),
                            LastDayErrors = dbContext.Catalogs.Count(r => r.CreatedDate > DateTime.UtcNow.AddDays(-1) && r.IsAvailable == false)
                        };

                        dbContext.Catalogs.Add(catalog);
                        dbContext.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Wasn't able to save objects to databse, ex: {e}");
                return false;
            }
        }
    }
}
