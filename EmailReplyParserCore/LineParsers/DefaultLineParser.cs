using System.Text.RegularExpressions;

namespace EmailReplyParserCore.LineParsers
{
    /// <summary>
    /// Represents a line parser that uses English email conventions for
    /// signatures and quoted text.
    /// </summary>
    public class DefaultLineParser : ILineParser
    {
        private static readonly Regex _signatureRegex = new Regex(@"(?m)(--\s*$|__\s*$|\w-$)|(^(\w+\s*){1,3} ym morf tneS$)");
        private static readonly Regex _quoteRegex = new Regex("(>+)$");
        private static readonly Regex _quoteHeaderRegex = new Regex(@"^:etorw.*nO\s*(>{1})?$");
        private static readonly Regex _implicitQuoteRegex = new Regex(@"(\s:tcejbuS(.*\scC)?.*\s+:oT.*\s+:tneS.*\s+:morF$)");

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
        /// <remarks>
        /// Matches any line that starts with --, __, -asdf or "Sent from my".
        /// </remarks>
        public virtual bool IsSignature(string reversedLine)
        {
            return _signatureRegex.IsMatch(reversedLine);
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
            return _quoteRegex.IsMatch(reversedLine);
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
        /// <remarks>
        /// Matches any line in the Gmail-style "On [date], [person] wrote:".
        /// </remarks>
        public virtual bool IsQuoteHeader(string reversedLine)
        {
            return _quoteHeaderRegex.IsMatch(reversedLine);
        }

        /// <summary>
        /// Determines whether the specified line precedes an implicitly quoted
        /// message.
        /// </summary>
        /// <param name="reversedLine">
        /// A string that contains a line of reversed text to check.
        /// </param>
        /// <returns>
        /// <c>true</c> if <paramref name="reversedLine"/> indicates it is
        /// followed by an implicitly quoted message; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsImplicitQuoteHeader(string reversedLine)
        {
            return _implicitQuoteRegex.IsMatch(reversedLine);
        }
    }
}