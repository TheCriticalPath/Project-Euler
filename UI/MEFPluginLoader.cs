using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using PluginInterface;
namespace UI
{
    class MEFPluginLoader
    {
        [ImportMany]
        public IEnumerable<IEulerPlugin> Plugins
        {
            get;
            set;
        }
        CompositionContainer _Container;
        public MEFPluginLoader(string path) {
            DirectoryCatalog directoryCatalog = new DirectoryCatalog(path);

            var catalog = new AggregateCatalog(directoryCatalog);

            _Container = new CompositionContainer(catalog);

            _Container.ComposeParts(this);
        }
    }
}
