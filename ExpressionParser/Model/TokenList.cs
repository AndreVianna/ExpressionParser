using System.Collections.Generic;
using System.Linq;
using ExpressionParser.Model.Tokens;

namespace ExpressionParser.Model
{
	internal class TokenList : List<Token>
	{
		internal Token TokenAt(int position) => (position >= 0 && position < Count) ? this[position] : null;

		internal Token Previous { get; private set; }
		internal Token Current => this[0];

		internal void MoveNext()
		{
			Previous = Current;
			RemoveAt(0);
		}
		internal void RemoveTokenBeforeLast()
		{
			RemoveAt(Count - 2);
		}

	}
}