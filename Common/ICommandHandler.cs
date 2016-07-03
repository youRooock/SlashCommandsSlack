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
        string Process(NameValueCollection query);
    }
}
