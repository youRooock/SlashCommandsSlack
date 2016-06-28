using Newtonsoft.Json;
using SlashCommandsService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CommandHandler : ICommandHandler
    {
        private readonly ICommandManager _commandManager;

        public CommandHandler(ICommandManager commandManager)
        {
            this._commandManager = commandManager;
        }

        public string Process(SlashCommand command)
        {
            var allowedChannels = ChannelManager.GetValidChannels();

            if (command.Token != ConfigurationManager.AppSettings["token"])
                return null;
            if (!allowedChannels.Contains(command.ChannelName))
                return null;

            return _commandManager.Execute(command.Text);
        }
    }
}
