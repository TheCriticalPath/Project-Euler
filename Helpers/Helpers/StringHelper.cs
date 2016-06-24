using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public static class StringHelper
    {
        public static string JoinInt(this int[] array,  string delimiter = ",")
        {
            StringBuilder retval = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                retval.AppendFormat("{0}{1}", array[i],delimiter);
            }
            return retval.ToString(0, retval.Length - delimiter.Length);
        }
        public static List<string> Combinations<T>(List<T> set, int k) {
            List<string> result = new List<string>();
            int n = set.Count();
            CombinationsRecursive(set, "", n, k, ref result);
            return result;
        }
        private static List<string> CombinationsRecursive<T>(List<T> set, string prefix, int n, int k, ref List<string> result) {
    
            if (k == 0) {
                result.Add(prefix);
            }
            else{
                for (int i = 0; i < n; i++) {
                    string newPrefix = prefix + set[i].ToString();
                    CombinationsRecursive(set, newPrefix, n, k - 1,ref result);
                }
            }
            return result;
        }


        public static IEnumerable<IEnumerable<T>> Permutations<T>(IEnumerable<T> list, int length) {
            if (length == 1) return list.Select(t => new T[] { t });

            return Permutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        public static List<string> Permutations<T>(int r, List<T> n) {
            List<string> result = new List<string>();
            int setLength = n.Count();
            T[] prefix = new T[r];
            PermutationsRecursive(n, ref prefix,  0, setLength-1, 0, r, ref result);
            return result;
        }
        private static List<string> PermutationsRecursive<T>(List<T> n, ref T[] prefix, int s, int e, int index , int r, ref List<string> result ) {
            string str = string.Empty;
            if (index == r)
            {
                str = string.Join("", prefix);
                if (!result.Contains(str))
                {
                    result.Add(str);
                }
                
            }
            else {
                for (int i = s; i <= e && e - i + 1 >= r - index; i++) {
                    prefix[index] = n[i];
                    PermutationsRecursive(n,ref prefix, i + 1, e, index + 1, r, ref result);
                }
            }
            return result;
        }
        public static List<string> PermuteString(string s)
        {
            List<string> result = new List<string>();
            List<string> r = new List<string>();
            if (s.Length <= 1)
            {
                result.Add(s);
            }
            else {
                for (int i = 0; i < s.Length; i++)
                {
                    r = PermuteString(s.Substring(0, i) + s.Substring(i + 1));
                    foreach (string t in r.Select(x => s.Substring(i, 1) + x))
                    {
                        if (!result.Contains(t.SortWord()))
                        {
                            result.Add(t.SortWord());
                        }
                    }
                    foreach (string t in r)
                    {
                        if (!result.Contains(t.SortWord()))
                        {
                            result.Add(t.SortWord());
                        }
                    }
                }
            }
            return result;
        }
        private static IEnumerable<string> GraphemeClusters(this string s) {
            var enumerator = StringInfo.GetTextElementEnumerator(s);
            while (enumerator.MoveNext()) {
                yield return (string)enumerator.Current;
            }
        }
        public static string ReverseString(this string s) {
            return string.Join("", s.GraphemeClusters().Reverse().ToArray());
        }
        public static bool isPalindrome(this string value) {
            string left = string.Empty;
            string right = string.Empty;
            decimal len = 0;
            decimal mid = 0;
            len = value.Length;
            if (len == 1) return true;
            mid = Math.Floor(len/2);
            left = value.Substring(0, (int)mid);
            if (len % 2 == 0)
            {
                right = value.Substring((int)mid);
            }
            else {
                right = value.Substring((int)mid + 1);
            }
            return left == right.ReverseString();
        }
        public static string ReplaceAt(this string value, int index, char newchar)
        {
            if (value.Length <= index)
                return value;
            else
                return string.Concat(value.Select((c, i) => i == index ? newchar : c));
        }
        public enum SHIFTDIRECTION
        {
            SHIFTLEFT = 1,
            SHIFTRIGHT = 2,
            SHIFTLEFT_CIRCULAR = 4,
            SHIFTRIGHT_CIRCULAR = 8
        }
        public static string Shift(this string s, SHIFTDIRECTION d)
        {
            string r = string.Empty;
            if (s.Length < 2) return s;
            switch (d)
            {
                case SHIFTDIRECTION.SHIFTLEFT:
                    r = s.Remove(0, 1);
                    break;
                case SHIFTDIRECTION.SHIFTRIGHT:
                    r = s.Remove(s.Length - 1, 1);
                    break;
                case SHIFTDIRECTION.SHIFTLEFT_CIRCULAR:
                    r = string.Format("{0}{1}", s.Substring(1, s.Length - 1), s.Substring(0, 1));
                    break;
                case SHIFTDIRECTION.SHIFTRIGHT_CIRCULAR:
                    r = string.Format("{0}{1}", s.Substring(s.Length - 1, 1), s.Substring(0, s.Length - 1));
                    break;
                default:
                    break;
            }
            return r;
        }

        public static string SortWord(this string s)
        {
            string retVal = String.Concat(s.OrderBy(c => c));
            return retVal;
        }

        public static bool IsPanDigital(this string s)
        {
            bool retVal = false;
            char chr = new char();
            int index = 0;
            int origLen = s.Length;
            int i = 0;
            retVal = true;
            try
            {
                for (i = 0; i <= origLen - 1; i++)
                {
                    chr = Convert.ToChar(i.ToString());
                    index = s.IndexOf(chr);
                    if (index >= 0)
                    {
                        s = s.Remove(index, 1);
                    }
                    else
                    {
                        retVal = false;
                        break;
                    }
                }
                if (s.Length > 0) { retVal = false; }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Source);
            }
            return retVal;
        }

    }
}



/*
    */
