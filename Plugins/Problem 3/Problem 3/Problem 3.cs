using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;
using System.ComponentModel.Composition;

namespace Problem_3
{
    [Export(typeof(IEulerPlugin))]
    public class Problem_3 : IEulerPlugin
    {
        public long _limit;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 3; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Largest prime factor", ID); } }
        public string Description { get { return "The prime factors of 13195 are 5, 7, 13 and 29.What is the largest prime factor of the number 600851475143?"; } }

        public Problem_3() { }
        public IEulerPluginContext PerformAction(IEulerPluginContext context) {
            context.strResultLongText = GetLargestPrimeFactor(_limit);
            return context;
        }

        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "600851475143";

            while (lngLimit < 1)
            {
                Helpers.InputBox.Show(Name, "Enter Upper Limit", ref strLimit);
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

        public string GetLargestPrimeFactor(long limit) {
            long[] values = FactorizeFast(limit);
            return string.Format("{0}", values[values.GetUpperBound(0)]);
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

        public static Int64[] FactorizeFast(Int64 Num)
        {
            #region vars
            // list of Factors
            List<Int64> _arrFactors = new List<Int64>();
            // temp variable
            Int64 _num = Num;
            #endregion
            #region Check if the number is Prime (<100)
            for (int k = 0; k < Primes.Length; k++)
            {
                if (_num == Primes[k])
                {
                    _arrFactors.Add(Primes[k]);
                    return _arrFactors.ToArray();
                }
            }
            #endregion
            #region Try to factorize using Primes Array
            for (int k = 0; k < Primes.Length; k++)
            {
                int m = Primes[k];
                if (_num < m) break;
                while (_num % m == 0)
                {
                    _arrFactors.Add(m);
                    _num = (Int64)_num / m;
                }
            }
            if (_num < _startNum)
            {
                _arrFactors.Sort();
                return _arrFactors.ToArray();
            }
            #endregion
            #region Main Factorization Algorithm
            Int64 _upMargin = (Int64)Math.Sqrt(_num) + 1;
            Int64 i = _startNum;
            while (i <= _upMargin)
            {
                if (_num % i == 0)
                {
                    _arrFactors.Add(i);
                    _num = _num / i;
                    _upMargin = (Int64)Math.Sqrt(_num) + 1;
                    i = _startNum;
                }
                else { i++; i++; }
            }
            _arrFactors.Add(_num);
            _arrFactors.Sort();
            return _arrFactors.ToArray();
            #endregion
        }
    }
}
