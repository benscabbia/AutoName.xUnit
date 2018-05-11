using System.Collections.Generic;
using System.Linq;

namespace AutoName.xUnit
{
    public class Join
    {
        public string JoinWithSingleSpace(IEnumerable<string> words)
        {
            Guard.ArgumentIsNotNullOrWhiteSpace(words);
            return string.Join(" ", words);
        }

		public string JoinWithSingleTrimmedSpace(IEnumerable<string> words)
		{
			Guard.ArgumentIsNotNullOrWhiteSpace(words);
			IEnumerable<string> trimmedWords = words.Select(s => s.Trim());
			return string.Join(" ", trimmedWords);
		}

		public string JoinWithDoubleSpace(IEnumerable<string> words)
        {
            Guard.ArgumentIsNotNullOrWhiteSpace(words);
            return string.Join("  ", words);
        }

        public string JoinWithTab(IEnumerable<string> words)
        {
            Guard.ArgumentIsNotNullOrWhiteSpace(words);
            return string.Join("\t", words);
        }
    }
}
