
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

        public string QueueBuild(NameValueCollection query)
        {
          var slashCommand = BuildModel(query);

          if (!IsCommandValid(slashCommand))
            return null;

          return _commandManager.QueueBuild(slashCommand.Text);
        }

        public string GetInfo(NameValueCollection query)
        {
          var slashCommand = BuildModel(query);

          if (!IsCommandValid(slashCommand))
            return null;

          return _commandManager.GetInfo();
        }

        public async Task<string> GetDetails(NameValueCollection query)
        {
          var slashCommand = BuildModel(query);

          if (!IsCommandValid(slashCommand))
            return null;

          return await _commandManager.GetDetails(slashCommand.Text);
        }

      private bool IsCommandValid(SlashCommand slashCommand)
      {
        var allowedChannels = ChannelManager.GetValidChannels();

        if (slashCommand.Token != ConfigurationManager.AppSettings["token1"] && slashCommand.Token != ConfigurationManager.AppSettings["token2"] && slashCommand.Token != ConfigurationManager.AppSettings["token3"])
          return false;
        if (!allowedChannels.Contains(slashCommand.ChannelId))
          return false;

        return true;
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
