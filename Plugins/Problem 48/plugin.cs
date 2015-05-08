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
namespace Problem_48
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public bool IsAsync { get { return true; } }
        public bool ImplementsGetInput { get { return true; } }

        public long _upperLimit;
        public long _lowerLimit;
        public int ID { get { return 48; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Self Powers", ID); } }
        public string Description
        {
            get
            {
                return @"The series, 11 + 22 + 33 + ... + 1010 = 10405071317.
Find the last ten digits of the series, 11 + 22 + 33 + ... + 10001000.";
            }
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
        public void PerformGetInput(IEulerPluginContext context)
        {
            _lowerLimit = GetLimit("Lower", "1000");
            //_upperLimit = GetLimit("Upper", "2000");
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
            BigInteger results = 0;
            for (int i = 1; i <= _lowerLimit;i++ )
            {
                try
                {
                    results += BigInteger.ModPow(i, i,10000000000);
                    results %= 10000000000;
                }
                catch (Exception e) {
                    System.Diagnostics.Debug.WriteLine("Error");
                }
            }
            return retval = String.Format ("The last 10 digits of the sum of the series n^n from 1 to {0} is {1}",_lowerLimit, results);
        }


    }
}
