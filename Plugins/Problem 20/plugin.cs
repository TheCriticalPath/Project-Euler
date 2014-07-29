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
namespace Problem_20
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public bool IsAsync { get { return false; } }

        public long _limit;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 20; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Factorial digit sum", ID); } }
        public string Description { get { return "Find the sum of the digits in the number 100!"; } }
        public EulerPlugin() { }
        public async Task<IEulerPluginContext> PerformActionAsync(IEulerPluginContext context)
        {
            throw new NotImplementedException();
        }
        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "100";

            while (lngLimit < 1)
            {
                Helpers.InputHelper.Show(Name, "Enter Number", ref strLimit);
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
            context.strResultLongText = SumFactorialDigits(_limit);
            return context;
        }

        private string SumFactorialDigits(long number)
        {
            BigInteger bi = new BigInteger();
            bi = MathHelper.Factorial(number);
            return string.Format("Sum of the digits in the answer for {0}! is {1}", number, bi.ToString("F0").SumDigits());
        }

    }
}
