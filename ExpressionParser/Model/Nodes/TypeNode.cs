using System;
using System.Linq.Expressions;
using ExpressionParser.Extensions;

namespace ExpressionParser.Model.Nodes
{
	internal class TypeNode : IdentifierNode
	{
		private readonly Type type;

		internal TypeNode(Type type) : base(type.FullName, 99)
		{
			this.type = type;
		}

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			return Expression.Convert(Expression.Constant(type.GetDefaultValue()), type);
		}
	}
}