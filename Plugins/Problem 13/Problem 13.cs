using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;
using System.ComponentModel.Composition;

namespace Problem_13
{
    [Export(typeof(IEulerPlugin))]
    public class Problem_13 : IEulerPlugin
    {
        public long _limit;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 13; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Large Sums: {0}", ID); } }
        public string Description { get { return "Work out the first ten digits of the sum of the following one-hundred 50-digit numbers."; } }
        public Problem_13() { }

        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "10";

            while (lngLimit < 1)
            {
                Helpers.InputHelper.Show(Name, "How many significant digits?", ref strLimit);
                if (!Int64.TryParse(strLimit, out lngLimit))
                {
                    lngLimit = 0;
                }

            }
            return lngLimit;
        }
        public void PerformGetInput(IEulerPluginContext context)
        {
            _limit = GetLimit();
        }
        public IEulerPluginContext PerformAction(IEulerPluginContext context)
        {
            throw new NotImplementedException();
            return context;
        }

    }
}
