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
namespace Problem_46
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public bool IsAsync { get { return true; } }
        public bool ImplementsGetInput { get { return true; } }

        public long _upperLimit;
        public long _lowerLimit;
        public int ID { get { return 46; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Goldbach's other conjecture", ID); } }
        public string Description
        {
            get
            {
                return @"It was proposed by Christian Goldbach that every odd composite number can be written as the sum of a prime and twice a square.

9 = 7 + 2×12
15 = 7 + 2×22
21 = 3 + 2×32
25 = 7 + 2×32
27 = 19 + 2×22
33 = 31 + 2×12

It turns out that the conjecture was false.

What is the smallest odd composite that cannot be written as the sum of a prime and twice a square?";
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
            _lowerLimit = GetLimit("Lower", "286");
            _upperLimit = GetLimit("Upper", "2000");
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
            retval = "Not Implemented due to lost data from computer corruption.  Answer is 5777";
            return retval;
        }


    }
}
