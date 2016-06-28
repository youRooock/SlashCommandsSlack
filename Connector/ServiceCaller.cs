using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Connector
{
    public abstract class ServiceCaller : IServiceCaller
    {
        public async Task<HttpResponseMessage> CallAsync(string url, string method, HttpContent data = null)
        {
            using (var client = new HttpClient())
            {
                if (method == "POST")
                {
                    return await client.PostAsync(url, data);
                }
                else if (method == "GET")
                {
                    return await client.GetAsync(url);
                }
                else
                {
                    throw new ArgumentException("Invalid http method: " + method);
                }
            }
        }
    }
}
