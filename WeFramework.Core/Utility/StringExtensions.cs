using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WeFramework.Core.Utility
{
    /// <summary>Provides methods to manipulate strings. </summary>
    public static class StringExtensions
    {
        /// <summary>Trims a string from the start of the input string. </summary>
        /// <param name="input">The input string. </param>
        /// <param name="trimString">The string to trim. </param>
        /// <returns>The trimmed string. </returns>
        public static string TrimStart(this string input, string trimString)
        {
            var result = input;
            while (result.StartsWith(trimString))
            {
                result = result.Substring(trimString.Length);
            }
            return result;
        }

        /// <summary>Trims a string from the end of the input string. </summary>
        /// <param name="input">The input string. </param>
        /// <param name="trimString">The string to trim. </param>
        /// <returns>The trimmed string. </returns>
        public static string TrimEnd(this string input, string trimString)
        {
            var result = input;
            while (result.EndsWith(trimString))
            {
                result = result.Substring(0, result.Length - trimString.Length);
            }
            return result;
        }

        /// <summary>Trims a string from the start and end of the input string. </summary>
        /// <param name="input">The input string. </param>
        /// <param name="trimString">The string to trim. </param>
        /// <returns>The trimmed string. </returns>
        public static string Trim(this string input, string trimString)
        {
            return input.TrimStart(trimString).TrimEnd(trimString);
        }
    }
}
