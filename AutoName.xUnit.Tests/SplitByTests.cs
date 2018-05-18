using Xunit;
using FluentAssertions;
using System;

namespace AutoName.xUnit.Tests
{
	public class SplitByTests
	{
		[Theory]
		[InlineData("SplitByUppercase", new[] { "Split", "By", "Uppercase" })]
		[InlineData("SplitByUpperCase", new[] { "Split", "By", "Upper", "Case" })]
		[InlineData("splitByUpperCase", new[] { "split", "By", "Upper", "Case" })]
		[InlineData("splitbyuppercase", new[] { "splitbyuppercase" })]
		[InlineData("Split_By_Upper_Case", new[] { "Split_", "By_", "Upper_", "Case" })]
		[InlineData("Split By Upper Case", new[] { "Split ", "By ", "Upper ", "Case" })]
		[InlineData("0Split1By2Upper3Case4", new[] { "0", "Split1", "By2", "Upper3", "Case4" })]
		[InlineData("split_by_upper_case", new[] { "split_by_upper_case" })]
		[InlineData("SPLITBYUPPERCASE", new[] { "S", "P", "L", "I", "T", "B", "Y", "U", "P", "P", "E", "R", "C", "A", "S", "E" })]
		public void SplitByUppercase_Should_Correctly_Handle_Various_Inputs(string word, string[] expected)
		{
			var attribute = new Split();
			var actual = attribute.SplitByUppercase(word);

			actual.Should().Equal(expected);
		}

		[Fact]
		public void SplitByUppercase_Should_Throw_Exception_When_Null()
		{
			var attribute = new Split();
			attribute.Invoking(x => x.SplitByUppercase(null)).Should().Throw<ArgumentException>();
		}

		[Theory]
		[InlineData("Split_By_Underscore", new[] { "Split", "By", "Underscore" })]
		[InlineData("Split_By_Under_Score", new[] { "Split", "By", "Under", "Score" })]
		[InlineData("split_By_Under_Score", new[] { "split", "By", "Under", "Score" })]
		[InlineData("splitbyunderscore", new[] { "splitbyunderscore" })]
		[InlineData("Split__By__Under__Score", new[] { "Split", "By", "Under", "Score" })]
		[InlineData("Split____By____Under____Score", new[] { "Split", "By", "Under", "Score" })]
		[InlineData("0Split1_By2_Under3_Score4", new[] { "0Split1", "By2", "Under3", "Score4" })]
		[InlineData("________", new string[0])]
		[InlineData("S_P_L_I_T_B_Y_U_N_D_E_R_S_C_O_R_E", new[] { "S", "P", "L", "I", "T", "B", "Y", "U", "N", "D", "E", "R", "S", "C", "O", "R", "E" })]
		public void SplitByUnderscore_Should_Correctly_Handle_Various_Inputs(string word, string[] expected)
		{
			var attribute = new Split();
			var actual = attribute.SplitByUnderscore(word);
			actual.Should().Equal(expected);
		}

		[Fact]
		public void SplitByUnderscore_Should_Throw_Exception_When_Null()
		{
			var attribute = new Split();
			attribute.Invoking(x => x.SplitByUnderscore(null)).Should().Throw<ArgumentException>();
		}
	}
}
