namespace EmailReplyParser.Lib.LineParsers
{
    /// <summary>
    /// Defines methods that determine the kind of line in an email message.
    /// </summary>
    public interface ILineParser
    {
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
        bool IsQuote(string reversedLine);

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
        bool IsQuoteHeader(string reversedLine);

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
        bool IsSignature(string reversedLine);

        /// <summary>
        /// Determines whether the specified line precedes an implicitly quoted message.
        /// </summary>
        /// <param name="reversedLine">
        /// A string that contains a line of reversed text to check.
        /// </param>
        /// <returns>
        /// <c>true</c> if <paramref name="reversedLine"/> indicates it is
        /// followed by an implicitly quoted message; otherwise, <c>false</c>.
        /// </returns>
        bool IsImplicitQuoteHeader(string reversedLine);
    }
}