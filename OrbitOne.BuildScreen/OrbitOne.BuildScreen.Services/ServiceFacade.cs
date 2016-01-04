using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using OrbitOne.BuildScreen.Models;

namespace OrbitOne.BuildScreen.Services
{
    public class ServiceFacade : IServiceFacade
    {
        private readonly IList<IService> _services;

        public ServiceFacade(IList<IService> services)
        {
            _services = services;
        }

        public List<BuildInfoDto> GetBuilds(string dateString = null)
        {
            var allBuilds = new List<BuildInfoDto>();
            try
            {
                if (dateString == null)
                {
                    LogService.WriteInfo("Lege datum, getbuildinfodtos");
                    Parallel.ForEach(_services, service => allBuilds.AddRange(service.GetBuildInfoDtos()));
                }
                else
                {
                    Parallel.ForEach(_services, service => allBuilds.AddRange(service.GetBuildInfoDtosPolling(dateString)));
                }
            }
            //catch (AggregateException ea)
            //{
            //    LogService.WriteInfo("AggregateException");
            //    LogService.WriteError(ea);
            //    foreach (var e in ea.InnerExceptions)
            //    {
            //        LogService.WriteInfo(e.Message);
            //        LogService.WriteError(e);               
            //    }
            //     throw;
            //}
            catch (Exception e)
            { 
                LogService.WriteError(e);
                throw;
            }

            return allBuilds;
        }
    }
}
