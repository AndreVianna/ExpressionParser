using System;
using System.Collections.Generic;

namespace ExpressionParser
{
	public static class ExpressionParser
	{
		public static Delegate Parse(string input)
		{
			var parser = new ExpressionParserImplementation();
			return parser.Parse(input);
		}

		public static Delegate ParseFor<TInput>(string input, string parameterName = null)
		{
			var parser = new ExpressionParserImplementation();
			return parser.Using(new[] { typeof(TInput) }).ParseFor<TInput>(input, parameterName);
		}


		public static Func<TOutput> Parse<TOutput>(string input)
		{
			var parser = new ExpressionParserImplementation();
			return parser.Parse<TOutput>(input);
		}

		public static Func<TInput, TOutput> ParseFor<TInput, TOutput>(string input, string parameterName = null)
		{
			var parser = new ExpressionParserImplementation();
			return parser.Using(new [] { typeof(TInput), typeof(TOutput) }).ParseFor<TInput, TOutput>(input, parameterName);
		}


		public static IExpressionParser Using(Type type)
		{
			var parser = new ExpressionParserImplementation();
			return parser.Using(type);
		}

		public static IExpressionParser Using(Type type, string alias)
		{
			var parser = new ExpressionParserImplementation();
			return parser.Using(type, alias);
		}

		public static IExpressionParser Using(IEnumerable<Type> types)
		{
			var parser = new ExpressionParserImplementation();
			return parser.Using(types);
		}

		public static IExpressionParser Using(IDictionary<Type, string> typeMap)
		{
			var parser = new ExpressionParserImplementation();
			return parser.Using(typeMap);
		}
	}
}