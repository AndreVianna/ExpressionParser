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
			{ "String", typeof(string) }
		};

		internal static readonly IEnumerable<string> TypeDefinition = new[]
		{
			"interface",
			"class",
			"enum",
			"struct"
		};

		internal static readonly IEnumerable<string> Query = new[]
		{
			"from",
			"where",
			"select",
			"group",
			"into",
			"orderby",
			"join",
			"let",
			"in",
			"on",
			"equals",
			"by",
			"ascending",
			"descending"
		};

		internal static readonly IEnumerable<string> Contextual = new[]
		{
			"add",
			"async",
			"await",
			"dynamic",
			"get",
			"global",
			"partial",
			"remove",
			"set",
			"value",
			"var",
			"when",
			"where",
			"yield"
		};

		internal static readonly IEnumerable<string> MemberAccess = new[]
		{
			"this",
			"base"
		};

		internal static readonly IEnumerable<string> Conversion = new[]
		{
			"explicit",
			"implicit",
			"operator"
		};

		internal static readonly IEnumerable<string> Operators = new[]
		{
			"as",
			"await",
			"is",
			"new",
			"nameof",
			"sizeof",
			"typeof",
			"stackalloc",
			"checked",
			"unchecked"
		};

		internal static readonly IEnumerable<string> Statements = new[]
		{
			"if",
			"else",
			"switch",
			"case",
			"default",
			"do",
			"for",
			"foreach",
			"in",
			"while",
			"break",
			"continue",
			"goto",
			"return",
			"throw",
			"try",
			"catch",
			"finally",
			"checked",
			"unchecked",
			"fixed",
			"lock"
		};

		internal static readonly IEnumerable<string> MethodDefinition = new[]
		{
			"delegate",
			"void",
			"params",
			"ref",
			"out",
			"function",
			"return"
		};

		internal static readonly IEnumerable<string> Namespace = new[]
		{
			"namespace",
			"using",
			"extern"
		};

		internal static readonly IEnumerable<string> Modifiers = new[]
		{
			"public",
			"protected",
			"internal",
			"private",
			"abstract",
			"async",
			"const",
			"event",
			"extern",
			"new",
			"override",
			"partial",
			"readonly",
			"sealed",
			"static",
			"unsafe",
			"virtual",
			"volatile",
			"in",
			"out"
		};

		internal static readonly IEnumerable<string> Literals = new[]
		{
			"null",
			"false",
			"true",
			"default"
		};
	}
}