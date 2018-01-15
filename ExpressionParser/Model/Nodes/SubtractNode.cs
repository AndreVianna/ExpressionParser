using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class SubtractNode : BinaryNode
	{
		internal SubtractNode() : base(4) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			return Expression.Subtract(Left.BuildExpression(callerExpression), Right.BuildExpression(callerExpression));
		}
	}
}