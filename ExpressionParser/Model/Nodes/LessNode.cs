using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class LessNode : BinaryNode
	{
		internal LessNode() : base(6) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			return Expression.LessThan(Left.BuildExpression(callerExpression), Right.BuildExpression(callerExpression));
		}
	}
}