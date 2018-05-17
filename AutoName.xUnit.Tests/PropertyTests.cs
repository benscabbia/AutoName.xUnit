using System;
using System.Reflection;
using Xunit;
using FluentAssertions;
using AutoName.xUnit;
using System.IO;

namespace AutoName.xUnit.Tests
{
    public class PropertyTests
    {
        [Fact]
        public void NameIt_Should_Have_Starting_State_Set_To_MethodName()
        {
            var attribute = new NamedFactAttribute();
            var result = attribute.NameIt;

            result.Should().Be(NameIt.MethodName);
        }

        [Fact]
        public void Splitter_Should_Have_Starting_State_Set_To_Uppercase()
        {
            var attribute = new NamedFactAttribute();
            var result = attribute.Splitter;

            result.Should().Be(SplitBy.Uppercase);
        }

        [Fact]
        public void Joiner_Should_Have_Starting_State_Set_To_SingleSpace()
        {
            var attribute = new NamedFactAttribute();
            var result = attribute.Joiner;

            result.Should().Be(JoinWith.SingleSpace);
        }

        [Fact]
        public void CallerMemberName_Should_Have_The_Correct_Value_Set_When_Using_Underscores()
        {
            var attribute = new NamedFactAttribute();
            var result = attribute.MethodName;
            
            result.Should().Be("CallerMemberName_Should_Have_The_Correct_Value_Set_When_Using_Underscores");
        }

        [Fact]
        public void CallerMemberNameShouldHaveTheCorrectValueSetWhenUsingUppercase()
        {
            var attribute = new NamedFactAttribute();
            var result = attribute.MethodName;
            
            result.Should().Be("CallerMemberNameShouldHaveTheCorrectValueSetWhenUsingUppercase");
        }

        [Fact]
        public void CallerMemberName_ShouldHaveTheCorrectValueSet_WhenUsingMixedNamed_()
        {
            var attribute = new NamedFactAttribute();
            var result = attribute.MethodName;
            
            result.Should().Be("CallerMemberName_ShouldHaveTheCorrectValueSet_WhenUsingMixedNamed_");
        }

        [Fact]
        public void CallerFileName_Should_Have_The_Correct_Value()
        {
            var attribute = new NamedFactAttribute();
            var result = attribute.FileName;
            
            result.Should().Be("PropertyTests.cs");
        }

		[Fact]
		public void AbsolutePath_Should_Have_The_Correct_Value()
		{
			var attribute = new NamedFactAttribute();
			var result = attribute.AbsolutePath;

			result.Should().Contain("AutoName.xUnit");
			result.Should().Contain("AutoName.xUnit.Tests");
			result.Should().Contain("PropertyTests.cs");
		}

		[Fact]
		public void AbsolutePathWithoutExtension_Should_Have_The_Correct_Value()
		{
			var attribute = new NamedFactAttribute();
			var result = attribute.AbsolutePathWithoutExtension;

			result.Should().Contain("AutoName.xUnit");
			result.Should().Contain("AutoName.xUnit.Tests");
			result.Should().Contain("PropertyTests");
			result.Should().NotContain("PropertyTests.cs");
		}

		[Fact]
		public void NameSpace_Should_Have_The_Correct_Value()
		{
			var attribute = new NamedFactAttribute();
			var result = attribute.NameSpace;

			result.Should().Be("AutoName.xUnit.Tests");
		}

		[Fact]
		public void FileName_Should_Have_The_Correct_Value()
		{
			var attribute = new NamedFactAttribute();
			var result = attribute.FileName;

			result.Should().Be("PropertyTests.cs");
		}

		[Fact]
		public void FileNameWithoutExtension_Should_Have_The_Correct_Value()
		{
			var attribute = new NamedFactAttribute();
			var result = attribute.FileNameWithoutExtension;

			result.Should().Be("PropertyTests");
		}
	}
}
