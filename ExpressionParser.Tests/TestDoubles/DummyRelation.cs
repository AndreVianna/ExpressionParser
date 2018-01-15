using System.Diagnostics.CodeAnalysis;

namespace ExpressionParser.Tests.TestDoubles
{
	[ExcludeFromCodeCoverage]
	internal class DummyRelation
	{
		public SomeDummy Some { get; set; }
		public OtherDummy Other { get; set; }
	}
}