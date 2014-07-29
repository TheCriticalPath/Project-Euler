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
namespace Problem_41
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public bool IsAsync { get { return true; } }
        public long _limit;
        public bool ImplementsGetInput { get { return false; } }
        public int ID { get { return 41; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Pandigital Prime", ID); } }
        public string Description
        {
            get
            {
                return @"We shall say that an n-digit number is pandigital if it makes use of all the digits 1 to n exactly once. For example, 2143 is a 4-digit pandigital and is also prime.

What is the largest n-digit pandigital prime that exists?

";
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
        public IEulerPluginContext PerformAction(IEulerPluginContext context) {

            DateTime dtStart, dtEnd;
            dtStart = DateTime.Now;
            context.strResultLongText = BruteForce();
            dtEnd = DateTime.Now;
            context.spnDuration = dtEnd.Subtract(dtStart);
            return context;

        }
       public   Task<IEulerPluginContext> PerformActionAsync(IEulerPluginContext context)
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
       public string BruteForce() {
           int prime = 0;
           for (int i = 1000000; i <= 7654321; i++)
           {
               if (!(i % 5 == 0))
               {
                   if (i.IsPrime() && i.ToString().IsPanDigital())
                   {
                       prime = i;
                   }
               }
           }
           return string.Format("The largest Pandigital Prime is {0}", prime);
       }
        async Task<string> BruteForceAsync()
        {
            int prime = 0;
            for (int i = 1000000; i <= 7654321; i++)
            {
                if (!(i % 5 == 0))
                {
                    if (i.IsPrime() && i.ToString().IsPanDigital())
                    {
                        prime = i;
                    }
                }
            }
            return string.Format("The largest Pandigital Prime is {0}", prime);
        }

    }
}
