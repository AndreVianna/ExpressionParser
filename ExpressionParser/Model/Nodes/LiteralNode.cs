using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal abstract class LiteralNode : Node
	{
		protected LiteralNode() : base(99) { }
	}

	internal class LiteralNode<T> : LiteralNode
	{
		internal LiteralNode(T value) => Value = value;

		internal T Value { get; }

		internal override Expression BuildExpression(Expression callerExpression = null) => Expression.Constant(Value);
	}
}