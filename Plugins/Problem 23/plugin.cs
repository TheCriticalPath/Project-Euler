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
namespace Problem_23
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        const int SmallestAbundant = 12;
        List<int> AbundantNumbersSums = new List<int>();
        public long _limit;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 23; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Non-aundant sums", ID); } }
        public string Description
        {
            get
            {
                return @"A perfect number is a number for which the sum of its proper divisors is exactly equal to the number. For example, the sum of the proper divisors of 28 would be 1 + 2 + 4 + 7 + 14 = 28, which means that 28 is a perfect number.
A number n is called deficient if the sum of its proper divisors is less than n and it is called abundant if this sum exceeds n.
As 12 is the smallest abundant number, 1 + 2 + 3 + 4 + 6 = 16, the smallest number that can be written as the sum of two abundant numbers is 24. By mathematical analysis, it can be shown that all integers greater than 28123 can be written as the sum of two abundant numbers. However, this upper limit cannot be reduced any further by analysis even though it is known that the greatest number that cannot be expressed as the sum of two abundant numbers is less than this limit.
Find the sum of all the positive integers which cannot be written as the sum of two abundant numbers.";
            }
        }
        public EulerPlugin() { }

        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "28123";

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
            context.strResultLongText = SumNonAbundantNumbers(_limit);
            return context;
        }
        private string SumNonAbundantNumbers(long limit)
        {
            bool[] CanBeWrittenAsAbundantSum = CalculateAllAbundantSums(limit);
            int sum = 0;
            for (int i = 1; i <= limit; i++)
            {
                if (!CanBeWrittenAsAbundantSum[i])
                {
                    sum += i;
                }
            }
            return string.Format("The sum of all the positive integers, <= {1}, which cannot be written as the sum of two abundant numbers is {0}", sum, limit);
        }
        private bool IsAbundant(int i)
        {
            return MathHelper.SumOfFactors(i) > i;
        }

        private List<int> GetAllAbundantNumbers(long limit)
        {
            List<int> retVal = new List<int>();
            for(int i = SmallestAbundant; i < limit;i++){
                if (IsAbundant(i)){
                    retVal.Add(i);
                }
            }
            return retVal;
        }
        private bool[] CalculateAllAbundantSums(long limit)
        {
            List<int> abundant = GetAllAbundantNumbers(limit);
            bool[] canBeWrittenAsAbundant= new bool[limit + 1];
            for (int i = 0; i < abundant.Count; i++)
            {
                for (int j = i; j < abundant.Count; j++)
                {
                    if (abundant[i] + abundant[j] <= limit)
                    {
                        canBeWrittenAsAbundant[abundant[i] + abundant[j]] = true;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return canBeWrittenAsAbundant;
        }
    }
}
