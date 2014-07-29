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
namespace Problem_28
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public bool IsAsync { get { return false; } }
        public long _limit;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 28; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Number spiral diagonals", ID); } }
        public string Description
        {
            get
            {
                return @"Starting with the number 1 and moving to the right in a clockwise direction a 5 by 5 spiral is formed as follows:

21 22 23 24 25
20  7  8  9 10
19  6  1  2 11
18  5  4  3 12
17 16 15 14 13

It can be verified that the sum of the numbers on the diagonals is 101.

What is the sum of the numbers on the diagonals in a 1001 by 1001 spiral formed in the same way?";
            }
        }
        public EulerPlugin() { }

        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "1001";

            while (lngLimit < 1 || lngLimit % 2 == 0)
            {
                Helpers.InputHelper.Show(Name, "Enter Square's length.  Must be odd.", ref strLimit);
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
            context.strResultLongText = BruteForce(_limit);
            return context;
        }
        public async Task<IEulerPluginContext> PerformActionAsync(IEulerPluginContext context)
        {
            throw new NotImplementedException();
        }
        public string BruteForce(long limit)
        {
            BigInteger ans = new BigInteger();
            BigInteger area = BigInteger.Pow(limit, 2);
            BigInteger sum = 0;

            //NorthEast
            for (int i = 1; i <= limit; i++)
            {
                //NorthWest
                ans = 4 * BigInteger.Pow(i, 2) - 6 * i + 3;
                if (ans <= area)
                    sum += ans;
                System.Diagnostics.Debug.WriteLine(ans);

                ans = 4 * BigInteger.Pow(i, 2) - 10 * i + 7;
                if (ans <= area)
                    sum += ans;
                System.Diagnostics.Debug.WriteLine(ans);

                ans = 4 * BigInteger.Pow(i, 2) + 1;
                if (ans <= area)
                    sum += ans;
                System.Diagnostics.Debug.WriteLine(ans);

                ans = BigInteger.Pow((2 * i + 1), 2);
                if (ans <= area)
                    sum += ans;
                System.Diagnostics.Debug.WriteLine(ans);

            }
            //too many ones
            sum--;

            return string.Format("A number spiral with side length {0} has a diagonal sum of {1}", limit, sum);
        }

    }
}
