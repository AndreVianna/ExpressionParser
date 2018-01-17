using ExpressionParser.Model.Nodes;

namespace ExpressionParser.Model.Tokens
{
	internal class NameToken : Token
	{
		private readonly string name;

		internal NameToken(string name, string nodeType) : base()
		{
			this.name = name;
			NodeType = nodeType;
		}
		public string NodeType { get; set; }

		internal override Node CreateNode(TokenList context)
		{
			if (NodeType == "Method") return new MethodNode(name);
			return new PropertyNode(name);
		}
	}
}