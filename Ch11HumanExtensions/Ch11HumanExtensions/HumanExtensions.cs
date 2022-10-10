using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingExtensions
{
    public static class ExtendaAHuman
    {
        public static bool IsDistressCall(this string s)
        {
            return s.Contains("Help");
        }
    }
}
