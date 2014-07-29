using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using Helpers;
namespace Problem_22
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public bool IsAsync { get { return false; } }
        private double CHAR_VALUE_A;
        private List<string> _Input;
        public long _limit;
        public bool ImplementsGetInput { get { return false; } }
        public int ID { get { return 22; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Names scores", ID); } }
        public string Description { get { return @"Using names.txt (right click and 'Save Link/Target As...'), a 46K text file containing over five-thousand first names, begin by sorting it into alphabetical order. Then working out the alphabetical value for each name, multiply this value by its alphabetical position in the list to obtain a name score.
For example, when the list is sorted into alphabetical order, COLIN, which is worth 3 + 15 + 12 + 9 + 14 = 53, is the 938th name in the list. So, COLIN would obtain a score of 938 × 53 = 49714.
What is the total of all the name scores in the file?"; } }
        public EulerPlugin() {
            CHAR_VALUE_A = 'A' - 1;
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
                _Input = FileHelper.GetFile("Input\\names.txt",",");

                context.strResultLongText = CalculateNameScores(_Input);
            }
            catch (Exception e) {
                context.strResultLongText = e.ToString();
            }
            
            return context;
        }

        private string CalculateNameScores(List<string> Input) {
            double sum = 0D;
            Input.Sort();
            int i = 1;
            string t;
            foreach (string s in Input) {
               t =  s.Replace("\"", string.Empty);
                sum += CalculateStringScore(t.ToUpper()) * i++;
            }
            return string.Format("The sum of all name scores in the file is {0}", sum );
        }

        private double  CalculateStringScore(string s) {
            double score = 0;
            foreach (char c in s.ToCharArray()) {
                score += c - CHAR_VALUE_A;
            }
            return score;
        }


    }
}
