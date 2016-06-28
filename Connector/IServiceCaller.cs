using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Connector
{
    public interface IServiceCaller
    {
        Task<HttpResponseMessage> CallAsync(string uri, string method, HttpContent data);
    }
}
