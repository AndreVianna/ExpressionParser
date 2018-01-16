using System.Linq.Expressions;
using System.Reflection;

namespace ExpressionParser.Model.Nodes
{
	internal abstract class Node
	{
		protected Node(int precedence) => Precedence = precedence;

		internal int Precedence { get; set; }

		internal virtual bool IsClosed => true;

		internal abstract Expression BuildExpression(Expression callerExpression = null);

		internal void KickPrecedenceUp() => Precedence = 0;

		internal virtual bool TryAddNode(Node node) => false;
	}
}