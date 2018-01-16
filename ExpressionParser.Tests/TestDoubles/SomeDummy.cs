using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ExpressionParser.Tests.TestDoubles
{
	[ExcludeFromCodeCoverage]
	internal class SomeDummy : ISomeDummy, ISomeDummy1
	{
		public bool TrueProperty { get; set; } = true;
		public bool FalseProperty { get; set; } = false;
		public int IntProperty { get; set; } = 42;
		public decimal DecimalProperty { get; set; } = 6.283184m;
		public string StringProperty { get; set; } = "Hello";
		public string NullProperty { get; set; } = null;
		public int[] ArrayProperty { get; } = { 1, 2, 3, 4 };

		public OtherDummy SingleNavigation { get; set; } = new OtherDummy();
		public ICollection<SomeOther> ManyNavigation { get; } = new List<SomeOther>();
	}
}