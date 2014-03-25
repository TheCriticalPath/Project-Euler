using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using System.Numerics;
using Helpers;
namespace Problem_16
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public long _limit;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 16; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Power digit sum", ID); } }
        public string Description { get { return "What is the sum of the digits of the number 2^x?"; } }
        public EulerPlugin() { }

        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "1000";

            while (lngLimit < 1)
            {
                Helpers.InputHelper.Show(Name, "Enter value for x", ref strLimit);
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

            context.strResultLongText = PowerDigitsSum((int)_limit);
            return context;
        }
        public string PowerDigitsSum(int power) {
            BigInteger x = BigInteger.Pow(2, power);
            return string.Format("The sum of the digits in 2^{0} is {1}", power, x.ToString("F0").SumDigits());
        }

    }
}
