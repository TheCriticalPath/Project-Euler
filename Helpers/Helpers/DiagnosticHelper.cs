using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public static class DiagnosticHelper
    {
        public static string Print(this List<string> l, bool output = false)
        {
            string st = string.Empty;
            foreach (string s in l)
            {
                st += s;
            }
            if (output)
                System.Diagnostics.Debug.WriteLine(st);
            return st;
        }
    }
}
