using System.Collections.Generic;

namespace AutoName.xUnit
{
    public class Join
    {
        public string JoinWithSingleSpace(IEnumerable<string> words)
        {
            Guard.ArgumentIsNotNullOrWhiteSpace(words);
            return string.Join(" ", words);
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