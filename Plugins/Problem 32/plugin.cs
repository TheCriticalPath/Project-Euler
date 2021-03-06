﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using Helpers;
using System.Numerics;
namespace Problem_32
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public bool IsAsync { get { return false; } }
        public long _limit;
        public bool ImplementsGetInput { get { return false; } }
        public int ID { get { return 32; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public async Task<IEulerPluginContext> PerformActionAsync(IEulerPluginContext context)
        {
            throw new NotImplementedException();
        }
        public string Title { get { return string.Format("Pandigital products", ID); } }
        public string Description
        {
            get
            {
                return @"We shall say that an n-digit number is pandigital if it makes use of all the digits 1 to n exactly once; for example, the 5-digit number, 15234, is 1 through 5 pandigital.
The product 7254 is unusual, as the identity, 39 × 186 = 7254, containing multiplicand, multiplier, and product is 1 through 9 pandigital.
Find the sum of all products whose multiplicand/multiplier/product identity can be written as a 1 through 9 pandigital.
HINT: Some products can be obtained in more than one way so be sure to only include it once in your sum.";
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
            products = new List<int>();
            context.strResultLongText = SumOfPanDigitalProducts();
            return context;
        }

        public string SumOfPanDigitalProducts() {
            int sum = 0;

            for (int a = 1; a < 2000; a++) {
                for (int b = 1; b < 2000; b++)
                {
                 
                    if (IsPanDigitalProduct(a, b) && !products.Contains(a*b))
                    {
                        products.Add(a*b);
                        System.Diagnostics.Debug.WriteLine(string.Format("{0} * {1} = {2}", a, b, a*b));
                    }
                }
            }
            sum = products.Sum();
            return string.Format("Sum of pandigital numbers is {0}", sum);
        }

        List<int> products;
        public bool IsPanDigitalProduct(int a, int b)
        {
            bool retVal = false;
            int c = a * b;

            string str = a.ToString() + b.ToString() + c.ToString();
            retVal = str.IsPanDigital();
        
            return retVal;
        }
    }
}
