using Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CommandManager : ICommandManager
    {
        private readonly Dictionary<string, string> _commands;

        public CommandManager()
        {
            _commands = new Dictionary<string, string>()
            {
                { "help", "Available commands: help"},
            };
        }

        public string Execute(string command)
        {
            string result;
            
            if (!_commands.TryGetValue(command, out result))
                result = "Type 'help' for supported commands";

            var response = new Response
            {
                Text = result
            };

            return JsonConvert.SerializeObject(response);
        }
    }
}
