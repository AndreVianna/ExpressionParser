using System;
using System.Collections.Generic;

namespace ExpressionParser.Model
{
	internal static class Keywords
	{
		internal static readonly IDictionary<string, Type> BuiltInTypes = new Dictionary<string, Type> {
			{ "bool", typeof(bool) },
			{ "byte", typeof(byte) },
			{ "sbyte", typeof(sbyte) },
			{ "char", typeof(char) },
			{ "decimal", typeof(decimal) },
			{ "double", typeof(double) },
			{ "float", typeof(float) },
			{ "int", typeof(int) },
			{ "uint", typeof(uint) },
			{ "long", typeof(long) },
			{ "ulong", typeof(ulong) },
			{ "object", typeof(object) },
			{ "short", typeof(short) },
			{ "ushort", typeof(ushort) },
			{ "string", typeof(string) },
			{ "Boolean", typeof(bool) },
			{ "Byte", typeof(byte) },
			{ "SByte", typeof(sbyte) },
			{ "Char", typeof(char) },
			{ "Decimal", typeof(decimal) },
			{ "Double", typeof(double) },
			{ "Single", typeof(float) },
			{ "Int32", typeof(int) },
			{ "UInt32", typeof(uint) },
			{ "Int64", typeof(long) },
			{ "UInt64", typeof(ulong) },
			{ "Object", typeof(object) },
			{ "Int16", typeof(short) },
			{ "UInt16", typeof(ushort) },
			{ "String", typeof(string) },
		};
	}
}