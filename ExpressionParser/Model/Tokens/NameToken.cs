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

		internal string Type { get; set; }

		internal override Node CreateNode(TokenList context)
		{
			switch (Type)
			{
				case "Method": return new MethodNode(name);
				case "TypeCast": return new TypeCastNode(name);
				default: return new PropertyNode(name);
			}
		}
	}
}