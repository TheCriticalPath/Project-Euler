using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using Helpers;
using System.Numerics;
using Algorithms;
namespace Problem_26
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public long _limit;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 26; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Reciprocal cycles", ID); } }
        public string Description { get { return "Find the value of d < 1000 for which 1/d contains the longest recurring cycle in its decimal fraction part."; } }
        public EulerPlugin() { }

        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "1000";

            while (lngLimit < 1)
            {
                Helpers.InputHelper.Show(Name, "Enter Limit", ref strLimit);
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

        BigInteger MaxInteger = new BigInteger();
        public IEulerPluginContext PerformAction(IEulerPluginContext context)
        {
            MaxInteger = BigInteger.Pow(10, 2000);
            context.strResultLongText = GetLongestCycle((int)_limit);
            return context;
        }

        public string GetLongestCycle(int limit)
        {
            int denominator = 0;
            int sequenceLength = 0;
            for (int d = limit; d > 1; d--)
            {
                if (sequenceLength >= d)
                {
                    break;
                }
                int[] foundRemainders = new int[d];
                int value = 1;
                int position = 0;

                while (foundRemainders[value] == 0 && value != 0)
                {
                    foundRemainders[value] = position;
                    value *= 10;
                    value %= d;
                    position++;
                }
                if (position - foundRemainders[value] > sequenceLength)
                {
                    sequenceLength = position - foundRemainders[value];
                    denominator = d;
                }
            }

            return string.Format("The denominator, < {0}, with the longest reciprocal cycle of {1} is {2}", limit, sequenceLength, denominator);
        }
    }
}
