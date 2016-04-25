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
namespace Problem_58
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public bool IsAsync { get { return true; } }
        public bool ImplementsGetInput { get { return true; } }

        public long _upperLimit;
        public long _lowerLimit;

        //TODO: Replace the project number, Title, and Description
        public int ID { get { return 58; } }
        public string Title { get { return "Spiral primes"; } }
        public string Description
        {
            get
            {
                return @"Starting with 1 and spiralling anticlockwise in the following way, a square spiral with side length 7 is formed.

37 36 35 34 33 32 31
38 17 16 15 14 13 30
39 18  5  4  3 12 29
40 19  6  1  2 11 28
41 20  7  8  9 10 27
42 21 22 23 24 25 26
43 44 45 46 47 48 49

It is interesting to note that the odd squares lie along the bottom right diagonal, but what is more interesting is that 8 out of the 13 numbers lying along both diagonals are prime; that is, a ratio of 8/13 ≈ 62%.

If one complete new layer is wrapped around the spiral above, a square spiral with side length 9 will be formed. If this process is continued, what is the side length of the square spiral for which the ratio of primes along both diagonals first falls below 10%?";
            }
        }

        public string Name
        {
            get { return $"Problem {ID}: {Title}"; }
        }
        public EulerPlugin() { }

        private long GetLimit(string strModifier = "", string defaultLimit = "1000")
        {
            long lngLimit = 0;
            string strLimit = defaultLimit;

            while (lngLimit < 1)
            {
                Helpers.InputHelper.Show(Name, $"Enter {strModifier} Limit", ref strLimit);
                if (!Int64.TryParse(strLimit, out lngLimit))
                {
                    lngLimit = 0;
                }

            }
            return lngLimit;
        }

        //TODO: Modify for the values in this routine to meet the needs of the specific problem
        public void PerformGetInput(IEulerPluginContext context)
        {
            _upperLimit = GetLimit("Ratio", "10");
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
            return BruteForce();
        }

        private string BruteForce()
        {
            //List<int> listPrime = MathHelper.PrimeSieve(10000000);
            string retval = string.Empty;   // return string
            //value = previ + 2 * (i - 1) - formula for numbers on the / diagonal
            //value = 2*(i - (i % 2)) + i - formula for numbers on \ diagonal
            //decimal ratio = 1.0M;
            int primes = 3; //3,5,7
            int sideLength = 2;
            int c = 9;
            double ratio = (double)primes / (2 * sideLength + 1);
            while (ratio > (double)_upperLimit / 100)
            {
                sideLength += 2;
                for (int i =0; i < 3; i++)
                {
                    c += sideLength;
                    if (MathHelper.IsPrime(c)) primes++;

                }
                c += sideLength;
                ratio = (double)primes / (2 * sideLength + 1);
            }
            return $"Ratio is {ratio:N2} and side length is {sideLength+1}";
        }

    }
}
