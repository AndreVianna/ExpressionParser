using System;
using ExpressionParser.Engine;

namespace ExpressionParser
{
	public class ExpressionParser
	{
		public static Delegate Parse(string input)
		{
			var tokens = Reader.ReadFrom(input);
			var expression = Builder.BuildExpression(tokens);
			return expression.Compile();
		}
		public static Func<TOutput> Parse<TOutput>(string input)
		{
			return (Func<TOutput>)Parse(input);
		}

		public static Delegate ParseFor<TInput>(string input, string parameterName = null)
		{
			var tokens = Reader.ReadFrom(input);
			var expression = Builder.BuildExpressionFor<TInput>(tokens, parameterName);
			return expression.Compile();
		}

		public static Func<TInput, TOutput> ParseFor<TInput, TOutput>(string input, string parameterName = null)
		{
			return (Func<TInput, TOutput>)ParseFor<TInput>(input, parameterName);
		}
	}
}