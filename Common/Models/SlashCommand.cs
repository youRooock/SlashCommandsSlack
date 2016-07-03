using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlashCommandsService.Models
{
    public class SlashCommand
    {
        public string Token;

        public string TeamId;

        public string TeamDomain;

        public string ChannelId;

        public string ChannelName;

        public string UserId;

        public string UserName;

        public string Command;

        public string Text;

        public string ResponseUrl;
    }
}