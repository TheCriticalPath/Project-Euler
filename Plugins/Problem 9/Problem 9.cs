using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;
using System.ComponentModel.Composition;

namespace Problem_9
{
    [Export(typeof(IEulerPlugin))]
    public class Problem_9 : IEulerPlugin
    {
        public long _limit;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 9; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }

        public string Title { get { return "Special Pythagorean triplet"; } }
        public string Description { get { return "A Pythagorean triplet is a set of three natural numbers, a < b < c, for which, a2 + b2 = c2.  For example, 32 + 42 = 9 + 16 = 25 = 52.  There exists exactly one Pythagorean triplet for which a + b + c = 1000. Find the product abc."; } }

        public Problem_9() { }
        public IEulerPluginContext PerformAction(IEulerPluginContext context)
        {
            context.strResultLongText = GetProductOfPythagoreanTriple(_limit);
            return context;
        }

        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "1000";

            while (lngLimit < 1)
            {
                Helpers.InputBox.Show(Name, "Enter target sum for a + b + c", ref strLimit);
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

        public string GetProductOfPythagoreanTriple(long limit)
        {
            int a = 0;
            int b = 0;
            int c = 0;
            int m = 0;
            int n = 0;
            int k = 1;
            int sum  = 0;
            int product = 0;

            //Find the primitive Pythagorean tripes first then then scale them up by a factor of k until the sum exceeds the limit.  If the sum does not meet the condition of exiting, find the next primitive and repeat.
            while (k < limit)
            {
                for (m = 1; m < limit ; m++)
                {
                    for (n = 1; n < m; n++)
                    {
                        a = k * (int)(Math.Pow(m, 2) - Math.Pow(n, 2));
                        b = k * (2 * m * n);
                        c = k * (int)(Math.Pow(m, 2) + Math.Pow(n, 2));
                        sum = a + b + c;
                        if ( sum >= limit){
                            break;
                        }
                    }
                    if (sum >= limit)
                    {
                        break;
                    }
                }
                if (sum == limit)
                {
                    product = a * b * c;
                    break;
                }
                sum = 0;
                k++;
            }

            return string.Format("The product of Pythagorean Triple a={0},b={1},c={2} is {3}", new Object[] { a, b, c, product });
        }


    }
}
