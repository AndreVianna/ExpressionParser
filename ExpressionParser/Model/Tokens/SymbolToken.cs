using System;
using ExpressionParser.Model.Nodes;

namespace ExpressionParser.Model.Tokens
{
	internal class SymbolToken : Token
	{
		internal SymbolToken(string symbol) : base()
		{
			Symbol = symbol;
		}

		internal string Symbol { get; }

		internal override Node CreateNode(TokenList context)
		{
			switch (Symbol)
			{
				case "-" when context.Previous == null || context.Previous is SymbolToken: return new NegateNode();
				case "+" when context.Previous == null || context.Previous is SymbolToken: return new ValueNode();
				case "!": return new NotNode();
				case "is": return new TypeIsNode();
				case "as": return new TypeAsNode();
				case "==": return new EqualNode();
				case "=>": return new LambdaNode();
				case "!=": return new NotEqualNode();
				case ">=": return new GreaterOrEqualNode();
				case ">":  return new GreaterNode();
				case "<=": return new LesserOrEqualNode();
				case "<":  return new LesserNode();
				case "&&": return new AndNode();
				case "||": return new OrNode();
				case "??": return new CoalesceNode();
				case "?.": return new NullPropagationNode();
				case "+": return new AddNode();
				case "-": return new SubtractNode();
				case "*": return new MultiplyNode();
				case "/": return new DivideNode();
				case "%": return new ModuloNode();
				case ".": return new DotNode();
				default: throw new Exception("Unsuported token.");
			}
		}

		internal override bool StartsIndex => Symbol == "[";
		internal override bool EndsIndex => Symbol == "]";
		internal override bool StartsExpressionOrParameters => Symbol == "(";
		internal override bool IsParameterSeparator => Symbol == ",";
		internal override bool EndsExpressionOrParameters => Symbol == ")";
	}
}