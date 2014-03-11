using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginInterface
{
    public interface IEulerPlugin
    {
        string Name { get; }
        string Description { get; }
        bool ImplementsGetInput { get; }
        IEulerPluginContext PerformAction(IEulerPluginContext context);
        void PerformGetInput(IEulerPluginContext context);
    }
}
