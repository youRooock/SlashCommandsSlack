using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface ICommandManager
    {
        string GetInfo();
        string QueueBuild(string command);
        Task<string> GetDetails(string command);
    }
}
