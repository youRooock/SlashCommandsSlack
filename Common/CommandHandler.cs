
using SlashCommandsService.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        public string Process(NameValueCollection query)
        {
            var allowedChannels = ChannelManager.GetValidChannels();
            var slashCommand = BuildModel(query);
            if (slashCommand.Token != ConfigurationManager.AppSettings["token"])
                return null;
            if (!allowedChannels.Contains(slashCommand.ChannelName))
                return null;

            return _commandManager.Execute(slashCommand.Text);
        }

        private SlashCommand BuildModel(NameValueCollection query)
        {
            var command = new SlashCommand
            {
                Token = query["token"],
                TeamId = query["team_id"],
                ChannelId = query["channel_id"],
                ChannelName = query["channel_name"],
                Command = query["command"],
                TeamDomain = query["team_domain"],
                UserId = query["user_id"],
                UserName = query["user_name"],
                Text = query["text"],
                ResponseUrl = query["response_url"]                
            };

            return command;
        }
    }
}
