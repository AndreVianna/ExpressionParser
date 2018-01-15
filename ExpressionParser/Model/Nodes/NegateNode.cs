using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class NegateNode : UnaryNode
	{
		internal NegateNode() : base(2) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			return Expression.Negate(Child.BuildExpression(callerExpression));
		}
	}
}