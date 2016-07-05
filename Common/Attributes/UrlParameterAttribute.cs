using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Attributes
{
    public class UrlParameterAttribute : Attribute
    {
        public string Name { get; private set; }

        public UrlParameterAttribute(string name)
        {
            Name = name;
        }
    }
}
