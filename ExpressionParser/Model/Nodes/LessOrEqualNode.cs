using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class LessOrEqualNode : BinaryNode
	{
		internal LessOrEqualNode() : base(6) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			return Expression.LessThanOrEqual(Left.BuildExpression(callerExpression), Right.BuildExpression(callerExpression));
		}
	}
}