using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Console;

namespace OutlookSort
{
    

    public static class ConsoleHelper
    {
        public static string WriteCenteredLine(string text)
        {
            int spaces = (WindowWidth - text.Length) / 2;
            return new string(' ', spaces) + text;
        }
    }
}

