using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericHost.Console
{
    interface ISomeDependency
    {
        int GetSomeValue();
    }

    class SomeDependency : ISomeDependency
    {
        public int GetSomeValue() => new Random().Next(0, 11);
    }
}
