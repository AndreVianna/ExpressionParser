using System.Collections.Generic;

namespace ExpressionParser.Tests.TestDoubles
{
	internal interface ISomeDummy
	{
		int[] ArrayProperty { get; }
		decimal DecimalProperty { get; set; }
		bool FalseProperty { get; set; }
		int IntProperty { get; set; }
		ICollection<SomeOther> ManyNavigation { get; }
		string NullProperty { get; set; }
		OtherDummy SingleNavigation { get; set; }
		string StringProperty { get; set; }
		bool TrueProperty { get; set; }
	}
}