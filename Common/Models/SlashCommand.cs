using Common.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlashCommandsService.Models
{
    public class SlashCommand
    {
        [UrlParameter("token")]
        public string Token;

        [UrlParameter("team_id")]
        public string TeamId;

        [UrlParameter("team_domain")]
        public string TeamDomain;

        [UrlParameter("channel_id")]
        public string ChannelId;

        [UrlParameter("channel_name")]
        public string ChannelName;

        [UrlParameter("user_id")]
        public string UserId;

        [UrlParameter("user_name")]
        public string UserName;

        [UrlParameter("command")]
        public string Command;

        [UrlParameter("text")]
        public string Text;

        [UrlParameter("response_url")]
        public string ResponseUrl;
    }
}