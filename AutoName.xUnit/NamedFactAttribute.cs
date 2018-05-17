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

		public string AbsolutePath { get; }
		public string AbsolutePathWithoutExtension { get; }
		public string NameSpace { get; }
		public string FileName { get; }
		public string FileNameWithoutExtension { get; }
		public string MethodName { get; }

		public NameIt NameIt { get; }
		public SplitBy Splitter { get; }
		public JoinWith Joiner { get; }

		public NamedFactAttribute(SplitBy splitBy, JoinWith joinWith, [CallerMemberName] string callerName = null, [CallerFilePath] string sourceFilePath = null)
		: this(NameIt.MethodName, splitBy, joinWith, callerName, sourceFilePath)
		{ }

		public NamedFactAttribute([CallerMemberName] string callerName = null, [CallerFilePath] string sourceFilePath = null)
		: this(NameIt.MethodName, SplitBy.Uppercase, JoinWith.SingleSpace, callerName, sourceFilePath)
		{ }

		public NamedFactAttribute(NameIt nameIt, SplitBy splitBy, JoinWith joinWith, [CallerMemberName] string methodName = null, [CallerFilePath] string absoluteFilePath = null)
		{
			NameIt = nameIt;
			Splitter = splitBy;
			Joiner = joinWith;

			AbsolutePath = absoluteFilePath;
			AbsolutePathWithoutExtension = GetCallerFilePathWithoutFileExtension();
			NameSpace = GetNameSpace();
			FileName = Path.GetFileName(absoluteFilePath);
			FileNameWithoutExtension = Path.GetFileNameWithoutExtension(absoluteFilePath);
			MethodName = methodName;

			SetDisplayName();
		}

		public virtual void SetDisplayName()
		{
			var splitters = GetSplitters();
			var joiner = GetJoiner();
			var splitterMethods = LoadSplitters(splitters);
			var joinerMethod = LoadJoiner(joiner);

			string result = ResolveName(splitterMethods, joinerMethod);

			base.DisplayName = result;
		}

		private string ResolveName(
			IEnumerable<Func<string, IEnumerable<string>>> splitterMethods,
			Func<IEnumerable<string>, string> joinerMethod)
		{
			var result = string.Empty;
			foreach (var splitterMethod in splitterMethods)
			{
				result = joinerMethod(splitterMethod(GetName()));
			}

			return result;
		}

		private string GetNameSpace()
		{
			var pathsArray = AbsolutePath.Split(Path.DirectorySeparatorChar);
			return pathsArray[pathsArray.Length - 2];
		}

		private string GetCallerFilePathWithoutFileExtension()
		{
			return AbsolutePath.Replace(".cs", "");
		}

		private string GetName()
		{
			string result = string.Empty;

			foreach (NameIt name in Enum.GetValues(typeof(NameIt)))
			{
				if (NameIt.HasFlag(name))
				{
					result += GetProperty<string>(name.ToString());
				};
			}

			return result;
		}

		private T GetProperty<T>(string name)
		{
			return (T)this.GetType().GetProperty(name).GetValue(this, null);
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

		private Func<IEnumerable<string>, string> LoadJoiner(string methodName)
		{
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

			foreach (var methodName in methodNames)
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
