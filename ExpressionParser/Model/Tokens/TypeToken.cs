using System;
using ExpressionParser.Model.Nodes;

namespace ExpressionParser.Model.Tokens
{
	internal class TypeToken : Token
	{
		private readonly Type type;

		internal TypeToken(Type type, string nodeType)
		{
			NodeType = nodeType;
			this.type = type;
		}

		public string NodeType { get; set; }

		internal override Node CreateNode()
		{
			if (NodeType == "Type") return new TypeNode(type);
			return new TypeCastNode(type);
		}
	}
}