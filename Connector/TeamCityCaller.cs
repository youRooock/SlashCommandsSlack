using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connector
{
    public class TeamCityCaller : ServiceCaller, IContiniousIntegrationCaller
    {
        private readonly string _credentials;
        private readonly string _url;

        public TeamCityCaller()
        {
            _credentials = ConfigurationManager.AppSettings["teamcityCredentials"];
            _url = ConfigurationManager.AppSettings["teamcityUrl"];
        }

        public async void QueueBuild(string buildId)
        {
          if (buildId == null)
            return;

          var requestedUrl = _url + "/httpAuth/action.html?add2Queue=" + buildId;
          await CallAsync(requestedUrl, "POST", _credentials);
        }

        public void GetLastBuildInfo(string buildId)
        {
            throw new NotImplementedException();
        }

        public void GetArtifacts(string buildId)
        {
            throw new NotImplementedException();
        }
    }
}
