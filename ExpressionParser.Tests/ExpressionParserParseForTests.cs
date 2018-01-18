using System.Diagnostics.CodeAnalysis;
using ExpressionParser.Tests.TestDoubles;
using NUnit.Framework;

namespace ExpressionParser.Tests
{
	[TestFixture]
	[ExcludeFromCodeCoverage]
	public class ExpressionParserParseForTests
	{
		private SomeDummy dummy;
		private OtherDummy other1;
		private OtherDummy other2;
		private OtherDummy other3;
		private SomeOther dummyOther1;
		private SomeOther dummyOther2;
		private SomeOther dummyOther3;

		[SetUp]
		public void Setup()
		{
			dummy = new SomeDummy();
			other1 = new OtherDummy {StringProperty = "A"};
			other2 = new OtherDummy {StringProperty = "B"};
			other3 = new OtherDummy {StringProperty = "A"};
			dummyOther1 = new SomeOther { Some = dummy, Other = other1 };
			dummyOther2 = new SomeOther { Some = dummy, Other = other2 };
			dummyOther3 = new SomeOther { Some = dummy, Other = other3 };
			dummy.ManyNavigation.Add(dummyOther1);
			dummy.ManyNavigation.Add(dummyOther2);
			dummy.ManyNavigation.Add(dummyOther3);
			other1.ManyNavigation.Add(dummyOther1);
			other2.ManyNavigation.Add(dummyOther2);
			other3.ManyNavigation.Add(dummyOther3);
		}

		[TestCase("!TrueProperty", ExpectedResult = false)]
		[TestCase("!!TrueProperty", ExpectedResult = true)]
		[TestCase("TrueProperty && TrueProperty", ExpectedResult = true)]
		[TestCase("TrueProperty && FalseProperty", ExpectedResult = false)]
		[TestCase("FalseProperty && TrueProperty", ExpectedResult = false)]
		[TestCase("FalseProperty && FalseProperty", ExpectedResult = false)]
		[TestCase("!TrueProperty && TrueProperty", ExpectedResult = false)]
		[TestCase("TrueProperty && !TrueProperty", ExpectedResult = false)]
		[TestCase("TrueProperty || TrueProperty", ExpectedResult = true)]
		[TestCase("TrueProperty || FalseProperty", ExpectedResult = true)]
		[TestCase("FalseProperty || TrueProperty", ExpectedResult = true)]
		[TestCase("FalseProperty || FalseProperty", ExpectedResult = false)]
		[TestCase("!TrueProperty || TrueProperty", ExpectedResult = true)]
		[TestCase("!TrueProperty || FalseProperty", ExpectedResult = false)]
		[TestCase("TrueProperty || !FalseProperty", ExpectedResult = true)]
		[TestCase("!FalseProperty || TrueProperty", ExpectedResult = true)]
		[TestCase("!FalseProperty || FalseProperty", ExpectedResult = true)]
		[TestCase("(FalseProperty || TrueProperty) && FalseProperty", ExpectedResult = false)]
		[TestCase("FalseProperty || ((TrueProperty && FalseProperty) || TrueProperty)", ExpectedResult = true)]
		[TestCase("FalseProperty || (!(TrueProperty && FalseProperty) && TrueProperty)", ExpectedResult = true)]
		public object ExpressionParser_ParseFor_WithValid_LogicalOperators_ShouldPass(string input)
		{
			var result = ExpressionParser.ParseFor<SomeDummy, bool>(input);
			return result(dummy);
		}

		[Test]
		public void ExpressionParser_ParseFor_WithParameterName_ShouldPass()
		{
			var result = ExpressionParser.ParseFor<SomeDummy>("FalseProperty || (!(TrueProperty && FalseProperty) && TrueProperty)", "p");
			Assert.That(result.DynamicInvoke(dummy), Is.True);
		}

		[Test]
		public void ExpressionParser_ParseFor_WithParameterName_AndReturnType_ShouldPass()
		{
			var result = ExpressionParser.ParseFor<SomeDummy, bool>("FalseProperty || (!(TrueProperty && FalseProperty) && TrueProperty)", "p");
			Assert.That(result(dummy), Is.True);
		}

