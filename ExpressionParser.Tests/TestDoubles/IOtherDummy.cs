using System.Collections.Generic;

namespace ExpressionParser.Tests.TestDoubles
{
	internal interface IOtherDummy
	{
		int IntProperty { get; set; }
		ICollection<SomeOther> ManyNavigation { get; }
		string StringProperty { get; set; }
		bool TrueProperty { get; set; }
	}
}