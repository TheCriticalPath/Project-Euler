using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using Helpers;
namespace Problem_17
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public long _limit;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 17; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Number Letter Counts", ID); } }
        public string Description { get { return "If all the numbers from 1 to 1000 (one thousand) inclusive were written out in words, how many letters would be used?  NOTE: Do not count spaces or hyphens. For example, 342 (three hundred and forty-two) contains 23 letters and 115 (one hundred and fifteen) contains 20 letters. The use of \"and\" when writing out numbers is in compliance with British usage."; } }
        public EulerPlugin() { }

        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "1000";

            while (lngLimit < 1)
            {
                Helpers.InputBox.Show(Name, "Enter end number", ref strLimit);
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
            context.strResultLongText = CountLettersInNumberWords(_limit);
            return context;
        }
        private string CountLettersInNumberWords(long limit) {
            int total = 0;
            string s = string.Empty;
            for (int i = 1; i <= limit; i++)
            {
                s = i.ConvertToWord();
                s = s.Replace(" ", string.Empty);
                s = s.Replace("-", string.Empty);
                total += s.Length;
                //System.Diagnostics.Debug.WriteLine(string.Format("{0,4}\t{1,2}\t{2}",i,s.Length, i.ConvertToWord()));
            }

            return string.Format("The number of letters in the sequence from 1 to {0} is {1}.", limit, total);

        }
        

    }
}
