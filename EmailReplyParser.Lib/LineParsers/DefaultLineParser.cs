using System.Text.RegularExpressions;

namespace EmailReplyParser.Lib.LineParsers
{
    /// <summary>
    /// Represents a line parser that uses English email conventions for
    /// determining line types.
    /// </summary>
    public class DefaultLineParser : ILineParser
    {
        private static readonly Regex _signatureRegex = new Regex(@"(?m)(--\s*$|__\s*$|\w-$)|(^(\w+\s*){1,3} ym morf tneS$)");
        private static readonly Regex _quoteRegex = new Regex("(>+)$");
        private static readonly Regex _quoteHeaderRegex = new Regex(@"^:etorw.*nO\s*(>{1})?$");

        /// <summary>
        /// Determines whether the specified line is a common signature.
        /// </summary>
        /// <param name="reversedLine">
        /// A string that contains a line of reversed text to check.
        /// </param>
        /// <returns>
        /// <c>true</c> if <paramref name="reversedLine"/> is a signature
        /// message; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsSignature(string reversedLine)
        {
            return _signatureRegex.Matches(reversedLine).Count > 0;
        }

        /// <summary>
        /// Determines whether the specified line is a quoted text.
        /// </summary>
        /// <param name="reversedLine">
        /// A string that contains a line of reversed text to check.
        /// </param>
        /// <returns>
        /// <c>true</c> if <paramref name="reversedLine"/> is quoted text;
        /// otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsQuote(string reversedLine)
        {
            return _quoteRegex.Matches(reversedLine).Count > 0;
        }

        /// <summary>
        /// Determines whether the specified line precedes quoted text.
        /// </summary>
        /// <param name="reversedLine">
        /// A string that contains a line of reversed text to check.
        /// </param>
        /// <returns>
        /// <c>true</c> if <paramref name="reversedLine"/> indicates it is
        /// followed by quoted text; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsQuoteHeader(string reversedLine)
        {
            return _quoteHeaderRegex.Matches(reversedLine).Count > 0;
        }
    }
}