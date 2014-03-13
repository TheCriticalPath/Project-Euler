using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;
using System.ComponentModel.Composition;

namespace Problem_10
{
    [Export(typeof(IEulerPlugin))]
    public class Problem_10 : IEulerPlugin
    {
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

        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "2000000";

            while (lngLimit < 1)
            {
                Helpers.InputBox.Show(Name, "Enter the limit", ref strLimit);
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
                    if (IsPrime(curNumber))
                    {
                        sum += curNumber;
                    }
                }
            }
            return string.Format("The sum of prime numbers < {0} is {1} ", limit, sum);
        }


        private static readonly int[] Primes =
        new int[] { 2, 3, 5, 7, 11, 13, 17, 19, 23,
                    29, 31, 37, 41, 43, 47, 53, 59,
                    61, 67, 71, 73, 79, 83, 89, 97 };
        // starting number for iterative factorization
        private const int _startNum = 101;
        /// <summary>
        /// Check if the number is Prime
        /// </summary>
        /// <param name="Num">Int64</param>
        /// <returns>bool</returns>
        public static bool IsPrime(Int64 Num)
        {
            int j;
            bool ret;
            Int64 _upMargin = (Int64)Math.Sqrt(Num) + 1; ;
            // Check if number is in Prime Array
            for (int i = 0; i < Primes.Length; i++)
            {
                if (Num == Primes[i]) { return true; }
            }
            // Check divisibility w/Prime Array
            for (int i = 0; i < Primes.Length; i++)
            {
                if (Num % Primes[i] == 0) return false;
            }
            // Main iteration for Primality check
            _upMargin = (Int64)Math.Sqrt(Num) + 1;
            j = _startNum;
            ret = true;
            while (j <= _upMargin)
            {
                if (Num % j == 0) { ret = false; break; }
                else { j++; j++; }
            }
            return ret;
        }
        /// <summary>
        /// Check if number-string is Prime
        /// </summary>
        /// <param name="Num">string</param>
        /// <returns>bool</returns>
        public static bool IsPrime(string StringNum)
        {
            return IsPrime(Int64.Parse(StringNum));
        }


    }
}
