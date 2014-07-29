using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using Helpers;
namespace Problem_7
{
    [Export(typeof(IEulerPlugin))]
    public class Problem_7 : IEulerPlugin
    {
        public bool IsAsync { get { return false; } }
        public long _limit;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 7; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }

        public string Title { get { return "10001st prime"; } }
        public string Description { get { return "By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, we can see that the 6th prime is 13. What is the 10,001st prime?"; } }

        public Problem_7() { }
        public IEulerPluginContext PerformAction(IEulerPluginContext context)
        {
            context.strResultLongText = GetNthPrime(_limit);
            return context;
        }
        public async Task<IEulerPluginContext> PerformActionAsync(IEulerPluginContext context)
        {
            throw new NotImplementedException();
        }
        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "10001";

            while (lngLimit < 1)
            {
                Helpers.InputHelper.Show(Name, "Enter Upper Limit", ref strLimit);
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

        public string GetNthPrime(long limit)
        {

            int curNumber = 5;
            int PrimeCounter = 3;

            while (PrimeCounter < limit)
            {
                curNumber += 2;
                if (curNumber % 5 != 0)
                {
                    if (MathHelper.IsPrime(curNumber))
                    {
                        PrimeCounter++;
                    }
                }
            }
            return string.Format("The {0}th prime number is {1} ", limit,curNumber);
        }


            }
}
