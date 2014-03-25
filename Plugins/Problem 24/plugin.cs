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
namespace Problem_24
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public long _limit;
        public string _characters;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 24; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Lexicographic permutations", ID); } }
        public string Description { get { return "What is the millionth lexicographic permutation of the digits 0, 1, 2, 3, 4, 5, 6, 7, 8 and 9?"; } }
        public EulerPlugin() { }

        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "1000000";
            while (lngLimit < 1)
            {
                Helpers.InputHelper.Show(Name, "Which position in the permutation?", ref strLimit);
                if (!Int64.TryParse(strLimit, out lngLimit))
                {
                    lngLimit = 0;
                }

            }
            return lngLimit;
        }
        private string GetCharacters()
        {
            string strLimit = "0,1,2,3,4,5,6,7,8,9";
            do
            {
                Helpers.InputHelper.Show(Name, "What are the characters in the permutation? (separate by comma)", ref strLimit);
            } while (strLimit.Length < 1);
            return strLimit;
        }
        public void PerformGetInput(IEulerPluginContext context)
        {
            _limit = GetLimit();
            _characters = GetCharacters();
        }
        public IEulerPluginContext PerformAction(IEulerPluginContext context)
        {
            List<string> Characters = new List<string>();
            Characters.AddRange(_characters.Split(','));
            context.strResultLongText = Permutations(Characters, _limit);
            return context;
        }

        public string Permutations(List<string> Characters, long limit)
        {
            string permutation = GetValueForPermutation(Characters, (int)limit);
            
            return string.Format("The permutation at postion {0} is {1}.", limit, permutation);
        }

        public string GetValueForPermutation(List<string> Values,int limit) {
            BigInteger Length = Values.Count();
            double denominator = 0D;
            int intDenominator = 0;
            string element = string.Empty;
            string permutation = string.Empty;
            double dblPosition = 0D;
            int position = 0;
            if (Length != 0)
            {
                denominator = (double)(Length - 1).ToFactorial();
                if (limit % denominator != 0)
                {
                    dblPosition = limit / denominator;
                    position = (int)Math.Floor(dblPosition);
                }
                else
                {
                    position =  (int)((limit / denominator)-1);

                }
                System.Diagnostics.Debug.WriteLine(string.Format("{0}{1}{2}^",Values.Print(false),System.Environment.NewLine,new String(' ',position))); 
                element = Values.ElementAt(position);
                Values.RemoveAt(position);
                limit  = (int)(limit - (position * denominator));
                permutation = string.Concat(element, GetValueForPermutation(Values, limit));
            }
            else {
               permutation =  string.Empty;
            }
            return permutation;
        }
        
    }
    
}
