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
namespace Problem_43
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {

        public bool IsAsync { get { return true; } }

        public long _limit;
        public bool ImplementsGetInput { get { return false; } }
        public int ID { get { return 43; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Sub-string divisibility", ID); } }
        public string Description
        {
            get
            {
                return @"The number, 1406357289, is a 0 to 9 pandigital number because it is made up of each of the digits 0 to 9 in some order, but it also has a rather interesting sub-string divisibility property.

Let d1 be the 1st digit, d2 be the 2nd digit, and so on. In this way, we note the following:

d2d3d4=406 is divisible by 2
d3d4d5=063 is divisible by 3
d4d5d6=635 is divisible by 5
d5d6d7=357 is divisible by 7
d6d7d8=572 is divisible by 11
d7d8d9=728 is divisible by 13
d8d9d10=289 is divisible by 17
Find the sum of all 0 to 9 pandigital numbers with this property.";
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

            DateTime dtStart, dtEnd;
            dtStart = DateTime.Now;
            context.strResultLongText = BruteForce();
            dtEnd = DateTime.Now;
            context.spnDuration = dtEnd.Subtract(dtStart);
            return context;

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

        public string BruteForce()
        {

            Int64 sum = 0;
            for (Int64 i = 1000000001; i <= 9876543210; i += 2)
            {
                if (!(i % 5 == 0))
                {
                    if (i.ToString().IsPanDigital())
                    {
                        System.Diagnostics.Debug.WriteLine(string.Format("Found Pandigital {0}", i));
                        //if (i.IsPrime())
                        //{
                        //    System.Diagnostics.Debug.WriteLine(string.Format("Found Prime {0}",i));
                        if (i.HasSubStringDivisibility())
                        {
                            System.Diagnostics.Debug.WriteLine(string.Format("Has Sub String Div {0}", i));
                            sum += i;

                        }
                        //}
                    }
                }
            }
            return string.Format("The sum of all 0 to 9 triangle sum pandigital numbers {0}", sum);
        }
        async Task<string> BruteForceAsync()
        {
            return BruteForce();
        }

    }
}
