using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using Helpers;

namespace Problem_67
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public bool IsAsync { get { return false; } }
        public long _limit;
        public bool ImplementsGetInput { get { return false; } }
        public int ID { get { return 67; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Maximum path sum I", ID); } }
        public string Description { get { return "Find the maximum total from top to bottom of the triangle below:"; } }
        public string _Input = "";
        public string[] Triangle;
        public EulerPlugin()
        {
        }
        public async Task<IEulerPluginContext> PerformActionAsync(IEulerPluginContext context)
        {
            throw new NotImplementedException();
        }
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
            try
            {
                Triangle = Helpers.FileHelper.GetFileAsStringArray("Input\\triangle.txt");
                context.strResultLongText = BottomUpSum(Triangle);
            }
            catch (Exception ex) {
                context.strResultLongText = ex.ToString();
            }
            return context;
        }
        public string BottomUpSum(string[] Tri)
        {
            string[] local = Tri;
            string[] line1;
            string[] line2;
            int sum = 0;

            for (int row = local.Length - 1; row > 0; row--)
            {
                line2 = local[row].Split(' ');
                line1 = local[row - 1].Split(' ');
                for (int i = 0; i < line1.Length; i++)
                {
                    line1[i] = (int.Parse(line1[i]) + (int)Math.Max(int.Parse(line2[i]), int.Parse(line2[i + 1]))).ToString();
                }

                local[row - 1] = string.Join(" ", line1);
            }
            return string.Format("The maximum path sum of the triangle is {0}.", local[0]);
        }

    }
}
