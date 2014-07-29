using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using Helpers;
namespace Problem_10
{
    [Export(typeof(IEulerPlugin))]
    public class Problem_10 : IEulerPlugin
    {
        public bool IsAsync { get { return false; } }
        public long _limit;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 10; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }

        public string Title { get { return "Summation of Primes"; } }
        public string Description { get { return "The sum of the primes below 10 is 2 + 3 + 5 + 7 = 17.  Find the sum of all the primes below two million."; } }


        public Problem_10() { }
        public IEulerPluginContext PerformAction(IEulerPluginContext context)
        {
            context.strResultLongText = GetSumOfFirstNPrimes(_limit);
            return context;
        }
        public async Task<IEulerPluginContext> PerformActionAsync(IEulerPluginContext context)
        {
            throw new NotImplementedException();
        }
        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "2000000";

            while (lngLimit < 1)
            {
                Helpers.InputHelper.Show(Name, "Enter the limit", ref strLimit);
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
        public string GetSumOfFirstNPrimes(long limit)
        {
            long sum = 10;
            int curNumber = 5;

            while (curNumber < limit - 2)
            {
                curNumber += 2;
                if (curNumber % 5 != 0)
                {
                    if (MathHelper.IsPrime(curNumber))
                    {
                        sum += curNumber;
                    }
                }
            }
            return string.Format("The sum of prime numbers < {0} is {1} ", limit, sum);
        }



    }
}
