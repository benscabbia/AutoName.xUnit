using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutoName.xUnit
{
    public class Split
    {
        public IEnumerable<string> SplitByUppercase(string word)
        {
            Guard.ArgumentIsNotNullOrWhiteSpace(word);
            return Regex.Split(word, "(?<!^)(?=[A-Z])");
        }

        public IEnumerable<string> SplitByUnderscore(string word)
        {
            Guard.ArgumentIsNotNullOrWhiteSpace(word);
            return word.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public IEnumerable<string> SplitByNumber(string word)
        {
            Guard.ArgumentIsNotNullOrWhiteSpace(word);
            return Regex.Split(word, @"(\d+|[A-Za-z]+)").Where(s => s != String.Empty);
        }
    }
}
