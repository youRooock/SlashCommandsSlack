using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ChannelManager
    {
        private static List<string> _list = new List<string>()
        {
            "selenium"
        };

        public static List<string> GetValidChannels()
        {
            return _list;
        }
    }
}
