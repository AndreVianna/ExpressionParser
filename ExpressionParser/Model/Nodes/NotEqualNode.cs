using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class NotEqualNode : BinaryNode
	{
		internal NotEqualNode() : base(7) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			return Expression.NotEqual(Left.BuildExpression(callerExpression), Right.BuildExpression(callerExpression));
		}
	}
}