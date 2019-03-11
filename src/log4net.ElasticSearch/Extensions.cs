using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace log4net.MicrosoftTeams
{

    internal static class Extensions
    {
        public static string Expand(this string text)
        {
            return text != null ? Environment.ExpandEnvironmentVariables(text) : null;
        }

        public static IEnumerable<string> SplitOn(this string text, int numChars)
        {
            var SplitOnPattern = new Regex(string.Format(@"(?<line>.{{1,{0}}})([\r\n]|$)", numChars), RegexOptions.Singleline | RegexOptions.IgnoreCase);
            return SplitOnPattern.Matches(text).OfType<Match>().Select(m => m.Groups["line"].Value);
        }

        public static string FormatString(this log4net.Layout.ILayout layout, log4net.Core.LoggingEvent loggingEvent)
        {
            using (var writer = new StringWriter())
            {
                layout.Format(writer, loggingEvent);
                return writer.ToString();
            }
        }

    }
}

