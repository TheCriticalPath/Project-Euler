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
namespace Problem_36
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public bool IsAsync { get { return false; } }
        public long _limit;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 36; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public async Task<IEulerPluginContext> PerformActionAsync(IEulerPluginContext context)
        {
            throw new NotImplementedException();
        }
        public string Title { get { return string.Format("Double-base palindromes", ID); } }
        public string Description
        {
            get
            {
                return @"The decimal number, 585 = 10010010012 (binary), is palindromic in both bases.

Find the sum of all numbers, less than one million, which are palindromic in base 10 and base 2.

(Please note that the palindromic number, in either base, may not include leading zeros.)";
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
        public string BruteForce(long limit)
        {
            int sum = 0;

            for (int i = 1; i < limit; i++)
            {
                string base10 = i.ToString();
                string base2 = Convert.ToString(i, 2);
                if (IsPalidrome(base10) && IsPalidrome(base2))
                {
                    sum += i;
                }
            }

            return string.Format("The sum of all double base palindromes less than {0} is {1}", limit, sum);
        }

        private static bool IsPalidrome(string s)
        {
            while (s.Substring(0, 1) == "0")
            {
                s = s.Shift(StringHelper.SHIFTDIRECTION.SHIFTLEFT);
            }
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return s == new string(charArray);
        }

    }
}
