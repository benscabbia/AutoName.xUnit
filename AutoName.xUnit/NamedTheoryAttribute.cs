using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit.Sdk;

namespace AutoName.xUnit
{
	[XunitTestCaseDiscoverer("Xunit.Sdk.TheoryDiscoverer", "xunit.execution.{Platform}")]
	public class NamedTheoryAttribute : NamedFactAttribute
	{
		public NamedTheoryAttribute(NameIt nameIt, SplitBy splitBy, JoinWith joinWith, [CallerMemberName] string callerName = null, [CallerFilePath] string sourceFilePath = null)
		: base(nameIt, splitBy, joinWith, callerName, sourceFilePath)
		{
		}

		public NamedTheoryAttribute(SplitBy splitBy, JoinWith joinWith, [CallerMemberName] string callerName = null, [CallerFilePath] string sourceFilePath = null)
		: this(NameIt.MethodName, splitBy, joinWith, callerName, sourceFilePath)
		{ }

		public NamedTheoryAttribute([CallerMemberName] string callerName = null, [CallerFilePath] string sourceFilePath = null)
		: this(NameIt.MethodName, SplitBy.Uppercase, JoinWith.SingleSpace, callerName, sourceFilePath)
		{ }
	}
}
