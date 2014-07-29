using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;
using System.ComponentModel.Composition;

namespace Problem_14
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public bool IsAsync { get { return false; } }
        public long _limit;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 14; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Longest Collatz sequence: ", ID); } }
        public string Description { get { return "The following iterative sequence is defined for the set of positive integers: n → n/2 (n is even) n → 3n + 1 (n is odd) Using the rule above and starting with 13, we generate the following sequence: 13 → 40 → 20 → 10 → 5 → 16 → 8 → 4 → 2 → 1 It can be seen that this sequence (starting at 13 and finishing at 1) contains 10 terms. Although it has not been proved yet (Collatz Problem), it is thought that all starting numbers finish at 1.  Which starting number, under one million, produces the longest chain?  NOTE: Once the chain starts the terms are allowed to go above one million."; } } 
        public EulerPlugin() { }
        public async Task<IEulerPluginContext> PerformActionAsync(IEulerPluginContext context)
        {
            throw new NotImplementedException();
        }
        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "999999";

            while (lngLimit < 1)
            {
                Helpers.InputHelper.Show(Name, "Enter starting term:", ref strLimit);
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
            context.strResultLongText = GetLongestCollatzSequence(_limit);
            return context;
        }

        SortedDictionary<long,int> numbers = new SortedDictionary<long,int>();
        public String GetLongestCollatzSequence(long _limit) { 
            long number = _limit;
            int length = 0;
            int maxLength = 0;
            for (int i = 2; i < _limit; i++) {
                length = GetCollatzSequenceLength(i);
                if (length > maxLength) {
                    maxLength = length;
                    number = i;
                }
            }   
            
            return string.Format("{0} produces a Collatz sequence of length {1}", number, maxLength);
        }

        public int GetCollatzSequenceLength(long number) {
            int length = 1;
            if (number == 1)
            {
                length = 1;
            }
            else if (numbers.ContainsKey(number))
            {
                numbers.TryGetValue(number, out length);
            }
            else if (number % 2 == 0)
            {
                length += GetCollatzSequenceLength(number / 2);
                numbers.Add(number, length);

            }
            else
            {
                length += GetCollatzSequenceLength((3 * number) + 1);
                numbers.Add(number, length);

            }
            return length;
        }
    }
}
