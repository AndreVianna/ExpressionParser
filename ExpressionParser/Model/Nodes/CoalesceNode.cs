using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class CoalesceNode : BinaryNode
	{
		internal CoalesceNode() : base(13) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			var left = Left.BuildExpression(callerExpression);
			var right = Right.BuildExpression(callerExpression);
			if (left is ConstantExpression leftValue && leftValue.Value == null) left = Expression.Convert(left, right.Type);
			return Expression.Coalesce(left, right);
		}
	}
}