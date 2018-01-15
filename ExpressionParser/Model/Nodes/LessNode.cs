using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class LesserNode : BinaryNode
	{
		internal LesserNode() : base(6) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			return Expression.LessThan(Left.BuildExpression(callerExpression), Right.BuildExpression(callerExpression));
		}
	}
}