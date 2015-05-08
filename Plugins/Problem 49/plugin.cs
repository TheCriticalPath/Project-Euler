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
namespace Problem_49
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public struct Pair{ public int a; public int b;} 
        public bool IsAsync { get { return true; } }
        public bool ImplementsGetInput { get { return true; } }

        public long _upperLimit;
        public long _lowerLimit;
        public int ID { get { return 49; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Prime permutations", ID); } }
        public string Description
        {
            get
            {
                return @"The arithmetic sequence, 1487, 4817, 8147, in which each of the terms increases by 3330, is unusual in two ways: (i) each of the three terms are prime, and, (ii) each of the 4-digit numbers are permutations of one another.

There are no arithmetic sequences made up of three 1-, 2-, or 3-digit primes, exhibiting this property, but there is one other 4-digit increasing sequence.

What 12-digit number do you form by concatenating the three terms in this sequence?";
            }
        }
        public EulerPlugin() { }

        private long GetLimit(string strModifier = "", string defaultLimit = "1000")
        {
            long lngLimit = 0;
            string strLimit = defaultLimit;

            while (lngLimit < 1)
            {
                Helpers.InputHelper.Show(Name, string.Format("Enter {0} prine length", strModifier), ref strLimit);
                if (!Int64.TryParse(strLimit, out lngLimit))
                {
                    lngLimit = 0;
                }

            }
            return lngLimit;
        }
        public void PerformGetInput(IEulerPluginContext context)
        {
            _lowerLimit = GetLimit("Lower", "4");
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

            for (int i = 1489; i < Math.Pow(10,_lowerLimit) - 2 * 3330; i += 2)
            {
                // Unclear if there should be a loop instead of just adding 3330 but
                // I removed the loop after seeing the answer was given by that number
                int num1 = i, num2 = i + 3330, num3 = i + 2 * 3330;

                if (num1.IsPrime() && num2.IsPrime() && num3.IsPrime())
                {
                    // Turn the numbers into lists of digits/chars so we can compare each digit
                    List<char> numbers1 = num1.ToString().ToList();
                    List<char> numbers2 = num2.ToString().ToList();
                    List<char> numbers3 = num3.ToString().ToList();

                    // Sort the lists of digits/chars
                    numbers1.Sort();
                    numbers2.Sort();
                    numbers3.Sort();

                    // Do an element by element check for the lists
                    // If all numbers match we got permutations and have found our answer
                    if (numbers1.SequenceEqual(numbers2) && numbers1.SequenceEqual(numbers3))
                    {
                        retval = string.Format("{0} {1} {2} are primes! ({0}{1}{2})", num1, num2, num3);
                        break;
                    }
                }
            }

            return retval;
        }


    }
}
