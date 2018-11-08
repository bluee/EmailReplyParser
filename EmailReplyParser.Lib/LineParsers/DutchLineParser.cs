using System.Text.RegularExpressions;

namespace EmailReplyParser.Lib.LineParsers
{
    /// <summary>
    /// Represents a line parser that uses Dutch email conventions for signatures
    /// and quoted text.
    /// </summary>
    public class DutchLineParser : DefaultLineParser
    {
        private static readonly Regex _signatureRegex = new Regex(@"(^\.?([\w-]+\s*){1,3} njim fanav druutsreV$)|(^\.?([\w-]+\s*){1,3} njim fanav nednozreV$)");
        private static readonly Regex _quoteHeaderRegex = new Regex(@"^:(.*)\s+feerhcs\s+(.*)\s+pO\s*>?$");
        private static readonly Regex _implicitQuoteRegex = new Regex(@"(\s:prewrednO(.*\sCC)?.*\s+:naA.*\s+:nednozreV.*\s+:naV$)");

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
        /// Matches any line that starts with --, __, -asdf or "Verzonden vanaf mijn".
        /// </remarks>
        public override bool IsSignature(string reversedLine)
        {
            return _signatureRegex.IsMatch(reversedLine);
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
        /// Matches any line in the Gmail-style "Op [date] schreef [person]:".
        /// </remarks>
        public override bool IsQuoteHeader(string reversedLine)
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
        public override bool IsImplicitQuoteHeader(string reversedLine)
        {
            return _implicitQuoteRegex.IsMatch(reversedLine);
        }
    }
}