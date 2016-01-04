using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginInterface
{
    public interface IEulerPluginContext
    {
        string strResultLongText { get; set; }
        TimeSpan spnDuration { get; set; }
        Dictionary<string, string> dctParameters { get; set; }
    }

    public class EulerPluginContext : IEulerPluginContext
    {
        public string strResultLongText { get; set; }
        public TimeSpan spnDuration { get; set; }
        public Dictionary<string, string> dctParameters {get; set;}
    }

}

