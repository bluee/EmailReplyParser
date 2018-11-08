using System.Text.RegularExpressions;

namespace EmailReplyParser.Lib.TextNormalizers
{
    /// <summary>
    /// Processes message text for email headers of implicitly quoted messages.
    /// </summary>
    public class EmailHeaderNormalizer : ITextNormalizer
    {
        private static readonly Regex _headersRegex = new Regex(@"(From:\s+.*\nSent:\s+.*\nTo:\s+.*\n(Cc:\s.*\n)?Subject:\s+.*)|(Van:\s+.*\nVerzonden:\s+.*\nAan:\s+.*\n(CC:\s.*\n)?Onderwerp:\s+.*)");

        /// <summary>
        /// Marks message text that is preceded by common email headers so that
        /// it is recognized as quoted text.
        /// </summary>
        /// <param name="text">
        /// A string that contains the message text to process.
        /// </param>
        /// <returns>A new string that contains the processed text.</returns>
        public string Normalize(string text)
        {
            return _headersRegex.Replace(text, x => x.Value.Replace("\n", ""));
        }
    }
}