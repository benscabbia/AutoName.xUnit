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
    public class NamedFactAttribute : FactAttribute
    {
        private Join _join = new Join();
        private Split _split = new Split();
        
        public string CallerMemberName { get; }
        public string CallerFilePath { get; }
        public string CallerFile { get; }
        public NameIt NameIt { get; set; }
        public SplitBy Splitter { get; set; }
        public JoinWith Joiner { get; set; }

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

			var splitters = GetSplitters();
            var joiner = GetJoiner();

            var splitterMethods = LoadSplitters(splitters);            
            var joinerMethod = LoadJoiner(joiner);

			string result = name;
			foreach(var splitterMethod in splitterMethods)
			{
				result = joinerMethod(splitterMethod(result));
			}

            base.DisplayName = result;
        }

		private string GetJoiner()
		{
			return $"JoinWith{Joiner.ToString()}";
		}

		private IEnumerable<string> GetSplitters()
		{			
			List<string> result = new List<string>();
			foreach (SplitBy splitter in Enum.GetValues(typeof(SplitBy)))
			{
				if (Splitter.HasFlag(splitter))
				{
					result.Add($"SplitBy{splitter.ToString()}");
				};				
			}
			return result;
		}		

        private Func<IEnumerable<string>, string> LoadJoiner(string methodName){
            var o = new Join();
            var method = o.GetType().GetMethod(methodName);
            Func<IEnumerable<string>, string> converted = 
                (Func<IEnumerable<string>, string>)Delegate.CreateDelegate(typeof(Func<IEnumerable<string>, string>), o, method, false);            
            return converted;
        }

        private IEnumerable<Func<string, IEnumerable<string>>> LoadSplitters(IEnumerable<string> methodNames)
        {
            var x = new Split();
			var methods = new List<Func<string, IEnumerable<string>>>();

			foreach(var methodName in methodNames)
			{
				var method = x.GetType().GetMethod(methodName);
				Func<string, IEnumerable<string>> converted = 
					(Func<string, IEnumerable<string>>)Delegate.CreateDelegate(typeof(Func<string, IEnumerable<string>>), x, method, false);
				methods.Add(converted);
			}

			return methods;
        }
    }
}
