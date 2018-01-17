using System;
using System.Collections.Generic;
using System.Reflection;
using ExpressionParser.Engine;

namespace ExpressionParser
{
	internal class ExpressionParserImplementation : IExpressionParser
	{
		private readonly Reader reader = new Reader();
		private readonly Builder builder = new Builder();

		public Delegate Parse(string input)
		{
			var tokens = reader.ReadFrom(input);
			var expression = builder.BuildExpression(tokens, Assembly.GetCallingAssembly());
			return expression.Compile();
		}

		public Func<TOutput> Parse<TOutput>(string input)
		{
			return (Func<TOutput>)Parse(input);
		}

		public Delegate ParseFor<TInput>(string input, string parameterName)
		{
			var tokens = reader.ReadFrom(input);
			var expression = builder.BuildExpressionFor<TInput>(tokens, Assembly.GetCallingAssembly(), parameterName);
			return expression.Compile();
		}

		public Func<TInput, TOutput> ParseFor<TInput, TOutput>(string input, string parameterName)
		{
			return (Func<TInput, TOutput>)ParseFor<TInput>(input, parameterName);
		}

		public IExpressionParser Using(Type type, string alias)
		{
			reader.AddTypeMap(alias ?? type.Name, type);
			return this;
		}

		public IExpressionParser Using(IEnumerable<Type> types)
		{
			foreach (var type in types)
				reader.AddTypeMap(type.Name, type);
			return this;
		}

		public IExpressionParser Using(IDictionary<Type, string> typeMaps)
		{
			foreach (var typeMap in typeMaps)
				reader.AddTypeMap(typeMap.Value, typeMap.Key);
			return this;
		}
	}
}