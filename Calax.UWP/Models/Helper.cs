using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calax.UWP.Models
{
    public static class Helper
    {
        public static bool EnumerableEquals<T>(IEnumerable<T> a, IEnumerable<T> b)
        {
            var cond = a == null || b == null || a.Count() != b.Count();
            if (cond) return false;
            var init = a.Select(v => b.Contains(v));
            var fin = !init.Contains(false);
            return  cond ? false : !(fin);
        }
    }
}
