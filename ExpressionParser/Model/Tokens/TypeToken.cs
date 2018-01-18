using System;
using ExpressionParser.Model.Nodes;

namespace ExpressionParser.Model.Tokens
{
	internal class TypeToken : Token
	{
		private readonly Type type;

		internal TypeToken(Type type, string nodeType)
		{
			this.NodeType = nodeType;
			this.type = type;
		}

		public string NodeType { get; set; }

		internal override Node CreateNode(TokenList context)
		{
			if (NodeType == "Type") return new TypeNode(type);
			return new TypeCastNode(type);
		}
	}
}