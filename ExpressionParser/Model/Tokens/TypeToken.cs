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

		internal override Node CreateNode() => NodeType == "Type" ? (Node) new TypeNode(type) : new TypeCastNode(type);
	}
}