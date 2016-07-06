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
                { "run mr", "SocialBank_MailRuTestsProd" },
                { "run ok", "SocialBank_OdnoklassnikiTestsProd" },
                { "run pp", "PortalBank_SileniumTests" }
            };
        }

        public string Execute(string command)
        {
            string result;
            string responseType = "ephemeral";
            command = command.ToLower();

            if (!_commands.TryGetValue(command, out result) && command != "help")
                result = "Type 'help' for supported commands";

            else if (command == "help")
                result = "Available commands: " + string.Join(", ", _commands.Keys.ToArray());

            else
            {
              string buildId;
              result = "Running...";
              responseType = "in_channel";

              _commands.TryGetValue(command, out buildId);
              _ciCaller.QueueBuild(buildId);
            }

            var response = new Response
            {
                ResponseType = responseType,
                Text = result
            };

            return JsonConvert.SerializeObject(response);
        }
    }
}
