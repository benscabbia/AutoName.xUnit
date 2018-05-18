using FluentAssertions;
using Xunit;

namespace AutoName.xUnit.Tests
{
	public class DisplayNameTests
	{
		[Fact]
		public void DisplayNameShouldHaveCorrectValueWhenUsingUppercase()
		{
			var attribute = new NamedFactAttribute();
			var result = attribute.DisplayName;

			result.Should().Be("Display Name Should Have Correct Value When Using Uppercase");
		}

		[Fact]
		public void DisplayName_Should_Have_Correct_Value_When_Using_Underscore()
		{
			var attribute = new NamedFactAttribute(SplitBy.Underscore, JoinWith.SingleSpace);

			attribute.SetDisplayName();

			var result = attribute.DisplayName;

			result.Should().Be("DisplayName Should Have Correct Value When Using Underscore");
		}

		[Fact]
		public void DisplayName_Should_Behave_Consistently_With_Invalid_Splitter()
		{
			var attribute = new NamedFactAttribute(SplitBy.Uppercase, JoinWith.SingleSpace);

			attribute.SetDisplayName();

			var result = attribute.DisplayName;

			result.Should().Be("Display Name_ Should_ Behave_ Consistently_ With_ Invalid_ Splitter");
		}

		[Fact]
		public void DisplayNameShouldBehaveConsistentlyWithInvalidSplitter()
		{
			var attribute = new NamedFactAttribute(SplitBy.Underscore, JoinWith.SingleSpace);

			attribute.SetDisplayName();

			var result = attribute.DisplayName;

			result.Should().Be("DisplayNameShouldBehaveConsistentlyWithInvalidSplitter");
		}

		[Fact]
		public void DisplayNameShouldHaveCorrectValueWhenUsingSingleSpaceJoiner()
		{
			var attribute = new NamedFactAttribute(SplitBy.Uppercase, JoinWith.SingleSpace);

			attribute.SetDisplayName();

			var result = attribute.DisplayName;

			result.Should().Be("Display Name Should Have Correct Value When Using Single Space Joiner");
		}

		[Fact]
		public void DisplayNameShouldHaveCorrectValueWhenUsingDoubleSpaceJoiner()
		{
			var attribute = new NamedFactAttribute(SplitBy.Uppercase, JoinWith.DoubleSpace);

			attribute.SetDisplayName();

			var result = attribute.DisplayName;

			result.Should().Be("Display  Name  Should  Have  Correct  Value  When  Using  Double  Space  Joiner");
		}

		[Fact]
		public void DisplayNameShouldHaveCorrectValueWhenUsingTabJoiner()
		{
			var attribute = new NamedFactAttribute(SplitBy.Uppercase, JoinWith.Tab);

			attribute.SetDisplayName();

			var result = attribute.DisplayName;

			result.Should().Be("Display\tName\tShould\tHave\tCorrect\tValue\tWhen\tUsing\tTab\tJoiner");
		}

		/*
            Test Splitter + Joiner

            
        
         */
	}
}
