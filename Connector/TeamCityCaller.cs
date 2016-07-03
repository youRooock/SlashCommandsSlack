using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connector
{
    public class TeamCityCaller : ServiceCaller, IContiniousIntegrationCaller
    {
        public void QueueBuild(string buildId)
        {
           // CallAsync();
        }

        public void GetLastBuildInfo(string buildId)
        {

        }

        public void GetArtifacts(string buildId)
        {

        }
    }
}
