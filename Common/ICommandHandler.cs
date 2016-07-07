using SlashCommandsService.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface ICommandHandler
    {
        string QueueBuild(NameValueCollection query);
        string GetInfo(NameValueCollection query);
        Task<string> GetDetails(NameValueCollection query);
    }
}
