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
namespace Problem_33
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public bool IsAsync { get { return false; } }
        public long _limit;
        public bool ImplementsGetInput { get { return false; } }
        public int ID { get { return 33; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public async Task<IEulerPluginContext> PerformActionAsync(IEulerPluginContext context)
        {
            throw new NotImplementedException();
        }
        public string Title { get { return string.Format("Digit Cancelling Fractions", ID); } }
        public string Description
        {
            get
            {
                return @"The fraction 49/98 is a curious fraction, as an inexperienced mathematician in attempting to simplify it may incorrectly believe that 49/98 = 4/8, which is correct, is obtained by cancelling the 9s.

We shall consider fractions like, 30/50 = 3/5, to be trivial examples.

There are exactly four non-trivial examples of this type of fraction, less than one in value, and containing two digits in the numerator and denominator.

If the product of these four fractions is given in its lowest common terms, find the value of the denominator.";
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

        private bool CancellableFraction(int num, int den)
        {
            if (num % 10 == 0 || den % 10 == 0)
            {
                return false;
            }
            else
            {
                double numTen = num / 10;
                double numOne = num % 10;
                double denTen = den / 10;
                double denOne = den % 10;

                double fraction = 0;
                if (numTen == denTen)
                {
                    fraction = numOne / denOne;
                }
                else if (numOne == denTen)
                {
                    fraction = numTen / denOne;
                }
                else if (numTen == denOne) {
                    fraction = numOne / denTen;
                }
                else if (numOne == denOne) {
                    fraction = numTen / denTen;
                }
                return fraction == num / (double)den;
            }


        }
        public string BruteForce()
        {
            int denominator = 1;
            int numerator = 1;
            int gcd = 0;
            for (int i = 11; i < 99; i++)
            {
                for (int j = i + 1; j <= 99; j++)
                {
                    if (CancellableFraction(i, j)) {
                        numerator *= i;
                        denominator *= j;
                    }
                }
            }
           gcd = MathHelper.GCD(numerator, denominator);
           gcd = denominator / gcd;
            return string.Format("The lowest common denominator product of the 4 non-trivial fractions is: {0}.", gcd);
        }
    }
}
