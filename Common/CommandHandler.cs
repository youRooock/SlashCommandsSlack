
using Common.Attributes;
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

        private static SlashCommand BuildModel(NameValueCollection data)
        {
            var result = new SlashCommand();

            foreach (var field in typeof(SlashCommand).GetFields())
            {
                foreach (var attribute in field.GetCustomAttributes(true))
                {
                    if (attribute.GetType().Name == "UrlParameterAttribute")
                    {
                        var request = attribute as UrlParameterAttribute;

                        if (data[request.Name] != null)
                            field.SetValue(result, data.Get(request.Name));
                    }
                }
            }
            return result;
        }
    }
}
