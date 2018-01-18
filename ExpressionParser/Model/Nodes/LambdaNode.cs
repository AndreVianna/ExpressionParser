using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class LambdaNode : BinaryNode
	{
		internal LambdaNode() : base(14) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			if (callerExpression == null) throw new ArgumentNullException(nameof(callerExpression));
			var parameterName = ((IdentifierNode)Left).Name;
			var parameterType = GetParameterType();
			Debug.Assert(parameterType != null);
			var parameterExpression = Expression.Parameter(parameterType, parameterName);
			return Expression.Lambda(Right.BuildExpression(parameterExpression), parameterExpression);

			Type GetParameterType()
			{
				return callerExpression.Type == typeof(string) ? typeof(char) :
					callerExpression.Type.IsArray ? callerExpression.Type.GetElementType() :
					callerExpression.Type.GetGenericArguments()[0];
			}
		}
	}
}