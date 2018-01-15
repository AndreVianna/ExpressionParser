using System.Collections.Generic;
using ExpressionParser.Model.Tokens;

namespace ExpressionParser.Model
{
	internal class TokenList : List<Token>
	{
		internal string NextSymbol => (Count > 1 && this[1] is SymbolToken token) ? token.Symbol : null;

		internal Token Previous { get; private set; }

		internal Token Current => this[0];
		internal void MoveNext()
		{
			Previous = Current;
			RemoveAt(0);
		}
	}
}