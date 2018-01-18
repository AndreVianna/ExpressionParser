using System;
using System.Collections.Generic;
using ExpressionParser.Model.Nodes;
using ExpressionParser.Model.Tokens;

namespace ExpressionParser.Model
{
	internal class TokenList : List<Token>
	{
		internal Token TokenAt(int position) => (position >= 0 && position < Count) ? this[position] : null;

		internal Token Current => this[0];
		internal void MoveNext()
		{
			RemoveAt(0);
		}

		internal void RemoveTokenAt(int position)
		{
			RemoveAt(position);
		}

		public static readonly IDictionary<string, Func<Node>> SupportedOperators = new Dictionary<string, Func<Node>>
		{
			{"[+]", () => new ValueNode()},
			{"[-]", () => new NegateNode()},
			{"is", () => new TypeIsNode()},
			{"as", () => new TypeAsNode()},
			{"!=", () => new NotEqualNode()},
			{"==", () => new EqualNode()},
			{"=>", () => new LambdaNode()},
			{">=", () => new GreaterOrEqualNode()},
			{"<=", () => new LessOrEqualNode()},
			{"&&", () => new AndNode()},
			{"||", () => new OrNode()},
			{"??", () => new CoalesceNode()},
			{"?.", () => new NullPropagationNode()},
			{"!", () => new NotNode()},
			{">", () => new GreaterNode()},
			{"<", () => new LessNode()},
			{"+", () => new AddNode()},
			{"-", () => new SubtractNode()},
			{"*", () => new MultiplyNode()},
			{"/", () => new DivideNode()},
			{"%", () => new ModuloNode()},
			{".", () => new DotNode()},
			{"[", null},
			{"]", null},
			{",", null},
			{"(", null},
			{")", null}
		};
	}
}