using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class NotNode : UnaryNode
	{
		internal NotNode() : base(2) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			return Expression.Not(Child.BuildExpression(callerExpression));
		}
	}
}