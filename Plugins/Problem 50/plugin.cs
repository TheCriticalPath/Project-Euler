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
namespace Problem_50
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public bool IsAsync { get { return true; } }
        public bool ImplementsGetInput { get { return true; } }

        public long _upperLimit;
        public long _lowerLimit;
        public int ID { get { return 50; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Consecutive prime sum", ID); } }
        public string Description
        {
            get
            {
                return @"The prime 41, can be written as the sum of six consecutive primes:

41 = 2 + 3 + 5 + 7 + 11 + 13
This is the longest sum of consecutive primes that adds to a prime below one-hundred.

The longest sum of consecutive primes below one-thousand that adds to a prime, contains 21 terms, and is equal to 953.

Which prime, below one-million, can be written as the sum of the most consecutive primes?";
            }
        }
        public EulerPlugin() { }

        private long GetLimit(string strModifier = "", string defaultLimit = "1000")
        {
            long lngLimit = 0;
            string strLimit = defaultLimit;

            while (lngLimit < 1)
            {
                Helpers.InputHelper.Show(Name, string.Format("Enter {0} Limit", strModifier), ref strLimit);
                if (!Int64.TryParse(strLimit, out lngLimit))
                {
                    lngLimit = 0;
                }

            }
            return lngLimit;
        }
        public void PerformGetInput(IEulerPluginContext context)
        {
           // _lowerLimit = GetLimit("Lower", "286");
            _upperLimit = GetLimit("Upper", "1000000");
        }

        public Task<IEulerPluginContext> PerformActionAsync(IEulerPluginContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                // need a more elegant solution.
                DateTime dtStart, dtEnd;
                dtStart = DateTime.Now;
                Task<String> s = BruteForceAsync();
                dtEnd = DateTime.Now;
                context.strResultLongText = s.Result;
                context.spnDuration = dtEnd.Subtract(dtStart);
                return context;
            });
        }

        public IEulerPluginContext PerformAction(IEulerPluginContext context)
        {
            context.strResultLongText = "";
            return context;
        }
        async Task<string> BruteForceAsync()
        {
            //return Brute2();
            return BruteForce();
        }
        private string BruteForce()
        {
            string retval = string.Empty;
            List<int> primes = GetPrimes();
            int runningTotal = 0;
            int greatestLength = 0;
            int startPosition = 0;
            int total = 0;
            for(int i = 0; i< primes.Count();i++) { 
                //create a running sum starting at i
                runningTotal = 0;
                for (int j = i; j < primes.Count(); j++) {
                    runningTotal += primes[j];
                    if (runningTotal > _upperLimit ){break;}
                    if (runningTotal.IsPrime() && runningTotal< _upperLimit){

                        if (j - i > greatestLength)
                        {
                            startPosition = i;
                            total = runningTotal;
                            greatestLength = j - i;
                        }
                   
                    }
                }
            }

            retval = GetEquation(primes, total, greatestLength);
            //retval = string.Format("{0}", total);
            
                return total.ToString();
        }
        private string GetEquation(List<int> primes,int total, int greatestLength)
        {
            string retval = string.Empty;
            for (int i = 0; i < greatestLength; i++)
            {
                if (i == 0)
                    retval = string.Format("{0} ", primes[i]);
                else if (i < (greatestLength - 1))
                    retval = string.Format("{0} + {1}", retval, primes[i]);
                else
                    retval = string.Format("{3} factors:  {0} + {1} = {2}", retval, primes[i], total, greatestLength);

            }
            return retval;
        }
        private List<int> GetPrimes()
        {
            List<int> retVal = new List<int>();
            retVal.Add(2);
            for(int i = 3; i < _upperLimit;i+=2){
                if(i.IsPrime()) { retVal.Add(i);}   
            }
            return retVal;
        }

    }
}
