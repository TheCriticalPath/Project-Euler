using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using Helpers;
namespace Problem_19
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public long _limit;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 19; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Counting Sundays", ID); } }
        public string Description { get { return "How many Sundays fell on the first of the month during the twentieth century (1 Jan 1901 to 31 Dec 2000)?"; } }
        public EulerPlugin() { }

        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "20";

            while (lngLimit < 1)
            {
                Helpers.InputHelper.Show(Name, "Enter Century", ref strLimit);
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
            context.strResultLongText = CountingSundays((int)_limit);
            return context;
        }

        public string CountingSundays(int Century)
        {
            int Cent = Century * 100;
            int sum = 0;
            DateTime dt;
            for (int i = Cent; i > (Cent) - 100; i--)
            {
                for (int m = 1; m <= 12; m++)
                {
                    dt = new DateTime(i, m, 1);
                    if (dt.DayOfWeek == DayOfWeek.Sunday) {
                        sum++;
                        System.Diagnostics.Debug.WriteLine(dt.ToString());
                    }
                }
            }
            return string.Format("There are {0} Sundays on the first of a month in the {1} Century.", sum, Century);
        }

    }
}
