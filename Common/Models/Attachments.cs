using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Attachments
    {
        [JsonProperty("text")]
        public string Text;

        [JsonProperty("color")]
        public string Color;

        [JsonProperty("fallback")]
        public string Fallback;

        [JsonProperty("title")]
        public string Title;

        [JsonProperty("title_link")]
        public string TitleLink;

        [JsonProperty("footer")]
        public string Footer;

        [JsonProperty("footer_icon")]
        public string FooterIcon;
    }
}
