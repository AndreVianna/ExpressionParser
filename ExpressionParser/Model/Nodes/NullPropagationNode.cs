using System;
using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class NullPropagationNode : BinaryNode
	{
		internal NullPropagationNode() : base(0) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			var left = Left.BuildExpression(callerExpression);
			var right = Right.BuildExpression(Left.BuildExpression(callerExpression));
			if (Nullable.GetUnderlyingType(right.Type) == null)
				right = Expression.Convert(right, typeof(Nullable<>).MakeGenericType(right.Type));

			return Expression.Condition(Expression.Equal(left, Expression.Constant(null, left.Type)), Expression.Constant(null, right.Type), right);
		}
	}
}