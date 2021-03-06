﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using Helpers;
namespace Problem_12
{
    [Export(typeof(IEulerPlugin))]
    public class Problem_12 : IEulerPlugin
    {
        public bool IsAsync { get { return false; } }
        public long _limit;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 12; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Highly divisible triangular number", ID); } }
        public string Description { get { return "The sequence of triangle numbers is generated by adding the natural numbers. So the 7th triangle number would be 1 + 2 + 3 + 4 + 5 + 6 + 7 = 28.  What is the value of the first triangle number to have over five hundred divisors?"; } }
        public Problem_12() { }
        public async Task<IEulerPluginContext> PerformActionAsync(IEulerPluginContext context)
        {
            throw new NotImplementedException();
        }
        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "500";

            while (lngLimit < 1)
            {
                Helpers.InputHelper.Show(Name, "Enter number for factors", ref strLimit);
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
            context.strResultLongText = CalculateTriangularNumbers(_limit);
            return context;
        }

        public string CalculateTriangularNumbers(long limit)
        {
            int lcv = 1;
            int numFactors = 0;
            int triangleNumber = 0;
            int curCandidate = 0;
            int maxFactors = 0;
            while (numFactors <= 500)
            {
                triangleNumber += lcv;
                if (triangleNumber > limit*limit)
                {

                    if (lcv > 90000)
                    {
                        numFactors = PollardRhoHeuristic(triangleNumber);
                    }
                    else {
                        numFactors = MathHelper.CountFactors(triangleNumber);
                    }
                    if (numFactors > maxFactors) { maxFactors = numFactors; curCandidate = triangleNumber; }
                }
                lcv++;
            }
            return string.Format("The triangle number {0} has {1} factors exceeding limit {2}", triangleNumber, numFactors, limit);
        }
        Random r = new Random();
        public int PollardRhoHeuristic(long triangleNumber)
        {
            int n = (int)triangleNumber;
            List<int> factors = new List<int>();
            int d = 0;
            int i = 0;
            List<int> x = new List<int>();
            x.Add(r.Next(0, n - 1));
            int y = x[0];
            int k = 2;
            do
            {
                i++;
                x.Add(x[i - 1] - 1 % n);
                d = MathHelper.GCD(y - x[i], n);
                if (d != 1 && d != n)
                {
                    if (!factors.Contains(d)) {  
                        factors.Add(d);
                        //System.Diagnostics.Debug.WriteLine(d);
                    }
                }
                if (d == n) { break; }
                if (i == k)
                {
                    y = x[i];
                    k *= 2;
                }
            } while (true);
            //System.Diagnostics.Debug.WriteLine("{0} has {1} factors.", n,factors.Count() + 2);
            return factors.Count() + 2; // add 2 for 1 and n;
        }
  }
}
