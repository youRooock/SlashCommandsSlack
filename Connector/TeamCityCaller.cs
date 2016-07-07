using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Connector.Models;

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

        public async Task<TeamCityBuildInfo> GetLastBuildInfo(string buildId)
        {
          return await GetBuild(buildId);
        }

        public void GetArtifacts(string buildId)
        {
            throw new NotImplementedException();
        }

        private async Task<TeamCityBuildInfo> GetBuild(string buildType)
        {
          string id;
          var buildInfo = await CallAsync("http://webintegration.plarium.local:8080/httpAuth/app/rest/buildTypes/" + buildType + "/builds?locator=start:0,count:1", "GET", _credentials);
          var xml =  await buildInfo.Content.ReadAsStringAsync();
          var document = new XmlDocument();
          document.LoadXml(xml);

          try
          {
            id = document.GetElementsByTagName("build")[0].Attributes["id"].Value;
          }
          catch (NullReferenceException e)
          {
            return null;
          }           

          DownloadFileAsync("http://webintegration.plarium.local:8080/httpAuth/downloadBuildLog.html?buildId=" + id, _credentials);

          TeamCityBuildInfo teamcityBuildModel;

          using (var reader = new StreamReader(ConfigurationManager.AppSettings["logFileLocation"]))
          {
            var data = await reader.ReadToEndAsync();
            var regexPassed = new Regex(@"Passed: (\d+)");
            var regexFailed = new Regex(@"Failures: (\d+)");
            var regexErrors = new Regex(@"Errors: (\d+)");

            teamcityBuildModel = new TeamCityBuildInfo
            {
              Passed = regexPassed.Match(data).GetNumbers(),
              Failed = regexFailed.Match(data).GetNumbers(),
              Errors = regexErrors.Match(data).GetNumbers()
            };
          }

          return teamcityBuildModel;
        }
    }
}
