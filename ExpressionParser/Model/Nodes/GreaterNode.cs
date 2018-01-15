using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class GreaterNode : BinaryNode
	{
		internal GreaterNode() : base(6) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			return Expression.GreaterThan(Left.BuildExpression(callerExpression), Right.BuildExpression(callerExpression));
		}
	}
}