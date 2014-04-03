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
namespace Problem_39
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public long _limit;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 39; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Integer Right Triangles", ID); } }
        public string Description
        {
            get
            {
                return @"If p is the perimeter of a right angle triangle with integral length sides, {a,b,c}, there are exactly three solutions for p = 120.

{20,48,52}, {24,45,51}, {30,40,50}

For which value of p ≤ 1000, is the number of solutions maximised?";
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
            context.strResultLongText = BruteForce(_limit);
            return context;
        }

        internal class triple
        {
            public int a { get; set; }
            public int b { get; set; }
            public int c { get; set; }
        }
        Dictionary<int, int> Perimeters = new Dictionary<int, int>();
        public string BruteForce(long limit)
        {
            int p = 0;
            int solutions = 0;
            int c = 0;
            for (int a = 2; a < limit / 2; a++)
            {
                for (int b = 3; b < limit / 2; b++)
                {
                    if (int.TryParse(Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2)).ToString(), out c))
                    {
                        p = a + b + c;
                        if (p <= limit)
                        {
                            if (Perimeters.ContainsKey(p))
                            {
                                Perimeters[p]++;
                            }
                            else
                            {
                                Perimeters.Add(p, 1);
                            }
                        }
                    }

                }

            }
            IOrderedEnumerable<KeyValuePair<int, int>> ioe = Perimeters.OrderByDescending(a => a.Value);
            p = ioe.First().Key;
            solutions = ioe.First().Value;
            return string.Format("The perimeter {0} has {1} solutions.", p, solutions);
        }

    }
}
