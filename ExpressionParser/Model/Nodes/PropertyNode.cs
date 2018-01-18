using System;
using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class PropertyNode : IdentifierNode
	{
		internal PropertyNode(string name) : base(name, 99) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			if (callerExpression == null) throw new InvalidOperationException($"Unknow identifier '{Name}'.");
			if (callerExpression is ParameterExpression parameterExpression && parameterExpression.Name == Name) return callerExpression;
			return Expression.PropertyOrField(callerExpression, Name);
		}
	}
}