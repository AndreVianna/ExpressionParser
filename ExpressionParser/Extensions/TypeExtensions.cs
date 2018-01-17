using System;

namespace ExpressionParser.Extensions
{
	public static class TypeExtensions
	{
		public static bool IsNullable(this Type source)
		{
			return source == typeof(string) || Nullable.GetUnderlyingType(source) != null;
		}

		public static Type MakeNullableType(this Type source)
		{
			return typeof(Nullable<>).MakeGenericType(source);
		}

		public static object GetDefaultValue(this Type source)
		{
			return !source.IsNullable() ? Activator.CreateInstance(source) : null;
		}
	}
}