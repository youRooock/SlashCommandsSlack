using Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Connector;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CommandManager : ICommandManager
    {
        private readonly Dictionary<string, string> _commands;
        private readonly IContiniousIntegrationCaller _ciCaller;

        public CommandManager(IContiniousIntegrationCaller ciCaller)
        {
            _ciCaller = ciCaller;
            _commands = new Dictionary<string, string>()
            {
                { "mr", "SocialBank_MailRuTestsProd" },
                { "ok", "SocialBank_OdnoklassnikiTestsProd" },
                { "pp", "PortalBank_SileniumTests" }
            };
        }

        public string QueueBuild(string command)
        {
          string buildId;
          command = command.ToLower();
          _commands.TryGetValue(command, out buildId);

          if (buildId == null)
            return null;

          _ciCaller.QueueBuild(buildId);

          var response = new Response
          {
            ResponseType = "in_channel",
            Text = "Running " + command
          };

          return JsonConvert.SerializeObject(response);
        }

        public string GetInfo()
        {
          var response = new Response
          {
            ResponseType = "ephemeral", // only visible for user . Use "in_channel" if you want this message to be visile for all members
            Text = "Available commands:",
            Attachments = new List<Attachments>
            {
              new Attachments { Text = "/run", Color = "normal"},
              new Attachments { Text = "/details", Color = "normal"}
            }
          };

          return JsonConvert.SerializeObject(response);
        }

        public async Task<string> GetDetails(string command)
        {
          string buildId;
          command = command.ToLower();
          _commands.TryGetValue(command, out buildId);

          if (buildId == null)
            return null;

          var buildInfo = await _ciCaller.GetLastBuildInfo(buildId);

          var response = new Response
          {
            ResponseType = "in_channel",
            Text = "Last build information " + command,
            Attachments = new List<Attachments>
            {
              new Attachments { Text = "Passed: " + buildInfo.Passed, Color = "good"},
              new Attachments { Text = "Failed: " + buildInfo.Failed, Color = "danger"},
              new Attachments { Text = "Errors: " + buildInfo.Errors, Color = "warning"},
            }
          };

          return JsonConvert.SerializeObject(response);
        }

    }
}
