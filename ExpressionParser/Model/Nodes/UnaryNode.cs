namespace ExpressionParser.Model.Nodes
{
	internal abstract class UnaryNode : OperationNode
	{
		protected UnaryNode(int precedence) : base(precedence) { }

		internal Node Child { get; set; }

		internal override bool IsClosed => Child.IsClosed;

		internal override bool TryAddNode(Node node)
		{
			if (Child != null) return Child.TryAddNode(node);
			Child = node;
			return true;
		}
	}
}