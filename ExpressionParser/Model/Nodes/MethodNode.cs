using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ExpressionParser.Extensions;

namespace ExpressionParser.Model.Nodes
{
	internal class MethodNode : IdentifierNode
	{
		private MethodInfo methodInfo;
		private bool isExtensionMethod;
		private Type callerType;
		private IList<Expression> arguments;

		internal MethodNode(string name) : base(name, 1) { }

		internal IList<Node>  Parameters { get; } = new List<Node>();

		internal override Expression BuildExpression(Expression callerExpression = null)
		{
			if (callerExpression == null) throw new Exception($"Invalid method '{Name}'");
			callerType = callerExpression.Type;
			GetArguments(callerExpression);
			GetMethodInfo(callerExpression);
			if (isExtensionMethod) UpdateExtensionMethodInfo();
			return GetCallExpression(callerExpression);
		}

		private void GetArguments(Expression callerExpression = null)
		{
			arguments = Parameters.Select(p => p.BuildExpression(callerExpression)).ToList();
		}

		private void GetMethodInfo(Expression callerExpression = null)
		{
			methodInfo = GetInstanceMethod(Name) ?? GetExtensionMethod(Name, callerExpression);
		}

		private void UpdateExtensionMethodInfo()
		{
			var genericArgumentType = callerType == typeof(string) ? typeof(char) : (callerType.IsArray ? callerType.GetElementType() : callerType.GetGenericArguments()[0]);
			methodInfo = methodInfo.MakeGenericMethod(genericArgumentType);
		}

		private MethodInfo GetInstanceMethod(string name)
		{
			var candidates = callerType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy).Where(i => i.Name == name).ToArray();
			return GetMethodInfoFromCandidates(candidates);
		}

		private MethodInfo GetExtensionMethod(string name, Expression callExpression)
		{
			arguments.Insert(0, callExpression);
			var candidates = typeof(Enumerable).GetMethods(BindingFlags.Static | BindingFlags.Public).Where(i => i.Name == name).ToArray();
			var method = GetMethodInfoFromCandidates(candidates);
			isExtensionMethod = method != null;
			return method;
		}

		private MethodInfo GetMethodInfoFromCandidates(MethodInfo[] candidates)
		{
			return (from candidate in candidates
				let parametersTypes = candidate.GetParameters().ToArray(p => p.ParameterType)
				where parametersTypes.Length == arguments.Count
					&& parametersTypes.Zip(arguments, AreEquivalent).All(e => e)
				select candidate).FirstOrDefault();
		}

		private static bool AreEquivalent(Type parameterType, Expression argument)
		{
			return argument.Type.Name == parameterType.Name || argument.Type.GetInterfaces().Any(i => i.Name.Equals(parameterType.Name));
		}

		private Expression GetCallExpression(Expression parameter)
		{
			return isExtensionMethod ? Expression.Call(methodInfo, arguments) : Expression.Call(parameter, methodInfo, arguments);
		}
	}
}