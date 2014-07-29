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
namespace Problem_30
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public bool IsAsync { get { return false; } }
        public long _limit;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 30; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public async Task<IEulerPluginContext> PerformActionAsync(IEulerPluginContext context)
        {
            throw new NotImplementedException();
        }
        public string Title { get { return string.Format("Digit fifth powers", ID); } }
        public string Description
        {
            get
            {
                return @"Surprisingly there are only three numbers that can be written as the sum of fourth powers of their digits:

1634 = 14 + 64 + 34 + 44
8208 = 84 + 24 + 04 + 84
9474 = 94 + 44 + 74 + 44
As 1 = 14 is not a sum it is not included.

The sum of these numbers is 1634 + 8208 + 9474 = 19316.

Find the sum of all the numbers that can be written as the sum of fifth powers of their digits.";
            }
        }
        public EulerPlugin() { }

        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "5";

            while (lngLimit < 1)
            {
                Helpers.InputHelper.Show(Name, "Enter power", ref strLimit);
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
            context.strResultLongText = CalculateDigitPowerSums(_limit);
            return context;
        }


        public string CalculateDigitPowerSums(long power)
        {
            int sum = 0;
            for (int i = 1000; i < Math.Pow(10, power +1); i++)
            {
                if (IsSumOfPowerDigits(i, (int)power)) {
                    sum += i;
                    System.Diagnostics.Debug.WriteLine(i);
                }
            }
            return string.Format("The sum of all the numbers that can be written as the sum of x^{0} powers of their digits is {1}", power, sum);
        }
        public bool IsSumOfPowerDigits(int number, int power)
        {
            bool retVal = false;
            int length = number.ToString().Length;
            int sum = 0;
            int digit = 0;
            int lclNumber = number;
            int[] numbers = new int[length];
            for (int i = 0 ; i <length; i++)
            {
                digit = (int)Math.Floor(number / (Math.Pow(10, length - (i + 1))));
                numbers[i] = digit;
                number -= digit * (int)Math.Pow(10, length-(1+i));
                sum += (int)Math.Pow(numbers[i], power);
            }

            return sum == lclNumber;
        }
    }
}
