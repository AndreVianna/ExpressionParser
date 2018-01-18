using ExpressionParser.Model.Nodes;

namespace ExpressionParser.Model.Tokens
{
	internal class LiteralToken<T> : Token
	{
		private readonly T value;

		internal LiteralToken(T value) {
			this.value = value;
		}

		internal override Node CreateNode()
		{
			return new LiteralNode<T>(value);
		}
	}
}