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
namespace Problem_52
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public bool IsAsync { get { return true; } }
        public bool ImplementsGetInput { get { return true; } }

        public long _upperLimit;
        public long _lowerLimit;
        //TODO: Replace the project number, Title, and Description
        public int ID { get { return 52; } }
        public string Title { get { return string.Format("Permuted Multiples", ID); } }
        public string Description
        {
            get
            {
                return @"It can be seen that the number, 125874, and its double, 251748, contain exactly the same digits, but in a different order.
Find the smallest positive integer, x, such that 2x, 3x, 4x, 5x, and 6x, contain the same digits.";
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
            _lowerLimit = GetLimit("Lower", "1");
            _upperLimit = GetLimit("Upper", long.MaxValue.ToString());
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
        async Task<string> BruteForceAsync() => BruteForce();

        private string BruteForce()
        {
            string retval = string.Empty;   // return string
            string one = string.Empty;
            string two = string.Empty;
            string three = string.Empty;
            string four = string.Empty;
            string five = string.Empty; 
            string six = string.Empty;
            for (long i = _lowerLimit; _lowerLimit < _upperLimit; i++) {
                one = i.ToString().SortWord();
                two = (2*i).ToString().SortWord();
                if (one == two) {
                    three = (3 * i).ToString().SortWord();
                    if (two == three) {
                        four = (4 * i).ToString().SortWord();
                        if (three == four) {
                            five = (5 * i).ToString().SortWord();
                            if (four == five) {
                                six = (6 * i).ToString().SortWord();
                                if (five == six) {
                                    retval = string.Format("{0} is the permuted multiple.",i);
                                    break;
                                }
                            }
                        }
                    }
                } 

            }

            return retval;
        }

    }
}
