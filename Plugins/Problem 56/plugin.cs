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
namespace Problem_56
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public bool IsAsync { get { return true; } }
        public bool ImplementsGetInput { get { return true; } }

        public long _upperLimit;
        public long _lowerLimit;
        public long _familySize;
        //TODO: Replace the project number, Title, and Description
        public int ID { get { return 56; } }
        public string Title { get { return string.Format("Powerful Digit Sum", ID); } }
        public string Description
        {
            get
            {
                return @"A googol (10^100) is a massive number: one followed by one-hundred zeros; 100^100 is almost unimaginably large: one followed by two-hundred zeros. Despite their size, the sum of the digits in each number is only 1.
Considering natural numbers of the form, a^b, where a, b < 100, what is the maximum digital sum?";
            }
        }

        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
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

        //TODO: Modify for the values in this routine to meet the needs of the specific problem
        public void PerformGetInput(IEulerPluginContext context)
        {
            _upperLimit = GetLimit("Upper", "100");

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
            int digitalSum = 0;
            int temp = 0;
            int i = 2, j = 2;
            BigInteger val = BigInteger.Zero;

            try
            {
                for (i = 2; i < _upperLimit; i++)
                {
                    for (j = 2; j < _upperLimit; j++)
                    {
                        val = BigInteger.Pow(i, j);
                        temp = val.DigitalSum();
                        if (temp > digitalSum)
                        {
                            digitalSum = temp;
                            System.Diagnostics.Debug.WriteLine($"Current max digital sum is {i}^{j} and {digitalSum}");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return $"Max digital sum for a^b where a,b < {_upperLimit} is {digitalSum}";
        }

    }
}
