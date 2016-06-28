using Common;
using SlashCommandsService.Models;
using System;
using System.Collections.Generic;
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
        public HttpResponseMessage Start([FromUri] SlashCommand command)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            response.Content = new StringContent(_commandHandler.Process(command));
            response.StatusCode = HttpStatusCode.OK;

            return response;
        }
    }
}
