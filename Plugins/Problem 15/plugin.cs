﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using Helpers;
namespace Problem_15
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public bool IsAsync { get { return false; } }
        public long _limit;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 15; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Lattice Paths ", ID); } }
        public string Description { get { return "Starting in the top left corner of a 2×2 grid, and only being able to move to the right and down, there are exactly 6 routes to the bottom right corner.  How many such routes are there through a 20×20 grid?"; } }
        public EulerPlugin() { }
        public async Task<IEulerPluginContext> PerformActionAsync(IEulerPluginContext context)
        {
            throw new NotImplementedException();
        }
        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "20";

            while (lngLimit < 1)
            {
                Helpers.InputHelper.Show(Name, "Enter limit", ref strLimit);
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
            context.strResultLongText = GetLatticePathLength(_limit);
            return context;
        }


        public string GetLatticePathLength(long _limit)
        {
            //Binomial Coeffiecient
            int x = (int)_limit * 2;
            int y = (int)_limit;
            double xF = MathHelper.Factorial(x);
            double yF = MathHelper.Factorial(y);
            double x_minus_yF = MathHelper.Factorial(x - y);
            double value = (xF) / (yF * x_minus_yF);
            
            return string.Format("The equation f(x)=(2x)!/(x!(2x-x)!). f({0}) = {1}", _limit, value);
        }



    }
}
