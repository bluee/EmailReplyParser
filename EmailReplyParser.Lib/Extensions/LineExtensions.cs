using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using EmailReplyParser.Lib.LineParsers;

namespace EmailReplyParser.Lib.Extensions
{
    internal static class LineExtensions
    {
        public static bool IsSignature(this string line, IEnumerable<ILineParser> lineParsers)
        {
            if (lineParsers == null)
                throw new System.ArgumentNullException(nameof(lineParsers));

            return lineParsers.Any(x => x.IsSignature(line));
        }

        public static bool IsQuote(this string line, IEnumerable<ILineParser> lineParsers)
        {
            if (lineParsers == null)
                throw new System.ArgumentNullException(nameof(lineParsers));

            return lineParsers.Any(x => x.IsQuote(line));
        }

        public static bool IsQuoteHeader(this string line, IEnumerable<ILineParser> lineParsers)
        {
            if (lineParsers == null)
                throw new System.ArgumentNullException(nameof(lineParsers));

            return lineParsers.Any(x => x.IsQuoteHeader(line));
        }

        public static bool IsImplicitQuoteHeader(this string line, IEnumerable<ILineParser> lineParsers)
        {
            if (lineParsers == null)
                throw new System.ArgumentNullException(nameof(lineParsers));

            return lineParsers.Any(x => x.IsImplicitQuoteHeader(line));
        }
    }
}
