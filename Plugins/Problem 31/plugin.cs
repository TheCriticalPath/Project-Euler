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
namespace Problem_31
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public long _limit;
        public bool ImplementsGetInput { get { return false; } }
        public int ID { get { return 31; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Coin Sums", ID); } }
        public string Description
        {
            get
            {
                return @"In England the currency is made up of pound, £, and pence, p, and there are eight coins in general circulation:

1p, 2p, 5p, 10p, 20p, 50p, £1 (100p) and £2 (200p).
It is possible to make £2 in the following way:

1×£1 + 1×50p + 2×20p + 1×5p + 1×2p + 3×1p
How many different ways can £2 be made using any number of coins?";
            }
        }
        public EulerPlugin() { }

        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "2";

            while (lngLimit < 1)
            {
                Helpers.InputHelper.Show(Name, "Enter Total Amount", ref strLimit);
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
        int[] Denominations = new int[] { 200, 100, 50, 20, 10, 5, 2, 1 };
        public IEulerPluginContext PerformAction(IEulerPluginContext context)
        {
            context.strResultLongText = BruteForce();
            return context;
        }

        public string BruteForce() {
            int combinations = 1;

            combinations = RecursiveSummation(0,0,200);

            return string.Format("There are {0} combinations of the denominations {1} to create £2", combinations, Denominations.JoinInt());
        }
        private int recursionLevel = 0;
        public int RecursiveSummation(int node,int CurrentSum, int DesiredSum){
            int combinations = 0;
            int val = 0;
            //System.Diagnostics.Debug.WriteLine("recursionLevel: {0}, currentSum: {1}, Starting Denomination: {2}", recursionLevel, CurrentSum, DesiredSum);
            for (int i = node; i < Denominations.Length;i++)
            {
                val = CurrentSum + Denominations[i];
                if (val== DesiredSum) {
                    combinations++;
                }
                else if (val <= DesiredSum)
                {
                    recursionLevel++;
                    combinations += RecursiveSummation(i, val, DesiredSum);
                    recursionLevel--;

                }
            }
            return combinations;
        }

    }
}
