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
namespace Problem_35
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public bool IsAsync { get { return false; } }
        public long _limit;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 35; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public async Task<IEulerPluginContext> PerformActionAsync(IEulerPluginContext context)
        {
            throw new NotImplementedException();
        }
        public string Title { get { return string.Format("Circular Primes", ID); } }
        public string Description
        {
            get
            {
                return @"The number, 197, is called a circular prime because all rotations of the digits: 197, 971, and 719, are themselves prime.

There are thirteen such primes below 100: 2, 3, 5, 7, 11, 13, 17, 31, 37, 71, 73, 79, and 97.

How many circular primes are there below one million?";
            }
        }
        public EulerPlugin() { }

        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "1000000";

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
        List<int> Primes = new List<int>();
        public string BruteForce(long limit) { 
            int count = 0; //There are thirteen such primes below 100: 2, 3, 5, 7, 11, 13, 17, 31, 37, 71, 73, 79, and 97.
            Primes.Add(1); count++;
            Primes.Add(2); count++; Primes.Add(3); count++; Primes.Add(5); count++;
            for (int i = 7; i < limit;i+=2){
                if (!Primes.Contains(i)) {
                    if (MathHelper.IsPrime(i))
                    {
                        if (i.ToString().IndexOfAny(new char[] { '0', '2', '4', '5', '6', '8', '0' }) < 0)
                        {
                            Primes.Add(i);
                        }
                    }
                }   
            }
            foreach (int p in Primes) {
                bool all = true;
                if (p > 10)
                {
                    string s = p.ToString().Shift(StringHelper.SHIFTDIRECTION.SHIFTLEFT_CIRCULAR);
                    do
                    {
                        if (!Primes.Contains(int.Parse(s)))
                        {
                            all = false;
                            break;
                        }
                        s = s.Shift(StringHelper.SHIFTDIRECTION.SHIFTLEFT_CIRCULAR);
                    } while (s != p.ToString());
                    if (all)
                    {
                        count++;
                    }
                }
            }



            return string.Format("There are {0} circular primes less than {1}", count,limit);
        }
        private int RotateNumber(int i) {
            int r = i;
            string s = i.ToString();
            return int.Parse(s.Shift(StringHelper.SHIFTDIRECTION.SHIFTLEFT_CIRCULAR));
        }

    }
}
