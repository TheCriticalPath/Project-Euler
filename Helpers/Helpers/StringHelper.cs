using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public static class StringHelper
    {
        public static string JoinInt(this int[] array) {
            StringBuilder retval = new StringBuilder();
            for (int i = 0; i < array.Length; i++) {
                retval.AppendFormat("{0},", array[i]);
            }
            return retval.ToString(0, retval.Length - 1);
         }
    }
}
