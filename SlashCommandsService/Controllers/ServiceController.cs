using Common;
using Newtonsoft.Json;
using SlashCommandsService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SlashCommandsService.Controllers
{
    [RoutePrefix("service")]
    public class ServiceController : ApiController
    {
        private readonly ICommandHandler _commandHandler;

        public ServiceController(ICommandHandler commandHandler)
        {
            this._commandHandler = commandHandler;
        }

        [HttpPost]
        [Route("queue")]
        public HttpResponseMessage QueueBuild(HttpRequestMessage message)
        {
          var values = message.Content.ReadAsFormDataAsync().Result;
          var responseBody = _commandHandler.QueueBuild(values);
          HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

          if (responseBody != null)
          {
            response.Content = new StringContent(responseBody);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
          }

          return response;
        }

        [HttpPost]
        [Route("details")]
        public async Task<HttpResponseMessage> GetDetails(HttpRequestMessage message)
        {
          var values = message.Content.ReadAsFormDataAsync().Result;
          var responseBody = await _commandHandler.GetDetails(values);
          HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

          if (responseBody != null)
          {
            response.Content = new StringContent(responseBody);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
          }

          return response;
        }

        [HttpPost]
        [Route("info")]
        public HttpResponseMessage GetInfo(HttpRequestMessage message)
        {
          var values = message.Content.ReadAsFormDataAsync().Result;
          var responseBody = _commandHandler.GetInfo(values);
          HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

          if (responseBody != null)
          {
            response.Content = new StringContent(responseBody);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
          }

          return response;
        }
    }
}
