using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public static class StringHelper
    {
        public static string JoinInt(this int[] array)
        {
            StringBuilder retval = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                retval.AppendFormat("{0},", array[i]);
            }
            return retval.ToString(0, retval.Length - 1);
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

        public static bool IsPanDigital(this string s)
        {
            bool retVal = false;
            char chr = new char();
            int index = 0;
            int origLen = s.Length;
            retVal = true;
            
            for (int i = 1; i <= origLen; i++)
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

            return retVal;
        }
    }
}
