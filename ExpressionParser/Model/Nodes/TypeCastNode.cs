using ExpressionParser.Engine;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ExpressionParser.Model.Nodes
{
	internal class TypeCastNode : UnaryNode
	{
		internal TypeCastNode(string typeName) : base(0)
		{
			TypeName = typeName;
		}

		internal string TypeName { get; }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			var type = Builder.GetMappedType(TypeName);
			var child = Child.BuildExpression(callerExpression);
			if (type == child.Type) return child;
			return Expression.ConvertChecked(child, type);
		}
	}
}