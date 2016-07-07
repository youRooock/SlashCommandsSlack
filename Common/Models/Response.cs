using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Response
    {
        [JsonProperty("response_type")]
        public string ResponseType;

        [JsonProperty("text")]
        public string Text;

        [JsonProperty("attachments")]
        public List<Attachments> Attachments;
    }
}
