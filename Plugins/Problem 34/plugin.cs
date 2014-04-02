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
namespace Problem_34
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public long _limit;
        public bool ImplementsGetInput { get { return false; } }
        public int ID { get { return 34; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Digit Factorials", ID); } }
        public string Description
        {
            get
            {
                return @"145 is a curious number, as 1! + 4! + 5! = 1 + 24 + 120 = 145.

Find the sum of all numbers which are equal to the sum of the factorial of their digits.

Note: as 1! = 1 and 2! = 2 are not sums they are not included.";
            }
        }
        public EulerPlugin() { }

        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "1000";

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
            context.strResultLongText = BruteForce();
            return context;
        }
        public string BruteForce() {
            int sum = 0;

            for (int i = 3; i < 100000; i++) {
                if (MeetsCondition(i)) {
                    sum += i;
                }
            }
            
            return string.Format("Sum of numbers which are equal to the sum of their digits factorials is {0}", sum);
        }

        public static int[] Factorials =  { 1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880 };
        public bool MeetsCondition(int i) {
            int fSum = 0;
            int k = i;
            int temp = 0;
            int digit = 0;
            for (int j = i.ToString().Length; j> 0; j--) {
                digit = (int)(k / Math.Pow(10, j - 1));
                //temp = (int)MathHelper.Factorial(digit);
                temp = Factorials[digit];
                fSum += temp;
                k -= (int)(digit * Math.Pow(10, j - 1));
                if (fSum > i) { break; }
            }

            return i == fSum;
        }
    }
}
