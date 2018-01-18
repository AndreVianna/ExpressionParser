using System;
using System.Collections.Generic;
using ExpressionParser.Engine;

namespace ExpressionParser
{
	internal class ExpressionParserImplementation : IExpressionParser
	{
		private readonly Reader reader = new Reader();

		public Delegate Parse(string input)
		{
			var tokens = reader.ReadFrom(input);
			var expression = Builder.BuildExpression(tokens);
			return expression.Compile();
		}

		public Func<TOutput> Parse<TOutput>(string input)
		{
			return (Func<TOutput>)Parse(input);
		}

		public Delegate ParseFor<TInput>(string input) {
			return ParseFor<TInput>(input, null);
		}

		public Delegate ParseFor<TInput>(string input, string parameterName)
		{
			var tokens = reader.ReadFrom(input);
			var expression = Builder.BuildExpressionFor<TInput>(tokens, parameterName);
			return expression.Compile();
		}

		public Func<TInput, TOutput> ParseFor<TInput, TOutput>(string input)
		{
			return (Func<TInput, TOutput>)ParseFor<TInput>(input, null);
		}

		public Func<TInput, TOutput> ParseFor<TInput, TOutput>(string input, string parameterName)
		{
			return (Func<TInput, TOutput>)ParseFor<TInput>(input, parameterName);
		}

		public IExpressionParser Using(Type type)
		{
			if (type == null) throw new ArgumentNullException(nameof(type));
			Reader.AddTypeMap(type.Name, type);
			return this;
		}

		public IExpressionParser Using(Type type, string alias)
		{
			if (alias == null) throw new ArgumentNullException(nameof(alias));
			Reader.AddTypeMap(alias, type);
			return this;
		}

		public IExpressionParser Using(IEnumerable<Type> types)
		{
			if (types == null) throw new ArgumentNullException(nameof(types));
			foreach (var type in types)
				Reader.AddTypeMap(type.Name, type);
			return this;
		}

		public IExpressionParser Using(IDictionary<Type, string> typeMaps)
		{
			if (typeMaps == null) throw new ArgumentNullException(nameof(typeMaps));
			foreach (var typeMap in typeMaps)
				Reader.AddTypeMap(typeMap.Value, typeMap.Key);
			return this;
		}
	}
}