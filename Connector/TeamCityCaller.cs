using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        private readonly string _teamCityArtifactsLocation;
        private readonly string _artifactPath;
        private readonly string _logFilePath;
    
        public TeamCityCaller()
        {
          _credentials = ConfigurationManager.AppSettings["teamcityCredentials"];
          _url = ConfigurationManager.AppSettings["teamcityUrl"];
          _teamCityArtifactsLocation = ConfigurationManager.AppSettings["teamcityArtifactsLocation"];
          _artifactPath = ConfigurationManager.AppSettings["artifact"];
          _logFilePath = ConfigurationManager.AppSettings["logFileLocation"];     
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

        public NameValueCollection GetArtifacts(string buildId)
        {
          var collection = new NameValueCollection();

          try
          {
            var directory =
              new DirectoryInfo(_teamCityArtifactsLocation + buildId)
                .GetDirectories()
                .OrderBy(r => r.LastWriteTime)
                 .LastOrDefault();

            if (directory != null)
            {
              var file = directory.GetFiles().Where(r => r.Name == "TestResult.html").FirstOrDefault();
              collection.Add("time", file.LastWriteTime.ToString());

              File.Copy(file.DirectoryName + "\\" + file.Name, _artifactPath + buildId + "\\" + "TestResult.html", true);
            }
          }
          catch (Exception e)
          {
            return null;
          }

          collection.Add("url", ConfigurationManager.AppSettings["remoteUrl"] + buildId + "/TestResult.html");

          return collection;
        }
      
        private async Task<TeamCityBuildInfo> GetBuild(string buildType)
        {
          string id;
          var buildInfo = await CallAsync(_url + "/httpAuth/app/rest/buildTypes/" + buildType + "/builds?locator=start:0,count:1", "GET", _credentials);
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

          DownloadFileAsync(_url + "/httpAuth/downloadBuildLog.html?buildId=" + id, _credentials);

          TeamCityBuildInfo teamcityBuildModel;

          using (var reader = new StreamReader(_logFilePath))
          {
            var data = await reader.ReadToEndAsync();
            var regexPassed = new Regex(@"Passed: (\d+)");
            var regexFailed = new Regex(@"Failures: (\d+)");
            var regexErrors = new Regex(@"Errors: (\d+)");

            teamcityBuildModel = new TeamCityBuildInfo
            {
              BuildId = id,
              Passed = regexPassed.Match(data).GetNumbers(),
              Failed = regexFailed.Match(data).GetNumbers(),
              Errors = regexErrors.Match(data).GetNumbers()
            };
          }

          return teamcityBuildModel;
        }
    }
}
