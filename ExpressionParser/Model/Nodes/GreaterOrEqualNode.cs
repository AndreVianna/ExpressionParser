using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class GreaterOrEqualNode : BinaryNode
	{
		internal GreaterOrEqualNode() : base(6) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			return Expression.GreaterThanOrEqual(Left.BuildExpression(callerExpression), Right.BuildExpression(callerExpression));
		}
	}
}