using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;
using System.ComponentModel.Composition;

namespace Problem_2
{
    [Export(typeof(IEulerPlugin))]
     public class Problem_2:IEulerPlugin
    {
        public int _limit;
        public bool ImplementsGetInput { get {return true;}}
        public string Name {get{return "Project Euler: Problem 2";}}
        public string Description {get{return "Sum of even Fibonacci numbers.";}}

        public Problem_2() { }
        public IEulerPluginContext PerformAction(IEulerPluginContext context) {
            context.strResultLongText = SumOfEventFibonacci(_limit);
            return context;
        }

        private int GetLimit()
        {
            int intLimit = 0;
            string strLimit = string.Empty;

            while (intLimit < 1)
            {
                Helpers.InputBox.Show(Name, "Enter Upper Limit", ref strLimit);
                if (!int.TryParse(strLimit, out intLimit))
                {
                    intLimit = 0;
                }

            }
            return intLimit;
        }
        
        public void PerformGetInput(IEulerPluginContext context)
        {
            _limit = GetLimit();
        }

        public string SumOfEventFibonacci(int limit)
        {
            int sum = 0;
            int f0 = 1;
            int f1 = 1;
            int f2 = 1;
            int i = 3;
            while(f0 < limit)
            {
                f0 = f1 + f2;
                if (i % 3 == 0)
                {
                    sum += f0;
                }
                f2 = f1;
                f1 = f0;
                i++;
            }
            return string.Format("Sum of even Fibonacci numbers less than {0} is {1}", limit, sum);
        }

    }
}
