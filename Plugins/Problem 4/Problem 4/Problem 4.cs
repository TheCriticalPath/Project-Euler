using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;
using System.ComponentModel.Composition;

namespace Problem_4
{
    [Export(typeof(IEulerPlugin))]
    public class Problem_4 : IEulerPlugin
    {
        public long _limit;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 4; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }

        public string Title{ get { return "Largest palindrome product"; } }
        public string Description { get { return "A palindromic number reads the same both ways. The largest palindrome made from the product of two 2-digit numbers is 9009 = 91 × 99.  Find the largest palindrome made from the product of two 3-digit numbers."; } }

        public Problem_4() { }
        public IEulerPluginContext PerformAction(IEulerPluginContext context)
        {
            context.strResultLongText = GetLargestPalindromicProduct(_limit);
            return context;
        }

        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "3";

            while (lngLimit < 1)
            {
                Helpers.InputHelper.Show(Name, "Enter length of multiple?", ref strLimit);
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

        public string GetLargestPalindromicProduct(long limit)
        {
            String strStarter = new String('9', (int)limit);
            bool isPalindrome = false;
            int multiple1 = 0;
            int multiple2 = 0;
            int lowerlimit = 0;
            int product = 0;
            int largestProduct = 0;
            int savedM1 = 0;
            int savedM2 = 0;
            int.TryParse(strStarter, out multiple1);
            multiple2 = multiple1;
            lowerlimit = multiple1 / 2;

            for (; multiple1 >= lowerlimit; multiple1--)
            {
                for (multiple2 = multiple1; multiple2 >= lowerlimit; multiple2--)
                {
                    product = multiple1 * multiple2;
                    isPalindrome = IsPalindrome(product.ToString());
                    if(isPalindrome)
                        break;
                }

                if (isPalindrome && product > largestProduct) {
                    largestProduct = product;
                    savedM1 = multiple1;
                    savedM2 = multiple2;
                }
            }
            return string.Format("The larget palidromic product is {0}*{1} = {2}", savedM1, savedM2, largestProduct);
        }

        public bool IsPalindrome(string str)
        {
            int strLen = str.Length-1;
            int pos = 0;
            bool retVal = true;
            while (pos < Math.Floor((double)str.Length /2d) && retVal){
                if (str.Substring(pos,1) != str.Substring(strLen - pos,1)) {
                    retVal = false;
                };
                pos++;
            }

            return retVal;
        }
    }
}
