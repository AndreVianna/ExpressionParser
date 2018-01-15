using ExpressionParser.Model.Nodes;

namespace ExpressionParser.Model.Tokens
{
	internal class NameToken : Token
	{
		private readonly string name;
		internal NameToken(string name) : base()
		{
			this.name = name;
		}

		internal override Node CreateNode(TokenList context)
		{
			return context.NextSymbol == "(" ? new MethodNode(name) : (Node)new PropertyNode(name);
		}
	}
}