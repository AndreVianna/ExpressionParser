using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class LesserOrEqualNode : BinaryNode
	{
		internal LesserOrEqualNode() : base(6) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			return Expression.LessThanOrEqual(Left.BuildExpression(callerExpression), Right.BuildExpression(callerExpression));
		}
	}
}