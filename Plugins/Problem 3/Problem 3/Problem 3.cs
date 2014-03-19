using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using Helpers;
namespace Problem_3
{
    [Export(typeof(IEulerPlugin))]
    public class Problem_3 : IEulerPlugin
    {
        public long _limit;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 3; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Largest prime factor", ID); } }
        public string Description { get { return "The prime factors of 13195 are 5, 7, 13 and 29.What is the largest prime factor of the number 600851475143?"; } }

        public Problem_3() { }
        public IEulerPluginContext PerformAction(IEulerPluginContext context) {
            context.strResultLongText = GetLargestPrimeFactor(_limit);
            return context;
        }

        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "600851475143";

            while (lngLimit < 1)
            {
                Helpers.InputBox.Show(Name, "Enter Upper Limit", ref strLimit);
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

        public string GetLargestPrimeFactor(long limit) {
            long[] values = MathHelper.FactorizeFast(limit);
            return string.Format("{0}", values[values.GetUpperBound(0)]);
        }


    }
}
