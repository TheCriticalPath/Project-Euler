using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;
using System.ComponentModel.Composition;
namespace Problem_1
{
    [Export(typeof(IEulerPlugin))]
    public class Problem_1 : IEulerPlugin
    {
        public bool ImplementsGetInput { get { return true; } }
        public int _limit;
        public string Name
        {
            get { return "Problem 1: Multiples of 3 and 5"; }
        }

        public string Description
        {
            get { return "If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9.  The sum of these multiples is 23.  Find the sum of all the multiples of 3 or 5 below 1000."; }
        }

        public Problem_1(){ }

        // The single point of entry to our plugin.
        // Accepts an IEulerPluginContext object.
        public IEulerPluginContext PerformAction( IEulerPluginContext context)
        {
            context.strResultLongText = AlternateSum(_limit);
            return context;
        }

       public void PerformGetInput( IEulerPluginContext context){
            _limit = GetLimit();
        }

        private int GetLimit() {
            int intLimit = 0;
            string strLimit = "1000";

            while (intLimit < 1) {
                Helpers.InputBox.Show(Name, "Enter Upper Limit", ref strLimit);
                if (!int.TryParse(strLimit, out intLimit)) {
                    intLimit = 0;   
                }

            }
            return intLimit;
        }
        public string SumMultiplesOf3And5(int limit) {
            int sum = 0;
            for (int i = 1; i <= limit; i++)
            {
                if (i % 3 == 0 || i % 5 == 0)
                {
                    sum += i;
                }
            }
            return string.Format("Sum of multiples from 3 to 5 for limit {0} is {1}", limit, sum);
        }

        public string AlternateSum(int limit) {
            double sum =  GeometricSum(3,limit) + GeometricSum(5,limit) - GeometricSum(15,limit);
            return string.Format("Sum of multiples from 3 to 5 for limit {0} is {1}", limit, sum);
        }
        public double GeometricSum(int multiple, int limit) {
            int upper = limit/multiple;
            return multiple * upper * (upper+ 1) / 2;
        }
    }
}
