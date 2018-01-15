using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class DivideNode : BinaryNode
	{
		internal DivideNode() : base(3) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			return Expression.Divide(Left.BuildExpression(callerExpression), Right.BuildExpression(callerExpression));
		}
	}
}