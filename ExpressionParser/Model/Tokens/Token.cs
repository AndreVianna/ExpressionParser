using ExpressionParser.Model.Nodes;

namespace ExpressionParser.Model.Tokens
{
	internal abstract class Token
	{
		internal abstract Node CreateNode(TokenList context);

		internal virtual bool StartsIndex => false;
		internal virtual bool EndsIndex => false;
		internal virtual bool StartsExpressionOrParameters => false;
		internal virtual bool EndsExpressionOrParameters => false;
		internal virtual bool IsParameterSeparator => false;
	}
}