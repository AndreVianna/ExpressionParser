﻿using System;
using System.Linq.Expressions;

namespace ExpressionParser.Model.Nodes
{
	internal class TypeCastNode : UnaryNode
	{
		internal TypeCastNode(Type type) : base(0)
		{
			Type = type;
		}

		internal Type Type { get; }

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			var child = Child.BuildExpression(callerExpression);
			return Type == child.Type ? child : Expression.ConvertChecked(child, Type);
		}
	}
}