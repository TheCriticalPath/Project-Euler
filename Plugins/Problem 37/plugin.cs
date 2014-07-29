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
namespace Problem_37
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public bool IsAsync { get { return false; } }
        public long _limit;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 37; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public async Task<IEulerPluginContext> PerformActionAsync(IEulerPluginContext context)
        {
            throw new NotImplementedException();
        }
        public string Title { get { return string.Format("Truncatable primes", ID); } }
        public string Description
        {
            get
            {
                return @"The number 3797 has an interesting property. Being prime itself, it is possible to continuously remove digits from left to right, and remain prime at each stage: 3797, 797, 97, and 7. Similarly we can work from right to left: 3797, 379, 37, and 3.

Find the sum of the only eleven primes that are both truncatable from left to right and right to left.

NOTE: 2, 3, 5, and 7 are not considered to be truncatable primes.";
            }
        }
        public EulerPlugin() { }

        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "11";

            while (lngLimit < 1)
            {
                Helpers.InputHelper.Show(Name, "Enter Number of primes", ref strLimit);
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
        List<int> TruncatablePrimes = new List<int>();
        public string BruteForce(long limit)
        {
            int sum = 0;
            string l = string.Empty;
            string r = string.Empty;
            bool valid = true; 
            for (int i =11; i < 1000000; i += 2)
            {
                valid = true;
                l = i.ToString();
                r = i.ToString();
                if (MathHelper.IsPrime(int.Parse(l)))
                {
                    do
                    {
                        l = l.Shift(StringHelper.SHIFTDIRECTION.SHIFTLEFT);
                        r = r.Shift(StringHelper.SHIFTDIRECTION.SHIFTRIGHT);
                        if (!MathHelper.IsPrime(int.Parse(l)) || !MathHelper.IsPrime(int.Parse(r)) || l == "1" || r == "1")
                        {
                            valid = false;
                        }

                    } while (l.Length > 1 && valid == true);
                    if (valid)
                    {
                        TruncatablePrimes.Add(i);
                        if (TruncatablePrimes.Count >= limit) { break; }
                    }
                }
            }
            sum = TruncatablePrimes.Sum();
            return string.Format("The sum of the first {2} out of {0} truncatable primes is {1}", limit, sum, TruncatablePrimes.Count);
        }
    }
}
