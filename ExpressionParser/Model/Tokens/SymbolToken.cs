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
			Node node = null;
			switch (Symbol)
			{
				case "-" when context.Previous == null || context.Previous is SymbolToken: node = new NegateNode(); break;
				case "+" when context.Previous == null || context.Previous is SymbolToken: node = new ValueNode(); break;
				case "!": node = new NotNode(); break;
				case "==": node = new EqualNode(); break;
				case "=>": node = new LambdaNode(); break;
				case "!=": node = new NotEqualNode(); break;
				case ">=": node = new GreaterOrEqualNode(); break;
				case ">":  node = new GreaterNode(); break;
				case "<=": node = new LesserOrEqualNode(); break;
				case "<":  node = new LesserNode(); break;
				case "&&": node = new AndNode(); break;
				case "||": node = new OrNode(); break;
				case "??": node = new CoalesceNode(); break;
				case "?.": node = new NullPropagationNode(); break;
				case "+": node = new AddNode(); break;
				case "-": node = new SubtractNode(); break;
				case "*": node = new MultiplyNode(); break;
				case "/": node = new DivideNode(); break;
				case "%": node = new ModuloNode(); break;
				case ".": node = new DotNode(); break;
			}
			return node;
		}

		internal override bool StartsIndex => Symbol == "[";
		internal override bool EndsIndex => Symbol == "]";
		internal override bool StartsExpressionOrParameters => Symbol == "(";
		internal override bool IsParameterSeparator => Symbol == ",";
		internal override bool EndsExpressionOrParameters => Symbol == ")";
	}
}