using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class LambdaNode : BinaryNode
	{
		internal LambdaNode() : base(14) { }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			var parameterName = ((IdentifierNode)Left).Name;
			var elementType = callerExpression.Type == typeof(string) ? typeof(char) : (callerExpression.Type.IsArray ? callerExpression.Type.GetElementType() : callerExpression.Type.GetGenericArguments()[0]);
			var parameterExpression = Expression.Parameter(elementType, parameterName);
			return Expression.Lambda(Right.BuildExpression(parameterExpression), parameterExpression);
		}
	}
}