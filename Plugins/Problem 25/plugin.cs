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
namespace Problem_25
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public long _limit;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 25; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("1000-digit Fibonacci Number", ID); } }
        public string Description { get { return "What is the first term in the Fibonacci sequence to contain 1000 digits?"; } }
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
            context.strResultLongText = GetFirstFibNumberWithNDigits(_limit);
            return context;
        }
        public string GetFirstFibNumberWithNDigits(long limit) {
            BigInteger[] fibonacci = new BigInteger[3];
            fibonacci[0] = 1;
            fibonacci[1] = 1;
            fibonacci[2] = 1;
            int index = 2;
            do
            {
                fibonacci[2] = fibonacci[1];
                fibonacci[1] = fibonacci[0];
                fibonacci[0] = fibonacci[1] + fibonacci[2];
                index++;



            } while (fibonacci[0].ToString().Length < limit);

            return string.Format("The {0}th term in the Fibonacci sequence contatins {1} digits", index, fibonacci[0].ToString().Length);
                    
        }

    }
}
