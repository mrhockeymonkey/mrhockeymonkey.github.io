using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericHost.ASP
{
    public class MyConfiguration
    {
        public string MyJsonKey { get; set; }
        public string MyIniKey { get; set; }
        public string MyXmlKey { get; set; }

    }
}
