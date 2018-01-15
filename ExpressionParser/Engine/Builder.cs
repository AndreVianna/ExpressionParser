using System.Linq;
using System.Linq.Expressions;
using ExpressionParser.Model;
using ExpressionParser.Model.Nodes;

namespace ExpressionParser.Engine
{
	internal class Builder
	{
		private static Node BuildTree(TokenList tokens)
		{
			var nodes = new NodeStack();
			while (tokens.Any() && !(tokens.Current.EndsExpressionOrParameters || tokens.Current.IsParameterSeparator || tokens.Current.EndsIndex))
			{
				if (tokens.Current.StartsExpressionOrParameters && nodes.LastAdded is MethodNode method) {
					tokens.MoveNext();
					ProcessParameters(tokens, method);
				} else if (tokens.Current.StartsExpressionOrParameters) {
					tokens.MoveNext();
					ProcessExpression(tokens, nodes);
				} else if (tokens.Current.StartsIndex) {
					tokens.MoveNext();
					ProcessIndex(tokens, nodes);
				} else
					nodes.Add(tokens.Current.CreateNode(tokens));
				tokens.MoveNext();
			}
			return nodes.Pop();
		}

		private static void ProcessParameters(TokenList tokens, MethodNode methodNode)
		{
			while (!tokens.Current.EndsExpressionOrParameters) {
				var childNode = BuildTree(tokens);
				methodNode.Parameters.Add(childNode);
				if (tokens.Current.IsParameterSeparator)
					tokens.MoveNext();
			}
		}

		private static void ProcessExpression(TokenList tokens, NodeStack nodes)
		{
			var childNode = BuildTree(tokens);
			childNode.KickPrecedenceUp();
			nodes.Add(childNode);
		}

		private static void ProcessIndex(TokenList tokens, NodeStack nodes)
		{
			nodes.Add(new ArrayIndexNode());
			var childNode = BuildTree(tokens);
			nodes.Add(childNode);
		}

		internal static LambdaExpression BuildExpression(TokenList tokens)
		{
			var root = BuildTree(tokens);
			var body = root.BuildExpression();
			return Expression.Lambda(body);
		}

		internal static LambdaExpression BuildExpressionFor<TInput>(TokenList tokens, string parameterName = null)
		{
			var root = BuildTree(tokens);
			var parameterExpression = parameterName == null ? Expression.Parameter(typeof(TInput)) : Expression.Parameter(typeof(TInput), parameterName);
			var body = root.BuildExpression(parameterExpression);
			return Expression.Lambda(body, parameterExpression);
		}
	}
}