using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class ModuloNode : BinaryNode
	{
		internal ModuloNode() : base(3) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			return Expression.Modulo(Left.BuildExpression(callerExpression), Right.BuildExpression(callerExpression));
		}
	}
}