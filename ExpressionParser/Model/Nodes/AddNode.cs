using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class AddNode : BinaryNode
	{
		internal AddNode() : base(4) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			return Expression.Add(Left.BuildExpression(callerExpression), Right.BuildExpression(callerExpression));
		}
	}
}