		[TestCase("FalseProperty == false", ExpectedResult = true)]
		[TestCase("FalseProperty == true", ExpectedResult = false)]
		[TestCase("FalseProperty != true", ExpectedResult = true)]
		[TestCase("FalseProperty && TrueProperty == false", ExpectedResult = false)]
		[TestCase("FalseProperty && (TrueProperty == false)", ExpectedResult = false)]
		[TestCase("(FalseProperty && TrueProperty) == false", ExpectedResult = true)]
		[TestCase("FalseProperty == TrueProperty && false", ExpectedResult = false)]
		[TestCase("(FalseProperty == TrueProperty) && false", ExpectedResult = false)]
		[TestCase("FalseProperty == (TrueProperty && false)", ExpectedResult = true)]
		[TestCase("(false == (true && false)) == (true && false)", ExpectedResult = false)]
		[TestCase("false == false && false == false && false == false", ExpectedResult = true)]
		[TestCase("false == (false && false) == false && false == false", ExpectedResult = false)]
		public object ExpressionParser_ParseFor_WithValid_ComparissionOperators_ShouldPass(string input)
		{
			var result = ExpressionParser.ParseFor<SomeDummy, bool>(input);
			return result(dummy);
		}

		[TestCase("StringProperty ?? \"ABC\"", ExpectedResult = "Hello")]
		[TestCase("NullProperty ?? \"ABC\"", ExpectedResult = "ABC")]
		[TestCase("NullProperty?.Length ?? 4", ExpectedResult = 4)]
		public object ExpressionParser_ParseFor_WithValid_CoalesceForProperty_ShouldPass(string input)
		{
			var result = ExpressionParser.ParseFor<SomeDummy>(input);
			return result.DynamicInvoke(dummy);
		}

		[TestCase("TrueProperty", ExpectedResult = true)]
		[TestCase("FalseProperty", ExpectedResult = false)]
		[TestCase("IntProperty", ExpectedResult = 42)]
		[TestCase("DecimalProperty", ExpectedResult = 6.283184)]
		[TestCase("SingleNavigation.TrueProperty", ExpectedResult = true)]
		[TestCase("SingleNavigation.IntProperty", ExpectedResult = 7)]
		public object ExpressionParser_ParseFor_WithValid_PropertyReferences_ShouldPass(string input)
		{
			var result = ExpressionParser.ParseFor<SomeDummy>(input);
			return result.DynamicInvoke(dummy);
		}

		[TestCase("IntProperty.ToString()", ExpectedResult = "42")]
		[TestCase("StringProperty.StartsWith(\"Hell\")", ExpectedResult = true)]
		[TestCase("ArrayProperty.Any()", ExpectedResult = true)]
		[TestCase("StringProperty.Substring(2)", ExpectedResult = "llo")]
		[TestCase("StringProperty.Substring(2, 2)", ExpectedResult = "ll")]
		[TestCase("IntProperty.ToString().Count()", ExpectedResult = 2)]
		public object ExpressionParser_WithValid_MethodCalls_ShouldPass(string input)
		{
			var result = ExpressionParser.ParseFor<SomeDummy>(input);
			return result.DynamicInvoke(dummy);
		}

		[TestCase("ArrayProperty[1]", ExpectedResult = 2)]
		[TestCase("ArrayProperty[1+2]", ExpectedResult = 4)]
		public object ExpressionParser_ParseFor_WithValid_ArrayIndex_ShouldPass(string input)
		{
			var result = ExpressionParser.ParseFor<SomeDummy, int>(input);
			return result(dummy);
		}

		[TestCase("ArrayProperty.Any(i => i % 2 == 0)", ExpectedResult = true)]
		[TestCase("ArrayProperty.Where(i => i % 2 == 0)", ExpectedResult = new [] { 2, 4 })]
		[TestCase("ManyNavigation.Any(i => i.Other.StringProperty == \"A\")", ExpectedResult = true)]
		[TestCase("ManyNavigation.Any(i => i.Other.StringProperty == \"D\")", ExpectedResult = false)]
		[TestCase("StringProperty.Where(i => i == 'l')", ExpectedResult = new[] { 'l', 'l' })]
		public object ExpressionParser_ParseFor_WithValid_LambdaExpression_ShouldPass(string input)
		{
			var result = ExpressionParser.ParseFor<SomeDummy>(input);
			return result.DynamicInvoke(dummy);
		}

		[Test]
		public void ExpressionParser_ParseFor_WithValid_LambdaExpression_WithNamedParameter_ShouldPass()
		{
			var result = ExpressionParser.ParseFor<char, bool>("i == 'l'", "i");
			Assert.That(result.DynamicInvoke('l'), Is.True);
		}
	}
}
