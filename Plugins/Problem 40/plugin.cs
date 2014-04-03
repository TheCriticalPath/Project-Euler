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
namespace Problem_40
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public long _limit;
        public bool ImplementsGetInput { get { return false; } }
        public int ID { get { return 40; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Champernowne\'s constant", ID); } }
        public string Description
        {
            get
            {
                return @"An irrational decimal fraction is created by concatenating the positive integers:

0.123456789101112131415161718192021...

It can be seen that the 12th digit of the fractional part is 1.

If dn represents the nth digit of the fractional part, find the value of the following expression.

d1 × d10 × d100 × d1000 × d10000 × d100000 × d1000000";
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

        public string BruteForce() {

            int product = 1;
            string s = string.Empty;
            int index = 1;
            do
            {
                s += index.ToString();
                index++;
            } while (s.Length <= 1000000);

            product = int.Parse(s.Substring(0,1)) * 
                      int.Parse(s.Substring(9,1)) * 
                      int.Parse(s.Substring(99,1)) * 
                      int.Parse(s.Substring(999,1)) * 
                      int.Parse(s.Substring(9999,1)) * 
                      int.Parse(s.Substring(99999,1)) *
                      int.Parse(s.Substring(999999,1));


            return string.Format("The Product Is {0}", product);
        }

    }
}
