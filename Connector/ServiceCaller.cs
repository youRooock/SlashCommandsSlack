﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Connector
{
  public abstract class ServiceCaller : IServiceCaller
  {
    public async Task<HttpResponseMessage> CallAsync(string url, string method, string netCredentials = null,
      HttpContent data = null)
    {
      if (url == null)
        throw new ArgumentNullException("Requested url is null");

      using (var client = new HttpClient())
      {
        if (netCredentials != null)
        {
          var credentials = Encoding.ASCII.GetBytes(netCredentials);
          client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
            Convert.ToBase64String(credentials));
        }
        if (method == "POST")
          return await client.PostAsync(url, data);

        if (method == "GET")
          return await client.GetAsync(url);

        throw new ArgumentException("Invalid http method: " + method);
      }
    }

    public void DownloadFileAsync(string url, string netCredentials = null)
    {
      if (url == null)
        throw new ArgumentNullException("Requested url is null");

      using (var client = new HttpClient())
      {
        if (netCredentials != null)
        {
          var credentials = Encoding.ASCII.GetBytes(netCredentials);
          client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
            Convert.ToBase64String(credentials));
        }

        HttpResponseMessage response = client.GetAsync(url).Result;

        using (FileStream fileStream = new FileStream(ConfigurationManager.AppSettings["logFileLocation"], FileMode.Create, FileAccess.Write, FileShare.None))
        {
          response.Content.CopyToAsync(fileStream).Wait();
        }
      }
    }
  }
}
