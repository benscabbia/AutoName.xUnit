using System;
using FluentAssertions;
using Xunit;

namespace AutoName.xUnit.Tests
{
    public class JoinWithTests
    {
        [Theory]
        [InlineData(new [] {"Join", "By", "Spaces"}, "Join By Spaces")]
        [InlineData(new [] {" Join", "By", "Spaces "}, " Join By Spaces ")]
        [InlineData(new [] {"Joinbyunderscore"}, "Joinbyunderscore")]
        [InlineData(new [] {"0Join1", "By2", "Under3", "Score4"}, "0Join1 By2 Under3 Score4")]                                                                                        
        [InlineData(new [] {""}, "")]
        [InlineData(new [] {"A B C", "D", "E", "FG"}, "A B C D E FG")]
        [InlineData(new [] {" ", " "}, "   ")]
        
        public void JoinWithSingleSpace_Should_Correctly_Handle_Various_Inputs(string[] words, string expected)
        {
            var attribute = new NamedFactAttribute();
            var actual = attribute.JoinWithSingleSpace(words);
            actual.Should().Be(expected);
        }

        [Fact]
        public void JoinWithSingleSpace_Should_Throw_Exception_When_Null()
        {
            var attribute = new NamedFactAttribute();
            attribute.Invoking(x => x.JoinWithSingleSpace(null)).Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(new [] {"Join", "By", "Spaces"}, "Join  By  Spaces")]
        [InlineData(new [] {" Join", "By", "Spaces "}, " Join  By  Spaces ")]
        [InlineData(new [] {"Joinbyunderscore"}, "Joinbyunderscore")]
        [InlineData(new [] {"0Join1", "By2", "Under3", "Score4"}, "0Join1  By2  Under3  Score4")]                                                                                        
        [InlineData(new [] {""}, "")]
        [InlineData(new [] {"A B C", "D", "E", "FG"}, "A B C  D  E  FG")]
        [InlineData(new [] {" ", " "}, "    ")]
        public void JoinWithDoubleSpace_Should_Correctly_Handle_Various_Inputs(string[] words, string expected)
        {
            var attribute = new NamedFactAttribute();
            var actual = attribute.JoinWithDoubleSpace(words);
            actual.Should().Be(expected);
        }

        [Fact]
        public void JoinWithDoubleSpace_Should_Throw_Exception_When_Null()
        {
            var attribute = new NamedFactAttribute();
            attribute.Invoking(x => x.JoinWithDoubleSpace(null)).Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(new [] {"Join", "By", "Spaces"}, "Join\tBy\tSpaces")]
        [InlineData(new [] {" Join", "By", "Spaces "}, " Join\tBy\tSpaces ")]        
        [InlineData(new [] {"Joinbyunderscore"}, "Joinbyunderscore")]
        [InlineData(new [] {"0Join1", "By2", "Under3", "Score4"}, "0Join1\tBy2\tUnder3\tScore4")]                                                                                        
        [InlineData(new [] {""}, "")]
        [InlineData(new [] {"A B C", "D", "E", "FG"}, "A B C\tD\tE\tFG")]
        [InlineData(new [] {" ", " "}, " \t ")]
        public void JoinWithTab_Should_Correctly_Handle_Various_Inputs(string[] words, string expected)
        {
            var attribute = new NamedFactAttribute();
            var actual = attribute.JoinWithTab(words);
            actual.Should().Be(expected);
        }

        [Fact]
        public void JoinWithTab_Should_Throw_Exception_When_Null()
        {
            var attribute = new NamedFactAttribute();
            attribute.Invoking(x => x.JoinWithDoubleSpace(null)).Should().Throw<ArgumentException>();
        }

    }
}