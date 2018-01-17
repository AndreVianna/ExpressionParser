using NUnit.Framework;

namespace ExpressionParser.Tests
{
	[TestFixture]
	public partial class ExpressionParserTests
	{
		[TestCase("null", ExpectedResult = null)]
		[TestCase("true", ExpectedResult = true)]
		[TestCase("false", ExpectedResult = false)]
		[TestCase("10", ExpectedResult = 10)]
		[TestCase("-7", ExpectedResult = -7)]
		[TestCase("+101", ExpectedResult = 101)]
		[TestCase("3.141592", ExpectedResult = 3.141592)]
		[TestCase("\"Hello Nurse!\"", ExpectedResult = "Hello Nurse!")]
		[TestCase("\"\tSome\tTabs.\"", ExpectedResult = "\tSome\tTabs.")]
		[TestCase("'\\t'", ExpectedResult = '\t')]
		[TestCase("'\\r'", ExpectedResult = '\r')]
		[TestCase("'\\n'", ExpectedResult = '\n')]
		[TestCase("'\\''", ExpectedResult = '\'')]
		[TestCase("'\\\\'", ExpectedResult = '\\')]
		[TestCase("'\"'", ExpectedResult = '"')]
		[TestCase("'a'", ExpectedResult = 'a')]
		public object ExpressionParser_Parse_WithValid_LiteralValues_ShouldPass(string input)
		{
			var result = ExpressionParser.Parse(input);
			return result.DynamicInvoke();
		}

		[TestCase("\"Hello\" ?? \"ABC\"", ExpectedResult = "Hello")]
		[TestCase("null ?? \"ABC\"", ExpectedResult = "ABC")]
		public object ExpressionParser_Parse_WithValid_CoalesceForLiteral_ShouldPass(string input)
		{
			var result = ExpressionParser.Parse<string>(input);
			return result();
		}

		[TestCase("2 + 3", ExpectedResult = 5)]
		[TestCase("2 - 3", ExpectedResult = -1)]
		[TestCase("5 - 4 + 7", ExpectedResult = 8)]
		[TestCase("2 * 3", ExpectedResult = 6)]
		[TestCase("2 / 5", ExpectedResult = 0)]
		[TestCase("2.0 / 5.0", ExpectedResult = 0.4)]
		[TestCase("3 + 2 * 3", ExpectedResult = 9)]
		[TestCase("(3 + 2) * 3", ExpectedResult = 15)]
		[TestCase("37 % 3", ExpectedResult = 1)]
		public object ExpressionParser_Parse_WithValid_MathExpression_ShouldPass(string input)
		{
			var result = ExpressionParser.Parse(input);
			return result.DynamicInvoke();
		}

		[TestCase("0 < 1", ExpectedResult = true)]
		[TestCase("0 <= 1", ExpectedResult = true)]
		[TestCase("1 <= 1", ExpectedResult = true)]
		[TestCase("2 > 1", ExpectedResult = true)]
		[TestCase("1 >= 1", ExpectedResult = true)]
		[TestCase("2 >= 1", ExpectedResult = true)]
		[TestCase("1 < 0", ExpectedResult = false)]
		[TestCase("1 < 1", ExpectedResult = false)]
		[TestCase("1 <= 0", ExpectedResult = false)]
		[TestCase("1 > 2", ExpectedResult = false)]
		[TestCase("1 > 1", ExpectedResult = false)]
		[TestCase("1 >= 2", ExpectedResult = false)]
		public object ExpressionParser_Parse_WithValid_NumericComparisson_ShouldPass(string input)
		{
			var result = ExpressionParser.Parse<bool>(input);
			return result();
		}

		[TestCase("(int)1", ExpectedResult = 1)]
		[TestCase("(string)\"Hi\"", ExpectedResult = "Hi")]
		[TestCase("(decimal)3", ExpectedResult = 3.0)]
		[TestCase("(decimal)3 + 6.0", ExpectedResult = 9.0)]
		[TestCase("(int)5.0 / (int)3.0", ExpectedResult = 1)]
		[TestCase("1 is int", ExpectedResult = true)]
		[TestCase("\"Hi\" is string", ExpectedResult = true)]
		[TestCase("\"Hi\" is decimal", ExpectedResult = false)]
		[TestCase("\"Hi\" as decimal", ExpectedResult = null)]
		[TestCase("\"Hi\" as string", ExpectedResult = "Hi")]
		[TestCase("3.0 as decimal", ExpectedResult = 3.0)]
		public object ExpressionParser_Parse_WithValid_TypeOperations_ShouldPass(string input)
		{
			var result = ExpressionParser.Parse(input);
			return result.DynamicInvoke();
		}

		[TestCase("abc")]
		[TestCase("abc()")]
		[TestCase("%&%&")]
		[TestCase("true false")]
		[TestCase("3 ?? 4")]
		[TestCase("'ab'")]
		[TestCase("t?b:c")]
		[TestCase("(int)abc")]
		[TestCase("(invalidType)abc")]
		[TestCase("abc is int")]
		[TestCase("abc as int == 3")]
		public void ExpressionParser_Parse_WithInvalidInput_ShouldThrow(string input)
		{
			Assert.That(() => ExpressionParser.Parse(input), Throws.Exception);
		}
	}
}
