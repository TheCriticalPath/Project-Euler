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
namespace Problem_51
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public bool IsAsync { get { return true; } }
        public bool ImplementsGetInput { get { return true; } }

        public long _upperLimit;
        public long _lowerLimit;
        public long _familySize;
        public int ID { get { return 51; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Prime digit replacements", ID); } }
        public string Description
        {
            get
            {
                return @"By replacing the 1st digit of the 2-digit number *3, it turns out that six of the nine possible values: 13, 23, 43, 53, 73, and 83, are all prime.
By replacing the 3rd and 4th digits of 56**3 with the same digit, this 5-digit number is the first example having seven primes among the ten generated numbers, yielding the family: 56003, 56113, 56333, 56443, 56663, 56773, and 56993. Consequently 56003, being the first member of this family, is the smallest prime with this property.
Find the smallest prime which, by replacing part of the number (not necessarily adjacent digits) with the same digit, is part of an eight prime value family.";
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
            _upperLimit = GetLimit("Prime Count", "1000000");
            _familySize = GetLimit("Family Size", "8");

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
            string retval = string.Empty;   // return string
            string position = string.Empty; // index in string of repeated values.
            List<int> primes = new List<int>(); // all primes
            int familyCount = 0, repeatedCount = 0;       // number of times value repeated, value that was repeated.
            primes = Helpers.MathHelper.PrimeSieve((int)_upperLimit);
            int p = 0;
            for (int i = 0; i < primes.Count; i++)
            {
                p = primes[i];
                if (p > 100000)
                {

                    getRepititionCount(p, out repeatedCount, out position);
                    if (repeatedCount == 3)
                    {
                        familyCount = getPrimeFamilyCount(p, primes, position);
                        System.Diagnostics.Debug.WriteLine(string.Format("Family Count: {0} - Prime: {1}", familyCount, p));
                        if (familyCount >= _familySize)
                        {
                            familyCount = getPrimeFamilyCount(p, primes, position);
                            retval = string.Format("Start: {0} - Replace: {1} - Number: {2}", p, position, familyCount);
                            break;
                        }
                    }
                }
            }
            if (retval == string.Empty)
            {
                retval = "no family found:  increase upper limit.";
            }
            return retval;
        }

        private void getRepititionCount(int v, out int repeatedCount, out string position)
        {
            char[] c = v.ToString().ToCharArray();
            repeatedCount = 0;
            char repeatedValue = char.MinValue;
            position = string.Empty;

            for (int i = 0; i < 3; i++)
            {
                repeatedCount = 0;
                repeatedValue = char.Parse(i.ToString());
                position = string.Empty;
                for (int j = 0; j < c.Length - 1; j++)
                {
                    if (c[j] == repeatedValue)
                    {
                        repeatedCount++;
                        position += j.ToString();
                    }
                }
                if (repeatedCount == 3) { return; }
            }

        }

        private List<long> getPrimes(List<long> primes)
        {
            List<long> l = new List<long>();
            int notPrime = 0;
            foreach (long p in primes)
            {
                if (p.IsPrime())
                {
                    l.Add(p);
                }
                else {
                    notPrime++;
                }
                if (notPrime > 2)
                {
                    break;
                }

            }
            return l;
        }

        private int getPrimeFamilyCount(int l, List<int> primes, string v)
        {
            string s = l.ToString();
            string t = string.Empty;
            int count = 0;
            int number = int.MinValue;
            char[] a = v.ToCharArray();
            for (int j = 0; j < 10; j++)
            {
                t = s;
                for (int i = 0; i < a.Length; i++)
                {
                    t = t.ReplaceAt(int.Parse(a[i].ToString()), j.ToString()[0]);
                }
                if (int.TryParse(t, out number))
                {
                    if (number >= l)
                    {
                        if (primes.Contains(number))
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        private void getLocalLimits(long i, out long localLow, out long localHigh)
        {
            long.TryParse("1" + new string('0', (int)i - 2) + "1", out localLow);
            long.TryParse(new string('9', (int)i), out localHigh);
        }

        private void getPositions(long i, out List<string> positions)
        {
            string s;
            s = string.Empty;
            for (long j = 0; j < i - 1; j++)
            {
                s += j.ToString();
            }
            positions = StringHelper.PermuteString(s);
        }
    }
}
