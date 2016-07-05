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
                { "run mr", "Running MailRu tests" },
                { "run ok", "Running Odnoklassniki tests" },
                { "run pp", "Running Portal tests" }
            };
        }

        public string Execute(string command)
        {
            string result;
            command = command.ToLower();

            if (!_commands.TryGetValue(command, out result))
                result = "Type 'help' for supported commands";

            if (command == "help")
            {
                result = "Available commands: " + string.Join(", ", _commands.Keys.ToArray());
            }

            else
            {
              //  _ciCaller.QueueBuild();
            }

            var response = new Response
            {
                Text = result
            };

            return JsonConvert.SerializeObject(response);
        }
    }
}
