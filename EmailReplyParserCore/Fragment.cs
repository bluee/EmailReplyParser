using System.Collections.Generic;
using System.Linq;

using EmailReplyParserCore.Extensions;

namespace EmailReplyParserCore
{
    /// <summary>
    /// Represents a group of lines in the email sharing common attributes.
    /// </summary>
    public class Fragment
    {
        /// <summary>
        /// Gets a collection of the lines of text in the fragment.
        /// </summary>
        public List<string> Lines { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the fragment represents
        /// quoted text.
        /// </summary>
        public bool IsQuote { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the fragment represents a
        /// signature.
        /// </summary>
        public bool IsSignature { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the fragment should be part
        /// of the reply text.
        /// </summary>
        public bool IsHidden { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Fragment"/> class,
        /// specifying whether the fragment is a quote and the first line of
        /// text.
        /// </summary>
        /// <param name="isQuote">
        /// <c>true</c> if the fragment represents quoted text; otherwise,
        /// <c>false</c>.
        /// </param>
        /// <param name="firstLine">
        /// A string containing the first line of text in the fragment.
        /// </param>
        public Fragment(bool isQuote, string firstLine)
        {
            IsSignature = false;
            IsHidden = false;
            IsQuote = isQuote;

            Lines = new List<string>();

            if (firstLine != null)
            {
                Lines.Add(firstLine);
            }
        }

        /// <summary>
        /// Finalizes the fragment.
        /// </summary>
        public void Finish()
        {
            Content = string.Join("\n", Lines);
            Content = Content.Reverse();

            Lines.Clear();
        }

        /// <summary>
        /// Gets the text content of the fragment.
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// Returns a string that represents the type of fragment.
        /// </summary>
        /// <returns>A string that represents the type of fragment.</returns>
        public override string ToString()
        {
            var attributes = new[]
            {
                IsQuote ? "Quote" : "",
                IsSignature ? "Signature" : "",
                IsHidden ? "Hidden" : ""
            }.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            if (attributes.Length > 0)
                return string.Join(", ", attributes);
            return Content?.Trim().Length > 0 ? "Content" : "White space";
        }
    }
}