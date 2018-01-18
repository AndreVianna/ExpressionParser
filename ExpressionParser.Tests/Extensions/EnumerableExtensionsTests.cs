using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using ExpressionParser.Extensions;
using NUnit.Framework;

namespace ExpressionParser.Tests.Extensions
{
	[TestFixture]
	[ExcludeFromCodeCoverage]
	public class EnumerableExtensionsTests
	{
		[Test]
		public void ToArray_ForValidCollection_ShouldPass()
		{
			var source = new List<int> {1, 2, 3};
			Assert.That(source.ToArray(i => i.ToString()), Is.EquivalentTo(new [] { "1", "2" , "3" }));
		}

		[Test]
		public void ToArray_ForEmptyCollection_ShouldPass()
		{
			var source = Enumerable.Empty<int>();
			Assert.That(source.ToArray(i => i.ToString()), Is.EquivalentTo(new string[] { }));
		}


		[Test]
		public void ToArray_ForNullCollection_ShouldPass()
		{
			IEnumerable<int> source = null;
			// ReSharper disable once ExpressionIsAlwaysNull
			Assert.That(source.ToArray(i => i.ToString()), Is.EquivalentTo(new string[] { }));
		}
	}
}