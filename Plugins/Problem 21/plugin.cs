using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using Helpers;
namespace Problem_21
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public bool IsAsync { get { return false; } }
        public long _limit;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 21; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Amicable Numbers", ID); } }
        public string Description { get { return "Let d(n) be defined as the sum of proper divisors of n (numbers less than n which divide evenly into n).  If d(a) = b and d(b) = a, where a ≠ b, then a and b are an amicable pair and each of a and b are called amicable numbers.  For example, the proper divisors of 220 are 1, 2, 4, 5, 10, 11, 20, 22, 44, 55 and 110; therefore d(220) = 284. The proper divisors of 284 are 1, 2, 4, 71 and 142; so d(284) = 220.  Evaluate the sum of all the amicable numbers under 10000."; } }
        public EulerPlugin() { }
        public async Task<IEulerPluginContext> PerformActionAsync(IEulerPluginContext context)
        {
            throw new NotImplementedException();
        }
        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "10000";

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
            AmicableSums = new Dictionary<int, int>();
            context.strResultLongText = CalculateAllAmicableSums(_limit);
            return context;
        }
        Dictionary<int, int> AmicableSums ;
        private string CalculateAllAmicableSums(long limit)
        {
            int sum = 0;
            int a_sum1 = 0;
            int a_sum2 = 0;

            for (int i = 2; i < limit; i++)
            {
                if (!AmicableSums.ContainsKey(i))
                {
                    a_sum1 = (int)MathHelper.GetDivisors(i).SumDigits();
                    AmicableSums.Add(i, a_sum1);

                    if (AmicableSums.ContainsKey(a_sum1))
                    {
                        AmicableSums.TryGetValue(a_sum1, out a_sum2);
                    }
                    else
                    {
                        a_sum2 = (int)MathHelper.GetDivisors(a_sum1).SumDigits();
                        AmicableSums.Add(a_sum1, a_sum2);
                    }

                    if (i == a_sum2 && i != a_sum1)
                    {
                        sum += a_sum1 + a_sum2;
                    }
                }
            }

            return string.Format("The sum of amicable numbers less than {0} is {1}", limit, sum);
        }
    }
}
