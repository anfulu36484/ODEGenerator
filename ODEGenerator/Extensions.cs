using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODEGenerator
{
    public static class Extensions
    {
        public static bool IsEven<T>(this IEnumerable<T> array, Func<T,bool> func)
        {
            return array.Count(func)%2 == 0;
        }
    }
}
