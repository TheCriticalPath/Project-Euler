using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;
using System.ComponentModel.Composition;

namespace Problem_5
{
    [Export(typeof(IEulerPlugin))]
    public class Problem_5 : IEulerPlugin
    {
        public long _limit;
        public bool ImplementsGetInput { get { return true; } }
        public string Name { get { return "Problem 5: Smallest Multiple"; } }
        public string Description { get { return "2520 is the smallest number that can be divided by each of the numbers from 1 to 10 without any remainder. What is the smallest positive number that is evenly divisible by all of the numbers from 1 to 20?"; } }

        public Problem_5() { }
        public IEulerPluginContext PerformAction(IEulerPluginContext context)
        {
            context.strResultLongText = GetNumberWithAllMultiplesFrom1ToLimit(_limit);
            return context;
        }

        private string GetNumberWithAllMultiplesFrom1ToLimit(long limit){
            long product = limit*limit;
            bool success = false;
            
            while (!success) {
                success = true;
                for (int i = 2; i <= limit; i++) {
                    if (product % i != 0) {
                        success= false;
                        break;
                    }
                }
                if (!success)
                product += limit;
            }
            return string.Format("The smallest number that is evenly divisible by numbers 1 to {0} is {1}",limit,product);
        }



        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "20";

            while (lngLimit < 1)
            {
                Helpers.InputBox.Show(Name, "Upper Limit", ref strLimit);
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
