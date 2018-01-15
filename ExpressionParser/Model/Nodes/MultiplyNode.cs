using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class MultiplyNode : BinaryNode
	{
		internal MultiplyNode() : base(3) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			return Expression.Multiply(Left.BuildExpression(callerExpression), Right.BuildExpression(callerExpression));
		}
	}
}