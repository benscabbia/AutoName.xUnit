using System;
using System.Reflection;
using Xunit;
using FluentAssertions;

namespace AutoName.xUnit.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void NameIt_Should_Have_Starting_State_Set_To_FileName()
        {
            var attribute = new NamedFactAttribute();
            var result = attribute.NameIt;

            result.Should().Be(NameIt.FileName);
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
            var result = attribute.CallerMemberName;
            
            result.Should().Be("CallerMemberName_Should_Have_The_Correct_Value_Set_When_Using_Underscores");
        }

        [Fact]
        public void CallerMemberNameShouldHaveTheCorrectValueSetWhenUsingUppercase()
        {
            var attribute = new NamedFactAttribute();
            var result = attribute.CallerMemberName;
            
            result.Should().Be("CallerMemberNameShouldHaveTheCorrectValueSetWhenUsingUppercase");
        }

        [Fact]
        public void CallerMemberName_ShouldHaveTheCorrectValueSet_WhenUsingMixedNamed_()
        {
            var attribute = new NamedFactAttribute();
            var result = attribute.CallerMemberName;
            
            result.Should().Be("CallerMemberName_ShouldHaveTheCorrectValueSet_WhenUsingMixedNamed_");
        }

        [Fact]
        public void CallerFileName_Should_Have_The_Correct_Value()
        {
            var attribute = new NamedFactAttribute();
            var result = attribute.CallerFile;
            
            result.Should().Be("PropertyTests.cs");
        }
    
    }
}
