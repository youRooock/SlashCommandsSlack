using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlashCommandsService.Models
{
    public class SlashCommand
    {
        [JsonProperty("token")]
        public string Token;

        [JsonProperty("team_id")]
        public string TeamId;

        [JsonProperty("team_domain")]
        public string TeamDomain;

        [JsonProperty("channel_id")]
        public string ChannelId;

        [JsonProperty("channel_name")]
        public string ChannelName;

        [JsonProperty("user_id")]
        public string UserId;

        [JsonProperty("user_name")]
        public string UserName;

        [JsonProperty("command")]
        public string Command;

        [JsonProperty("text")]
        public string Text;

        [JsonProperty("response_url")]
        public string ResponseUrl;
    }
}