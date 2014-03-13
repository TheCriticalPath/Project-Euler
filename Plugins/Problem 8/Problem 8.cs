using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;
using System.ComponentModel.Composition;

namespace Problem_8
{
    [Export(typeof(IEulerPlugin))]
    public class Problem_8 : IEulerPlugin
    {
        public long _limit;
        public string _numericSeries;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 8; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return "Largest product in a series"; } }
        public string Description { get { return "Find the greatest product of five consecutive digits in the 1000-digit number. \n\n7316717653133062491922511967442657474235534919493496983520312774506326239578318016984801869478851843858615607891129494954595017379583319528532088055111254069874715852386305071569329096329522744304355766896648950445244523161731856403098711121722383113622298934233803081353362766142828064444866452387493035890729629049156044077239071381051585930796086670172427121883998797908792274921901699720888093776657273330010533678812202354218097512545405947522435258490771167055601360483958644670632441572215539753697817977846174064955149290862569321978468622482839722413756570560574902614079729686524145351004748216637048440319989000889524345065854122758866688116427171479924442928230863465674813919123162824586178664583591245665294765456828489128831426076900422421902267105562632111110937054421750694165896040807198403850962455444362981230987879927244284909188845801561660979191338754992005240636899125607176060588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450 "; } }

        public Problem_8() { }
        public IEulerPluginContext PerformAction(IEulerPluginContext context)
        {
            context.strResultLongText = GetLargestProductInSeries(_limit, _numericSeries);
            return context;
        }

        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "5";

            while (lngLimit < 1)
            {
                Helpers.InputBox.Show(Name, "Enter number of consecutive digits.", ref strLimit);
                if (!Int64.TryParse(strLimit, out lngLimit))
                {
                    lngLimit = 0;
                }

            }
            return lngLimit;
        }
        public string GetSeries()
        {

            string strSeries = string.Empty;

            while (strSeries.Length <= _limit)
            {
                if (strSeries == string.Empty) { strSeries = "7316717653133062491922511967442657474235534919493496983520312774506326239578318016984801869478851843858615607891129494954595017379583319528532088055111254069874715852386305071569329096329522744304355766896648950445244523161731856403098711121722383113622298934233803081353362766142828064444866452387493035890729629049156044077239071381051585930796086670172427121883998797908792274921901699720888093776657273330010533678812202354218097512545405947522435258490771167055601360483958644670632441572215539753697817977846174064955149290862569321978468622482839722413756570560574902614079729686524145351004748216637048440319989000889524345065854122758866688116427171479924442928230863465674813919123162824586178664583591245665294765456828489128831426076900422421902267105562632111110937054421750694165896040807198403850962455444362981230987879927244284909188845801561660979191338754992005240636899125607176060588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450"; }
                Helpers.InputBox.Show(Name, string.Format("Enter numeric series longer than {0}", _limit), ref strSeries);

            }
            return strSeries;

        }
        public void PerformGetInput(IEulerPluginContext context)
        {
            _limit = GetLimit();
            _numericSeries = GetSeries();

        }

        public string GetLargestProductInSeries(long limit, string series)
        {
            string originalSeries = series;
            long product = 0;
            int number = 0;
            int pos = 0;
            string consecutiveSeries = string.Empty;
            string runningString = string.Empty;
            // I miss read the problem.  This finds the largest product of consecutive x-digit numbers.
            //while (pos < series.Length - limit)
            //{
            //    pos = series.IndexOf("0");
            //    if (pos > 0 && pos < limit)
            //    {
            //        series = series.Remove(0, (int)limit + pos);
            //    }
            //    else if (pos > series.Length - limit)
            //    {
            //        series = series.Remove(pos-((int)limit-1));
            //    }
            //    else if (pos >= limit)
            //    {
            //        series = series.Remove(pos - ((int)limit - 1), (int)(limit * 2) - 1);
            //    }
            //    else { break; }
            //}//END get rid of 0's
            pos = 0;
            while (pos < series.Length - limit)
            {
                number = 1;
                runningString = "";
                for (int i = 0; i < limit; i++)
                {
                    number *= int.Parse(series.Substring(pos+i,1));
                    runningString += series.Substring(pos + i, 1);
                }
                if (number > product)
                {
                    consecutiveSeries = series.Substring(pos, (int)limit);
                    product = number;
                }
                pos++;
            }
            return string.Format("The largest product of consecutive numbers of length {0}, is {1} with a product of {2}", new Object[] { limit, consecutiveSeries, product });
        }


    }
}
