using System;

namespace EmailReplyParserCore.Extensions
{
    internal static class StringExtenstions
    {
        public static string Reverse(this string s)
        {
            var charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
