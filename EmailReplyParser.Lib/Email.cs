using System.Collections.Generic;
using System.Linq;

using EmailReplyParser.Lib.Extensions;
using EmailReplyParser.Lib.LineParsers;
using EmailReplyParser.Lib.TextNormalizers;

namespace EmailReplyParser.Lib
{
    /// <summary>
    /// Email.
    /// </summary>
    public class Email
    {
        // An email is an array of fragments
        private readonly List<Fragment> _fragments = new List<Fragment>();

        private readonly IEnumerable<ILineParser> _lineParsers;
        private readonly IEnumerable<ITextNormalizer> _normalizers;

        // It points to the current Fragment. If the matched line fits, it should
        // be added to this Fragment. Otherwise, finish it and start a new
        // Fragment.
        private Fragment _currentFragment;

        // This determines if any 'visible' Fragment has been found. Once any
        // visible Fragment is found, stop looking for hidden ones.
        private bool _foundVisible;

        /// <summary>
        /// Create an email instance from a text.
        /// </summary>
        /// <param name="text">email body</param>
        public Email(string text)
            : this(text,
                new ITextNormalizer[] { new LineEndingsNormalizer(), new ReplyHeaderTextNormalizer(), new ReplyUnderscoresTextNormalizer(), new EmailHeaderNormalizer() },
                new ILineParser[] { new DefaultLineParser(), new DutchLineParser() })
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Email"/> class with the
        /// specified text.
        /// </summary>
        /// <param name="text">A string containing the email body text.</param>
        /// <param name="normalizers">
        /// A collection of normalizers for tidying up the <paramref
        /// name="text">text</paramref>
        /// </param>
        /// <param name="lineParsers">
        /// A collection of line parsers used to determine the type of a line of
        /// text in an email message.
        /// </param>
        public Email(string text, IEnumerable<ITextNormalizer> normalizers, IEnumerable<ILineParser> lineParsers)
        {
            Text = text;

            _normalizers = normalizers;
            _lineParsers = lineParsers;
        }

        /// <summary>
        /// The reply after parsing the email.
        /// </summary>
        public string Reply
        {
            get
            {
                var visibleContent = _fragments
                    .Where(fragment => !fragment.IsHidden)
                    .Select(fragment => fragment.Content);

                return string.Join("\n", visibleContent).Trim();
            }
        }

        private string Text { get; set; }

        /// <summary>
        /// Parse email.
        /// </summary>
        public void Parse()
        {
            // Splits the given text into a list of Fragments. This is roughly
            // done by reversing the text and parsing from the bottom to the top.
            // This way we can check for 'On <date>, <author> wrote:' lines above
            // quoted blocks.

            foreach (var normalizer in _normalizers)
            {
                Text = normalizer.Normalize(Text);
            }

            ParseEmail();
        }

        /// <summary>
        /// Mark a fragment as finished.
        /// </summary>
        private void FinishFragment()
        {
            // Finishing a fragment will detect any attributes (hidden,
            // signature, reply), and join each line into a string.

            if (_currentFragment != null)
            {
                _currentFragment.Finish();

                if (!_foundVisible)
                {
                    if (_currentFragment.IsQuote
                        || _currentFragment.IsSignature
                        || string.IsNullOrWhiteSpace(_currentFragment.Content))
                    {
                        _currentFragment.IsHidden = true;
                    }
                    else
                    {
                        _foundVisible = true;
                    }
                }

                _fragments.Add(_currentFragment);
            }

            _currentFragment = null;
        }

        private void ParseEmail()
        {
            // The content is reversed initially due to the way we check for
            // hidden fragments.
            Text = Text.Reverse();

            _foundVisible = false;
            _currentFragment = null;

            foreach (var line in Text.Split('\n'))
            {
                ParseEmailLine(line);
            }

            // Finish up the final fragment.
            FinishFragment();

            _currentFragment = null;

            // Now that parsing is done, reverse the order.
            _fragments.Reverse();
        }

        /// <summary>
        /// Parse a line.
        /// </summary>
        private void ParseEmailLine(string line)
        {
            // Scans the given line of text and figures out which fragment it
            // belongs to.

            line = line.TrimEnd('\n');
            if (line.IsSignature(_lineParsers))
            {
                line = line.Trim();
            }

            // Mark the current Fragment as a signature if the current line is
            // empty and the Fragment starts with a common signature indicator.
            if (_currentFragment != null
                && string.IsNullOrWhiteSpace(line)
                && _currentFragment.Lines.Last().IsSignature(_lineParsers))
            {
                _currentFragment.IsSignature = true;

                FinishFragment();
            }

            var isQuote = line.IsQuote(_lineParsers);

            // If the line matches the current fragment, add it. Note that a
            // common reply header also counts as part of the quoted Fragment,
            // even though it doesn't start with `>`.

            var isMatched = false;
            if (_currentFragment != null)
            {
                isMatched = _currentFragment.IsQuote == isQuote
                    || (_currentFragment.IsQuote && (line.IsQuoteHeader(_lineParsers) || line.IsImplicitQuoteHeader(_lineParsers) || string.IsNullOrWhiteSpace(line)));
            }

            if (!isMatched)
            {
                FinishFragment();
                _currentFragment = new Fragment(isQuote, line);
            }
            else
            {
                _currentFragment.Lines.Add(line);

                // If any fragment is preceded by typical mail header (`From`,
                // `Sent`, `To`, `Subject`), the fragment is a quote even if
                // there is no explicit `>` on every line (e.g. Outlook 2016 rich
                // text converted to plain text).
                if (line.IsImplicitQuoteHeader(_lineParsers))
                    _currentFragment.IsQuote = true;
            }
        }
    }
}