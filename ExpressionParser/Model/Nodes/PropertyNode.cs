using System;
using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class PropertyNode : IdentifierNode
	{
		internal PropertyNode(string name) : base(name, 99) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			switch (callerExpression)
			{
				case null: throw new InvalidOperationException($"Unknow identifier '{Name}'.");
				case ParameterExpression parameterExpression when parameterExpression.Name == Name: return callerExpression;
				default: return Expression.PropertyOrField(callerExpression, Name);
			}
		}
	}
}