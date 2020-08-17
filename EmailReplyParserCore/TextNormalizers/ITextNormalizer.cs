namespace EmailReplyParserCore.TextNormalizers
{
    /// <summary>
    /// Defines a method for preprocessing message text before it is parsed into
    /// fragments.
    /// </summary>
    public interface ITextNormalizer
    {
        /// <summary>
        /// Performs any preprocessing on the specified message text.
        /// </summary>
        /// <param name="text">
        /// A string that contains the message text to process.
        /// </param>
        /// <returns>A new string that contains the processed text.</returns>
        string Normalize(string text);
    }
}