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
namespace Problem_60
{
    [Export(typeof(IEulerPlugin))]
    public class EulerPlugin : IEulerPlugin
    {
        public bool IsAsync { get { return true; } }
        public bool ImplementsGetInput { get { return true; } }

        public long _upperLimit;
        public long _lowerLimit;

        //TODO: Replace the project number, Title, and Description
        public int ID { get { return 60; } }
        public string Title { get { return "Prime Pair Sets"; } }
        public string Description
        {
            get
            {
                return @"The primes 3, 7, 109, and 673, are quite remarkable. By taking any two primes and concatenating them in any order the result will always be prime. For example, taking 7 and 109, both 7109 and 1097 are prime. The sum of these four primes, 792, represents the lowest sum for a set of four primes with this property.
Find the lowest sum for a set of five primes for which any two primes concatenate to produce another prime.";
            }
        }

        public string Name
        {
            get { return $"Problem {ID}: {Title}"; }
        }
        public EulerPlugin() { }

        private long GetLimit(string strModifier = "", string defaultLimit = "1000")
        {
            long lngLimit = 0;
            string strLimit = defaultLimit;

            while (lngLimit < 1)
            {
                Helpers.InputHelper.Show(Name, $"Enter {strModifier} Limit", ref strLimit);
                if (!Int64.TryParse(strLimit, out lngLimit))
                {
                    lngLimit = 0;
                }

            }
            return lngLimit;
        }

        //TODO: Modify for the values in this routine to meet the needs of the specific problem
        public void PerformGetInput(IEulerPluginContext context)
        {
            _upperLimit = GetLimit("Prime Group Size", "5");
            _lowerLimit = GetLimit("Number primes to check", "1000");
        }

        public Task<IEulerPluginContext> PerformActionAsync(IEulerPluginContext context)
        {
            return Task.Factory.StartNew(() =>
           {
               // need a more elegant solution.
               DateTime dtStart, dtEnd;
               dtStart = DateTime.Now;
               Task<String> s = BruteForceAsync();
               dtEnd = DateTime.Now;
               context.strResultLongText = s.Result;
               context.spnDuration = dtEnd.Subtract(dtStart);
               return context;
           });
        }

        public IEulerPluginContext PerformAction(IEulerPluginContext context)
        {
            context.strResultLongText = "";
            return context;
        }
        async Task<string> BruteForceAsync()
        {
            return BruteForce();
        }

        private string BruteForce()
        {
            string retval = string.Empty;   // return string
            List<int> primes = MathHelper.PrimeSieve((int)_lowerLimit);
            List<string> badDuples = new List<string>();
            int sum = 0;
            int tempSum = 0;
            int lastPrime = primes.Last();
            int prime = 0;
            sum = int.MaxValue;

            primes.Remove(2);
            primes.Remove(5);

            var strSample = StringHelper.Permutations(primes, 5);

            //Console.WriteLine(string.Join(",",strSample));
            foreach (var s in strSample)
            {
                tempSum = 0;
                var strDuples = StringHelper.Permutations(s, 2);
                foreach (IEnumerable<int> d in strDuples)
                {
                    if (badDuples.Contains(StringHelper.JoinInt(d.ToArray(), ";"))
                        //|| badDuples.Contains(StringHelper.JoinInt(d.Reverse().ToArray(), ";"))
                        )
                    {
                        tempSum = int.MinValue;
                        break;
                    }
                }

                if (tempSum != int.MinValue)
                {
                    foreach (IEnumerable<int> d in strDuples)
                    {

                        string str = StringHelper.JoinInt(d.ToArray(), "");
                        if (int.TryParse(str, out prime))
                        {

                            if (prime > lastPrime)
                            {
                                if (prime.IsPrime())
                                {
                                    tempSum += prime;
                                }
                                else
                                {
                                    tempSum = 0;
                                    badDuples.Add(StringHelper.JoinInt(d.ToArray(), ";"));
                                    badDuples.Add(StringHelper.JoinInt(d.Reverse().ToArray(), ";"));
                                    System.Diagnostics.Debug.WriteLine(string.Format("({0},{1})", d.ElementAt(0), d.ElementAt(1)));
                                    break;
                                }
                            }
                            else if (primes.Contains(prime))
                            {
                                tempSum += prime;
                            }
                            else
                            {
                                tempSum = 0;
                                badDuples.Add(StringHelper.JoinInt(d.ToArray(), ";"));
                                badDuples.Add(StringHelper.JoinInt(d.Reverse().ToArray(), ";"));
                                System.Diagnostics.Debug.WriteLine(string.Format("({0},{1})", d.ElementAt(0), d.ElementAt(1)));
                                break;
                            }

                        }
                    }
                    if (tempSum > 0 && tempSum < sum)
                    {
                        Console.WriteLine(string.Format("sum: {0}", tempSum));
                        sum = tempSum;
                    }
                }
            }
            return string.Format("Lowest sum found was {0} for the first {1} primes;", sum, _lowerLimit);
        }

    }
}
