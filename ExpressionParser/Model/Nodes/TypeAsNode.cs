using System.Linq.Expressions;
using ExpressionParser.Extensions;

namespace ExpressionParser.Model.Nodes
{
	internal class TypeAsNode : BinaryNode
	{
		internal TypeAsNode() : base(6) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			var rightType = Right.BuildExpression(callerExpression).Type;
			if (!rightType.IsNullable()) rightType = rightType.MakeNullableType();
			return Expression.TypeAs(Left.BuildExpression(callerExpression), rightType);
		}
	}
}