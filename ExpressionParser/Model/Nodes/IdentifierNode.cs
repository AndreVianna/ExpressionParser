namespace ExpressionParser.Model.Nodes
{
	internal abstract class IdentifierNode : Node
	{
		protected IdentifierNode(string name, int precedence) : base(precedence) => Name = name;

		internal string Name { get; }
	}
}