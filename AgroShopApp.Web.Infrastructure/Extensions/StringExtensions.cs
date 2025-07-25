using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroShopApp.Web.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string Truncate(this string input, int length = 100)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return input.Length > length ? input.Substring(0, length) + "..." : input;
        }
    }
}
