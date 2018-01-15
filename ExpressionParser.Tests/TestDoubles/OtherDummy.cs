using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ExpressionParser.Tests.TestDoubles
{
	[ExcludeFromCodeCoverage]
	internal class OtherDummy
	{
		public bool TrueProperty { get; set; } = true;
		public int IntProperty { get; set; } = 7;
		public string StringProperty { get; set; } = "Nurse!";
		public ICollection<DummyRelation> ManyNavigation { get; } = new List<DummyRelation>();
	}
}