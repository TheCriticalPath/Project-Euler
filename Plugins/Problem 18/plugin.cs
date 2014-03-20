using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using Helpers;
namespace Problem_18
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public long _limit;
        public bool ImplementsGetInput { get { return false; } }
        public int ID { get { return 18; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Maximum path sum I", ID); } }
        public string Description { get { return "Find the maximum total from top to bottom of the triangle below:"; } }
        public string _Input = "75~95 64~17 47 82~18 35 87 10~20 04 82 47 65~19 01 23 75 03 34~88 02 77 73 07 63 67~99 65 04 28 06 16 70 92~41 41 26 56 83 40 80 70 33~41 48 72 33 47 32 37 16 94 29~53 71 44 65 25 43 91 52 97 51 14~70 11 33 28 77 73 17 78 39 68 17 57~91 71 52 38 17 14 91 43 58 50 27 29 48~63 66 04 68 89 53 67 30 73 16 69 87 40 31~04 62 98 27 23 09 70 98 73 93 38 53 60 04 23";
        public string[] Triangle;
        public EulerPlugin() {
        }

        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "1000";

            while (lngLimit < 1)
            {
                Helpers.InputBox.Show(Name, "Enter Limit", ref strLimit);
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
            Triangle = _Input.Split('~');
            context.strResultLongText = BottomUpSum(Triangle);
            return context;
        }
        public string BottomUpSum(string[] Tri) {
            string[] local = Tri;
            string[] line1;
            string[] line2;
            int sum = 0;

            for (int row = local.Length-1; row > 0; row--) {
                line2 = local[row].Split(' ');
                line1 = local[row - 1].Split(' ');
                for (int i = 0; i < line1.Length; i++) {
                    line1[i] = (int.Parse(line1[i]) + (int)Math.Max(int.Parse(line2[i]), int.Parse(line2[i + 1]))).ToString();
                }
                
                local[row-1] = string.Join(" ", line1);
            }
            return string.Format("The maximum path sum of the triangle is {0}.",local[0]);
        }
    
    }
}
