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
namespace Problem_42
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public bool IsAsync { get { return true; } }
        public long _limit;
        public bool ImplementsGetInput { get { return false; } }
        public int ID { get { return 42; } } 
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Coded Triangle Numbers", ID); } }
        public string Description
        {
            get
            {
                return @"The nth term of the sequence of triangle numbers is given by, tn = ½n(n+1); so the first ten triangle numbers are:

1, 3, 6, 10, 15, 21, 28, 36, 45, 55, ...

By converting each letter in a word to a number corresponding to its alphabetical position and adding these values we form a word value. For example, the word value for SKY is 19 + 11 + 25 = 55 = t10. If the word value is a triangle number then we shall call the word a triangle word.";
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

        public Task<IEulerPluginContext> PerformActionAsync(IEulerPluginContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                // need a more elegant solution.
                DateTime dtStart, dtEnd;
                dtStart = DateTime.Now;
                string[] Words = Helpers.FileHelper.GetFileAsStringArray("Input\\words.txt", new char[] { ',' });
                Task<String> s = BruteForceAsync(Words);
                dtEnd = DateTime.Now;
                context.strResultLongText = s.Result;
                context.spnDuration = dtEnd.Subtract(dtStart);
                return context;
            });
        }

        public IEulerPluginContext PerformAction(IEulerPluginContext context)
        {
            string[] Words = Helpers.FileHelper.GetFileAsStringArray("Input\\words.txt", new char[] { ',' });
            context.strResultLongText = BruteForce(Words);
            return context;
        }
        private string BruteForce(string[] words)
        {
            int retval = 0;
            for (int i = 0; i < words.Length; i++)
            {
                if (IsTriangle(GetWordValue(words[i]))) { retval++; }
            }
            return String.Format("There are {0} triangle words.", retval);

        }

        async Task<string> BruteForceAsync(string[] words)
        {
            int retval = 0;
            for (int i = 0; i < words.Length; i++)
            {
                if (IsTriangle(GetWordValue(words[i]))) { retval++; }
            }
            return String.Format("There are {0} triangle words.", retval);
        }

        private bool IsTriangle(double num)
        {
            bool retval = false;
            double d = -0.5 + Math.Sqrt(0.25 - 2 * (0 - num));
            if (d == Math.Floor(d)) { 
                retval =  true; 
            }
            return retval;


        }

        private double GetWordValue(string word)
        {
            char[] chrs = word.ToCharArray();
            double value = 0;
            double c_val = 0;
            for (int i = 0; i < chrs.Length; i++)
            {
                c_val = (int)Char.ToUpper(chrs[i])-(int)'A' + 1;
                if (c_val > 0){
                    value += c_val;
                }
            }
            return value;
        }

    }
}
