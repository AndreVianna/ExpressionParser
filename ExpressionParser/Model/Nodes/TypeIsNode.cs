using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class TypeIsNode : BinaryNode
	{
		internal TypeIsNode() : base(6) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			return Expression.TypeIs(Left.BuildExpression(callerExpression), Right.BuildExpression(callerExpression).Type);
		}
	}
}