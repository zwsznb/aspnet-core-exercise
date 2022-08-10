using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zws.Core.Extensions
{
    public static class StringExtension
    {
        public static bool IsIn(this string str, params string[] ps)
        {
            if (ps.Contains(str))
            {
                return true;
            }
            return false;
        }
    }
}
