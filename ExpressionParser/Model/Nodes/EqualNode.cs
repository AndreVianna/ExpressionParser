using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class EqualNode : BinaryNode
	{
		internal EqualNode() : base(7) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			return Expression.Equal(Left.BuildExpression(callerExpression), Right.BuildExpression(callerExpression));
		}
	}
}