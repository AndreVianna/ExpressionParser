namespace ExpressionParser.Model.Nodes
{
	internal abstract class OperationNode : Node
	{
		protected OperationNode(int precedence) : base(precedence) { }
	}
}