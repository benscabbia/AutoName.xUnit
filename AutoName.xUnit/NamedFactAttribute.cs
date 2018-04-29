using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Xunit;

namespace AutoName.xUnit
{
    //https://bitwiseguy.wordpress.com/2015/11/23/creating-readable-xunit-test-method-names-automatically/
    [AttributeUsage(AttributeTargets.All)]
    public class NamedFactAttribute : FactAttribute
    {
        public string CallerMemberName { get; }
        public string CallerFilePath { get; }
        public string CallerFile { get; }
        public virtual NameIt NameIt { get; set; } = NameIt.FileName;
        public virtual SplitBy Splitter { get; set; } = SplitBy.Uppercase;
        public virtual JoinWith Joiner { get; set; } = JoinWith.SingleSpace;

        public NamedFactAttribute(NameIt nameIt, SplitBy splitBy, JoinWith joinWith, [CallerMemberName] string callerName = null, [CallerFilePath] string sourceFilePath = null)
        : this(callerName, sourceFilePath)
        {
            NameIt = nameIt;
            Splitter = splitBy;
            Joiner = joinWith;

            SetDisplayName();
        }

        public NamedFactAttribute(SplitBy splitBy, JoinWith joinWith, [CallerMemberName] string callerName = null, [CallerFilePath] string sourceFilePath = null)
        : this(NameIt.FileName, splitBy, joinWith, callerName, sourceFilePath)
        {}

        public NamedFactAttribute([CallerMemberName] string callerName = null, [CallerFilePath] string sourceFilePath = null)
        {
            CallerMemberName = callerName;
            CallerFilePath = sourceFilePath;            
            CallerFile = Path.GetFileName(sourceFilePath);
        }

        public void SetDisplayName(){
            var name = NameIt.ToString();
            var splitter = $"SplitBy{Splitter.ToString()}";
            var joiner = $"JoinWith{Joiner.ToString()}";

            var splitterMethod = LoadMethod(splitter);
            var joinerMethod = LoadMethod(joiner);

            // these should be ran with a try catch to ensure exception is handled with default
            IEnumerable<string> splitterResult = ExecuteMethod<IEnumerable<string>>(splitterMethod, new[] { name });            
            var joinerResult =  ExecuteMethod<string>(joinerMethod, new[] { splitterResult} );

            SetDisplayName(joinerResult);

        }
        public void SetDisplayName(string word)
        {
            base.DisplayName = word;
        }
        public void SetDisplayName(string word, SplitBy splitBy, JoinWith joinWith){
            
        }

        public void SetDisplayName(string word, Func<string, string> formatter)
        {
            base.DisplayName = formatter(word);
        }

        public void SetDisplayName(string word, Func<string, string[]> splitter, Func<string[], string> joiner)
        {
            var splitString = splitter(word);
            base.DisplayName = joiner(splitString);
        }

        public IEnumerable<string> SplitByUppercase(string word)
        {
            Guard.ArgumentIsNotNullOrWhiteSpace(word);
            return Regex.Split(word, @"(?<!^)(?=[A-Z])");
        }

        public IEnumerable<string> SplitByUnderscore(string word)
        {
            Guard.ArgumentIsNotNullOrWhiteSpace(word);
            return word.Split(new [] {'_'}, StringSplitOptions.RemoveEmptyEntries);
        }

        public string JoinWithSingleSpace(string[] words)
        {
            return string.Join(" ", words);
        }   

        
        private MethodInfo LoadMethod(string methodName)
        {
            MethodInfo method = this.GetType().GetMethod(methodName);
            return method;
        }

        private T ExecuteMethod<T>(MethodInfo method, object[] @params){
            return (T)method.Invoke(this, @params);
        }     
    }
}
