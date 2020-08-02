using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHPBB_Image_Pak_Creator
{
    internal static class Extensions
    {
        public static string Capitalise(this string input) =>
            $"{input[0].ToString().ToUpper()}{input.Substring(1)}";
    }
}
