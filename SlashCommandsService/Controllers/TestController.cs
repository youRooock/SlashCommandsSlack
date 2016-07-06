using Common;
using Newtonsoft.Json;
using SlashCommandsService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SlashCommandsService.Controllers
{
    public class TestController : ApiController
    {
        private readonly ICommandHandler _commandHandler;

        public TestController(ICommandHandler commandHandler)
        {
            this._commandHandler = commandHandler;
        }

        [HttpPost]
        public HttpResponseMessage Start(HttpRequestMessage message)
        {
          var values = message.Content.ReadAsFormDataAsync().Result;
          var responseBody = _commandHandler.Process(values);
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
