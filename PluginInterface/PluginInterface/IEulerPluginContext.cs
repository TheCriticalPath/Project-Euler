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
    }

    public class EulerPluginContext : IEulerPluginContext
    {
        public string strResultLongText { get; set; }
    }

}

