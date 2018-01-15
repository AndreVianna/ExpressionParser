using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class AndNode : BinaryNode
	{
		internal AndNode() : base(11) { }
		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			return Expression.And(Left.BuildExpression(callerExpression), Right.BuildExpression(callerExpression));
		}
	}
}