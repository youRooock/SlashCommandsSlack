using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connector
{
    public interface IContiniousIntegrationCaller
    {
        void QueueBuild(string buildId);
    }
}
