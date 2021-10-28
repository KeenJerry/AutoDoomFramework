using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDoomFramework.Common.Tools
{
    class SuffixCutter
    {
        public static string Cut(string str, string suffix)
        {
            return str.TrimEnd(suffix.ToCharArray());
        }
    }
}
