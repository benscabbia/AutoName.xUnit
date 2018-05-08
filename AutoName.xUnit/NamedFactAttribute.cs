using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Xunit;

namespace AutoName.xUnit
{
    [AttributeUsage(AttributeTargets.All)]
    public class NamedFactAttribute : FactAttribute
    {
        private Join _join = new Join();
        private Split _split = new Split();
        
        public string CallerMemberName { get; }
        public string CallerFilePath { get; }
        public string CallerFile { get; }
        public virtual NameIt NameIt { get; set; }
        public virtual SplitBy Splitter { get; set; }
        public virtual JoinWith Joiner { get; set; }

        public NamedFactAttribute(NameIt nameIt, SplitBy splitBy, JoinWith joinWith, [CallerMemberName] string callerName = null, [CallerFilePath] string sourceFilePath = null)
        {
            NameIt = nameIt;
            Splitter = splitBy;
            Joiner = joinWith;

            CallerMemberName = callerName;
            CallerFilePath = sourceFilePath;            
            CallerFile = Path.GetFileName(sourceFilePath);

            SetDisplayName();
        }

        public NamedFactAttribute(SplitBy splitBy, JoinWith joinWith, [CallerMemberName] string callerName = null, [CallerFilePath] string sourceFilePath = null)
        : this(NameIt.MethodName, splitBy, joinWith, callerName, sourceFilePath)
        {}


        public NamedFactAttribute([CallerMemberName] string callerName = null, [CallerFilePath] string sourceFilePath = null)
        : this(NameIt.MethodName, SplitBy.Uppercase, JoinWith.SingleSpace, callerName, sourceFilePath)
        {}

        public virtual void SetDisplayName(){
            var name = CallerMemberName;        
            var splitter = $"SplitBy{Splitter.ToString()}";
            var joiner = $"JoinWith{Joiner.ToString()}";

            var splitterMethod = LoadSplitter(splitter);
            
            var joinerMethod = LoadJoiner(joiner);
            // var splitterResult = splitterMethod(name);
            // var joinerResult = joinerMethod(splitterResult);
           
            var splitOutput = splitterMethod(name);
            var result = joinerMethod(splitOutput); 

            base.DisplayName = result;
        }

        private Func<IEnumerable<string>, string> LoadJoiner(string methodName){
            var o = new Join();
            var method = o.GetType().GetMethod(methodName);
            Func<IEnumerable<string>, string> converted = 
                (Func<IEnumerable<string>, string>)Delegate.CreateDelegate(typeof(Func<IEnumerable<string>, string>), o, method, false);            
            return converted;
        }

        private Func<string, IEnumerable<string>> LoadSplitter(string methodName)
        {
            var x = new Split();
            var method = x.GetType().GetMethod(methodName);
            Func<string, IEnumerable<string>> converted = 
                (Func<string, IEnumerable<string>>)Delegate.CreateDelegate(typeof(Func<string, IEnumerable<string>>), x, method, false);            
            return converted;
        }
    }
}
