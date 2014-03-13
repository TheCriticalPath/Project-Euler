using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Helpers
{
    public static class BindingListEntityExtension
    {
        public static BindingList<T> ToBindingList<T>(this IEnumerable<T> entities)
        {
            BindingList<T> rtn = new BindingList<T>();

            foreach (T obj in entities)
            {
                rtn.Add(obj);
            }

            return rtn;
        }
    }
}
