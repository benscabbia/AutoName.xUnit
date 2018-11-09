using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutoName.xUnit
{
    public class Join
    {
        public string JoinWithSingleSpace(IEnumerable<string> words)
        {
            Guard.ArgumentIsNotNullOrWhiteSpace(words);
            var joined = string.Join(" ", words);
            return Regex.Replace(joined, @"\s+", " ");
        }

        public string JoinWithSingleTrimmedSpace(IEnumerable<string> words)
        {
            Guard.ArgumentIsNotNullOrWhiteSpace(words);
            var trimmedWords = words.Select(s => s.Trim());
            var joined = string.Join(" ", trimmedWords);
            return Regex.Replace(joined, @"\s+", " ");
        }

        public string JoinWithDoubleSpace(IEnumerable<string> words)
        {
            Guard.ArgumentIsNotNullOrWhiteSpace(words);
            var joined = string.Join("  ", words);
            return Regex.Replace(joined, @"\s+", "  ");
        }

        public string JoinWithTab(IEnumerable<string> words)
        {
            Guard.ArgumentIsNotNullOrWhiteSpace(words);
            var joined = string.Join("\t", words);
            return Regex.Replace(joined, @"\t+", "\t");
        }
    }
}
