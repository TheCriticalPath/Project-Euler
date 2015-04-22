using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
namespace Helpers
{
    public static class MathHelper
    {
        public struct QuadraticResult
        {
            public double n1, n2;
        }
        public static int SumDigits(this string s)
        {
            int v = 0;
            for (int i = 0; i < s.Length; i++)
            {
                v += int.Parse(s[i].ToString());
            }
            return v;
        }

        public static long SumDigits(this long[] l)
        {
            long v = 0;
            for (int i = 0; i < l.Length; i++)
            {
                v += l[i];
            }
            return v;
        }
        public static int GCD(int a, int b)
        {
            int Remainder;

            while (b != 0)
            {
                Remainder = a % b;
                a = b;
                b = Remainder;
            }

            return a;
        }

        public static bool IsPrime(this double num)
        {
            return _IsPrime((Int64)num);
        }
        public static bool IsPrime(this Int64 num)
        {
            return _IsPrime(num);
        }
        public static bool IsPrime(this int num)
        {
            return _IsPrime((Int64)num);
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
        public static bool _IsPrime(Int64 Num)
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

        private static String[] NumberWords = { "zero"
                                              , "one"
                                              , "two"
                                              , "three"
                                              , "four"
                                              , "five"
                                              , "six"
                                              , "seven"
                                              , "eight"
                                              , "nine"
                                              , "ten"
                                              , "eleven"
                                              , "twelve"
                                              , "thirteen"
                                              , "fourteen"
                                              , "fifteen"
                                              , "sixteen"
                                              , "seventeen"
                                              , "eighteen"
                                              , "nineteen"
                                              , "twenty"
                                              , "thirty"
                                              , "forty"
                                              , "fifty"
                                              , "sixty"
                                              , "seventy"
                                              , "eighty"
                                              , "ninety"};
        private const string AND = "and";
        private const string HUNDRED = "hundred";
        private const string THOUSAND = "thousand";
        public static string ConvertToWord(this int i)
        {
            string retVal = string.Empty;
            string s = i.ToString();
            int length = i.ToString().Length;
            int mod = 0;
            int floor = 0;
            if (i == 0)
            {
                retVal = "";
            }
            else if (i <= 20)
            {
                retVal = NumberWords[i];
            }
            else if (i < 100)
            {
                floor = (int)Math.Floor(i / 10D);
                mod = i % (floor * 10);
                retVal = NumberWords[18 + floor];
                if (mod != 0)
                {
                    retVal += "-" + NumberWords[mod];
                };
            }
            else if (i < 1000)
            {
                floor = (int)Math.Floor(i / 100D);
                mod = i % (floor * 100);

                retVal += NumberWords[floor] + " " + HUNDRED;
                if (mod != 0)
                {
                    retVal += " " + AND + " " + mod.ConvertToWord(); ;
                };
            }
            else if (i == 1000)
            {
                retVal = "One Thousand";
            }

            return retVal;
        }

        public static BigInteger ToFactorial(this BigInteger x)
        {
            return Factorial(x);
        }
        public static BigInteger Factorial(BigInteger x)
        {
            BigInteger retVal = 1;
            for (BigInteger i = 1; i <= x; i++)
            {
                retVal *= i;
            }
            return retVal;

        }
        public static int ToInt(this string s)
        {
            return int.Parse(s);
        }
        public static BigInteger Factorial(long x)
        {
            BigInteger retVal = 1;
            for (BigInteger i = 1; i <= x; i++)
            {
                retVal *= i;
            }
            return retVal;

        }
        public static double Factorial(int x)
        {
            double retVal = 1D;
            for (int i = 1; i <= x; i++)
            {
                retVal *= i;
            }
            return retVal;
        }
        public static int CountFactors(long triangleNumber)
        {
            int count = 0;
            int end = (int)Math.Sqrt(triangleNumber);
            for (int i = 1; i < end; i++)
            {
                if (triangleNumber % i == 0)
                {
                    count += 2;
                }
            }
            if (end * end == triangleNumber) { count++; }
            return count;
        }
        public static BigInteger SumOfFirstNNaturalNumbers(BigInteger n)
        {
            return (n * (n + 1)) / 2;
        }

        public static int SumOfFactors(int number)
        {
            int sqrtOfNumber = (int)Math.Sqrt(number);
            int sum = 1;

            //If the number is a perfect square
            //Count the squareroot once in the sum of factors
            if (number == sqrtOfNumber * sqrtOfNumber)
            {
                sum += sqrtOfNumber;
                sqrtOfNumber--;
            }

            for (int i = 2; i <= sqrtOfNumber; i++)
            {
                if (number % i == 0)
                {
                    sum = sum + i + (number / i);
                }
            }

            return sum;
        }
        public static long[] GetDivisors(long number)
        {
            List<long> divisors = new List<long>();
            if (number > 1)
            {
                int end = (int)Math.Ceiling(Math.Sqrt(number));
                for (int i = 2; i < end; i++)
                {
                    if (number % i == 0)
                    {
                        divisors.Add(i);
                        divisors.Add(number / i);
                    }
                }
                if (end * end == number && number > 1)
                {
                    divisors.Add(end);
                }
                divisors.Add(1);
            }
            System.Diagnostics.Debug.WriteLine(number + ": " + divisors.Count() + " = " + divisors.ToArray<long>().SumDigits());
            return divisors.ToArray<long>();
        }
        public static void SwapIndexes(this List<string> List, int pos1, int pos2)
        {
            string s = List[pos1];
            List[pos1] = List[pos2];
            List[pos2] = s;
        }


        public static QuadraticResult GetQuadraticResult(Int64 a, Int64 b, Int64 c, Int64 x = 1)
        {
            QuadraticResult qr = new QuadraticResult();
            qr.n1 = (-b + Math.Sqrt((b * b) - 4 * a * c * x)) / (2 * a);
            qr.n2 = (-b - Math.Sqrt((b * b) - 4 * a * c * x)) / (2 * a);
            return qr;
        }
        public static bool IsTriangle(this Int64 num)
        {
            bool Is = false;
            QuadraticResult qr = GetQuadraticResult(1, 1, -2, num);
            if (qr.n1 > 0)
            {
                Is = qr.n1 == (Int64)qr.n1;
            }
            if (!Is && qr.n2 > 0)
            {
                Is = qr.n2 == (Int64)qr.n2;
            }
            return Is;

        }
        public static bool IsPentagonal(this Int64 num)
        {
            bool Is = false;
            QuadraticResult qr = GetQuadraticResult(3, -1, -2, num);
            if (qr.n1 > 0) { 
                Is = qr.n1 == (Int64)qr.n1;
            }
            if(!Is && qr.n2 > 0){
                Is = qr.n2 == (Int64)qr.n2;
            }
            return Is;
        }
        public static bool IsHexagonal(this Int64 num)
        {
            bool Is = false;
            QuadraticResult qr = GetQuadraticResult(2, -1, -1, num);
            if (qr.n1 > 0)
            {
                Is = qr.n1 == (Int64)qr.n1;
            }
            if (!Is && qr.n2 > 0)
            {
                Is = qr.n2 == (Int64)qr.n2;
            }
            return Is;
        }
        public static double GetTriangleNumber(this Int64 num)
        {
            return ((num * num) + num) / 2;
        }
        public static double GetPentagonalNumber(this Int64 num)
        {
            return ((3 * (num * num)) - num) / 2;
        }
        public static double GetHexagonalNumber(this Int64 num)
        {
            return (2 * (num * num)) - num;
        }

        public static bool HasSubStringDivisibility(this Int64 num)
        {
            string strNum = num.ToString();
            string subStr = "";
            int intSub = 0;

            for (int i = 1; i <= 7; i++)
            {
                subStr = strNum.Substring(i, 3);
                intSub = int.Parse(subStr);
                switch (i)
                {
                    case 1:
                        if (intSub % 2 != 0) return false;
                        break;
                    case 2:
                        if (intSub % 3 != 0) return false;
                        break;
                    case 3:
                        if (intSub % 5 != 0) return false;
                        break;
                    case 4:
                        if (intSub % 7 != 0) return false;
                        break;
                    case 5:
                        if (intSub % 11 != 0) return false;
                        break;
                    case 6:
                        if (intSub % 13 != 0) return false;
                        break;
                    case 7:
                        if (intSub % 17 != 0) return false;
                        break;
                }
            }
            return true;
        }

        public static int FindCeil(List<string> str, int first, int l, int h)
        {
            // initialize index of ceiling element
            int ceilIndex = l;

            // Now iterate through rest of the elements and find
            // the smallest character greater than 'first'
            for (int i = l + 1; i <= h; i++)
                if (str[i].ToInt() > first && str[i].ToInt() < str[ceilIndex].ToInt())
                    ceilIndex = i;

            return ceilIndex;
        }



    }
}
