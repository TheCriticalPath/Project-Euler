using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
//using System.ComponentModel.Composition.AttributedModel;
namespace UI
{
    static class Program
    {
        [ImportMany]
        public static IEnumerable<PluginInterface.IEulerPlugin> EulerPlugins { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MEFPluginLoader loader = new MEFPluginLoader("Plugins");
            EulerPlugins = loader.Plugins;
            
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
