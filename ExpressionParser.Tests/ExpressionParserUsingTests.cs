using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ExpressionParser.Tests.TestDoubles;
using NUnit.Framework;

namespace ExpressionParser.Tests
{
	[TestFixture]
	[ExcludeFromCodeCoverage]
	public class ExpressionParserUsingTests
	{
		private SomeDummy dummy;

		[SetUp]
		public void Setup()
		{
			dummy = new SomeDummy();
		}


		[Test]
		public void ExpressionParser_UsingSingleType_WithValidInput_ShouldPass()
		{
			Assert.That(ExpressionParser.Using(typeof(OtherDummy)).ParseFor<SomeDummy, bool>("((OtherDummy)SingleNavigation).TrueProperty")(dummy), Is.True);
		}

		[Test]
		public void ExpressionParser_UsingInterface_WithValidInput_ShouldPass()
		{
			Assert.That(ExpressionParser.Using(typeof(IOtherDummy)).ParseFor<SomeDummy, bool>("((IOtherDummy)SingleNavigation).TrueProperty")(dummy), Is.True);
		}

		[Test]
		public void ExpressionParser_UsingSingleTypeWithAlias_WithValidInput_ShouldPass()
		{
			Assert.That(ExpressionParser.Using(typeof(OtherDummy), "other").ParseFor<SomeDummy, bool>("((other)SingleNavigation).TrueProperty")(dummy), Is.True);
		}

		[Test]
		public void ExpressionParser_UsingTypeMap_WithValidInput_ShouldPass()
		{
			var types = new Dictionary<Type, string>
			{
				{ typeof(OtherDummy), "other" },
				{ typeof(SomeOther), "navigation" }
			};
			Assert.That(ExpressionParser.Using(types).ParseFor<SomeDummy, bool>("((other)SingleNavigation).TrueProperty")(dummy), Is.True);
		}

		[Test]
		public void ExpressionParser_UsingTypeMap_WithNullAlias_ShouldThrow()
		{
			var types = new Dictionary<Type, string>
			{
				{ typeof(OtherDummy), "other" },
				{ typeof(SomeOther), null }
			};
			Assert.That(() => ExpressionParser.Using(types).Parse<bool>("Anything")(), Throws.Exception);
		}

		[Test]
		public void ExpressionParser_UsingListOfTypes_WithValidInput_ShouldPass()
		{
			Assert.That(ExpressionParser.Using(new [] { typeof(OtherDummy), typeof(SomeOther) }).ParseFor<SomeDummy, bool>("((OtherDummy)SingleNavigation).TrueProperty")(dummy), Is.True);
		}

		[Test]
		public void ExpressionParser_UsingLWithNullAlias_ShouldThrow()
		{
		}

		[Test]
		public void ExpressionParser_UsingLWithNullArgument_ShouldThrow()
		{
			Assert.Throws<ArgumentNullException>(() => ExpressionParser.Using((Type)null).Parse<bool>("Anything")());
			Assert.Throws<ArgumentNullException>(() => ExpressionParser.Using((IEnumerable<Type>)null).Parse<bool>("Anything")());
			Assert.Throws<ArgumentNullException>(() => ExpressionParser.Using((IDictionary<Type, string>)null).Parse<bool>("Anything")());
		}
	}
}
