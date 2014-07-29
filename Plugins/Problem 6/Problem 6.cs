using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;
using System.ComponentModel.Composition;

namespace Problem_6
{
    [Export(typeof(IEulerPlugin))]
    public class Problem_6 : IEulerPlugin
    {
        public bool IsAsync { get { return false; } }
        public long _limit;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 6; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }

        public string Title { get { return string.Format("Sum square difference",ID); } }
        public string Description { get { return "The sum of the squares of the first ten natural numbers is, 12 + 22 + ... + 102 = 385 The square of the sum of the first ten natural numbers is, (1 + 2 + ... + 10)2 = 552 = 3025 Hence the difference between the sum of the squares of the first ten natural numbers and the square of the sum is 3025 − 385 = 2640. Find the difference between the sum of the squares of the first one hundred natural numbers and the square of the sum."; } }

        public Problem_6() { }
        public IEulerPluginContext PerformAction(IEulerPluginContext context)
        {
            context.strResultLongText = GetSumSquareDifference(_limit);
            //context.strResultLongText = Alternative(_limit);
            return context;
        }
        public async Task<IEulerPluginContext> PerformActionAsync(IEulerPluginContext context)
        {
            throw new NotImplementedException();
        }
        private string GetSumSquareDifference(long limit)
        {
            long squareOfSums = 0;
            long sumOfSquares = 0;

            squareOfSums = (long)Math.Pow((limit * (limit + 1)) / 2,2d);
            sumOfSquares = (limit * (limit + 1) * (2 * limit + 1)) / 6;

            return string.Format("The Sum Square Difference of the first {0} numbers is {1} - {2} = {3}", new Object[] { limit, squareOfSums, sumOfSquares, Math.Abs(squareOfSums - sumOfSquares )});
        }



        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "20";

            while (lngLimit < 1)
            {
                Helpers.InputHelper.Show(Name, "Upper Limit", ref strLimit);
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


    }
}
