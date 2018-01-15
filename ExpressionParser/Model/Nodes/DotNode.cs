using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class DotNode : BinaryNode
	{
		internal DotNode() : base(0) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			return Right.BuildExpression(Left.BuildExpression(callerExpression));
		}
	}
}