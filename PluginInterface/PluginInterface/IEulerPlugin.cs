using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginInterface
{
    public interface IEulerPlugin
    {
        bool IsAsync { get; }
        int ID { get; }
        string Title { get; }
        string Name { get; }
        string Description { get; }
        bool ImplementsGetInput { get; }

        IEulerPluginContext PerformAction(IEulerPluginContext context);
        Task<IEulerPluginContext> PerformActionAsync(IEulerPluginContext context);
        void PerformGetInput(IEulerPluginContext context);
    }
}
