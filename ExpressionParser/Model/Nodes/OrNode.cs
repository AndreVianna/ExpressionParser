using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class OrNode : BinaryNode
	{
		internal OrNode() : base(12) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			return Expression.Or(Left.BuildExpression(callerExpression), Right.BuildExpression(callerExpression));
		}
	}
}