using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class ValueNode : UnaryNode
	{
		internal ValueNode() : base(2) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			return Child.BuildExpression(callerExpression);
		}
	}
}