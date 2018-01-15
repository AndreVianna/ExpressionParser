namespace ExpressionParser.Model.Nodes
{
	internal abstract class BinaryNode : OperationNode
	{
		protected BinaryNode(int precedence) : base(precedence) { }

		internal Node Left { get; set; }
		internal Node Right { get; set; }

		internal override bool IsClosed => (Left?.IsClosed ?? false) && Right.IsClosed;

		internal override bool TryAddNode(Node node)
		{
			if (Right != null) return Right.TryAddNode(node);
			Right = node;
			return true;
		}
	}
}