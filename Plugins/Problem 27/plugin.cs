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
namespace Problem_27
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public long _limit;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 27; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Quadratic Primes", ID); } }
        public string Description
        {
            get
            {
                return @"Euler discovered the remarkable quadratic formula:

n² + n + 41

It turns out that the formula will produce 40 primes for the consecutive values n = 0 to 39. However, when n = 40, 402 + 40 + 41 = 40(40 + 1) + 41 is divisible by 41, and certainly when n = 41, 41² + 41 + 41 is clearly divisible by 41.

The incredible formula  n² − 79n + 1601 was discovered, which produces 80 primes for the consecutive values n = 0 to 79. The product of the coefficients, −79 and 1601, is −126479.

Considering quadratics of the form:

n² + an + b, where |a| < 1000 and |b| < 1000

where |n| is the modulus/absolute value of n
e.g. |11| = 11 and |−4| = 4
Find the product of the coefficients, a and b, for the quadratic expression that produces the maximum number of primes for consecutive values of n, starting with n = 0.";
            }
        }
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
        public IEulerPluginContext PerformAction(IEulerPluginContext context)
        {
            context.strResultLongText = BruteForce(_limit);
            return context;
        }
        private string BruteForce(long limit)
        {
            long a = 0;
            long b = 0;
            long n = 0;
            long a1 = 0, b1 = 0, n1 = 0;
            double answer = 0d;
            for (a = (-1L * limit); a < limit; a++)
            {
                for (b = (-1L * limit); b < limit; b++)
                {
                    if (MathHelper.IsPrime(b))
                    {
                        n = -1;
                        do
                        {
                            n++;
                            answer = Math.Pow(n, 2) + (a * n) + b;
                        } while (MathHelper.IsPrime((long)answer));
                        if (n > n1)
                        {
                            n1 = n; a1 = a; b1 = b;
                        }
                    }
                }
            }

            return string.Format("The product of the coefficents {0} and {1} is {2}. There are {3} consecutive values", new object[] { a1, b1, a1*b1, n1 });
        }
    }
}
