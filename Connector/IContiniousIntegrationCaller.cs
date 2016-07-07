using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connector.Models;

namespace Connector
{
    public interface IContiniousIntegrationCaller
    {
        void QueueBuild(string buildId);
        Task<TeamCityBuildInfo> GetLastBuildInfo(string buildId);
    }
}
