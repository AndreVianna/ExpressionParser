using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ExpressionParser.Model;
using ExpressionParser.Model.Tokens;

namespace ExpressionParser.Engine
{
	internal class Reader
	{
		private readonly TokenList result = new TokenList();
		private int characterPosition;
		private static readonly IDictionary<string, Type> availableTypes = new Dictionary<string, Type>(Keywords.BuiltInTypes);

		internal static void AddTypeMap(string alias, Type type) => availableTypes[alias ?? type?.Name ?? throw new ArgumentNullException(nameof(type))] = type;

		public TokenList ReadFrom(string input)
		{
			for (characterPosition = 0; characterPosition < input.Length;)
				ProcessCharacter(input);
			return result;
		}

		private void ProcessCharacter(string input)
		{
			if (FindValidToken(input)) return;
			throw new ArgumentException($"Invalid token at position {characterPosition + 1}.", nameof(input));
		}

		private bool FindValidToken(string input)
		{
			return FindWhiteSpace(input) || FindChar(input) || FindString(input) || FindDecimal(input) || FindInteger(input) || FindToken(input) || FindCandidate(input);
		}

		private bool FindWhiteSpace(string input)
		{
			return TryCreateToken(input.Substring(characterPosition), @"^\s+", a => null);
		}

		private bool FindChar(string input)
		{
			return TryCreateToken(input.Substring(characterPosition), @"^('[^\\']'|'\\[\\'trn]')", a => new LiteralToken<char>(ConvertToChar(a.Substring(1, a.Length - 2))));
		}

		private static char ConvertToChar(string source)
		{
			switch (source)
			{
				case @"\t": return '\t';
				case @"\r": return '\r';
				case @"\n": return '\n';
				case @"\'": return '\'';
				case @"\\": return '\\';
				default: return source[0];
			}
		}

		private bool FindString(string input)
		{
			return TryCreateToken(input.Substring(characterPosition), @"^""[^""]*""", a => new LiteralToken<string>(a.Trim('"')));
		}

		private bool FindDecimal(string input)
		{
			return TryCreateToken(input.Substring(characterPosition), @"^((\d*\.\d+)|(\d+\.\d*))", a => new LiteralToken<decimal>(Convert.ToDecimal(a)));
		}
		private bool FindInteger(string input)
		{
			return TryCreateToken(input.Substring(characterPosition), @"^\d+", a => new LiteralToken<int>(Convert.ToInt32(a)));
		}

		private bool FindCandidate(string input)
		{
			var match = Regex.Match(input.Substring(characterPosition), @"^[\w]*", RegexOptions.IgnoreCase);
			var candidate = match.Value;
			return FindNull(candidate) || FindBoolean(candidate) || FindNamedOperator(candidate) || FindType(candidate) || FindName(candidate);
		}

		private bool FindNull(string candidate)
		{
			return TryCreateToken(candidate, @"^null$", a => new LiteralToken<object>(null));
		}

		private bool FindBoolean(string candidate)
		{
			return TryCreateToken(candidate, @"^(true|false)$", a => new LiteralToken<bool>(Convert.ToBoolean(a)));
		}

		private bool FindNamedOperator(string candidate)
		{
			return TryCreateToken(candidate, @"^(is|as)$", a => new SymbolToken(a));
		}

		private bool FindType(string candidate)
		{
			if (!availableTypes.TryGetValue(candidate, out var type)) return false;
			result.Add(new TypeToken(type, "Type"));
			characterPosition += candidate.Length;
			return true;
		}

		private bool FindName(string candidate)
		{
			return TryCreateToken(candidate, @"^[a-zA-Z_][\w]*$", a => new NameToken(a, "Property"));
		}

		private bool TryCreateToken(string source, string regex, Func<string, Token> creator)
		{
			var match = Regex.Match(source, regex, RegexOptions.IgnoreCase);
			if (!match.Success) return false;
			var token = creator(match.Value);
			if (token != null) result.Add(token);
			characterPosition += match.Length;
			return true;
		}

		private bool FindToken(string input)
		{
			var current = input[characterPosition];
			var next = characterPosition < (input.Length - 1) ? input[characterPosition + 1] : (char?)null;
			return FindSupportedSymbol($"{current}{next}") || FindSupportedSymbol($"{current}");
		}

		private bool FindSupportedSymbol(string token)
		{
			var candidates = TokenList.SupportedOperators.Keys.Where(i => i.Length == token.Length).ToArray();
			var symbol = candidates.FirstOrDefault(s => s == token);
			if (symbol == null) return false;
			switch (symbol) {
				case "is":
				case "as":
					return false;
				case "+" when IsUnaryOperatorPattern():
					result.Add(new SymbolToken("[+]"));
					break;
				case "-" when IsUnaryOperatorPattern():
					result.Add(new SymbolToken("[-]"));
					break;
				case "(" when IsMethodtPattern(out var methodToken):
					methodToken.NodeType = "Method";
					result.Add(new SymbolToken(token));
					break;
				case ")" when IsTypeCastPattern(out var typeCastToken):
					typeCastToken.NodeType = "TypeCast";
					result.RemoveAt(result.Count - 2);
					break;
				default:
					result.Add(new SymbolToken(token));
					break;
			}
			characterPosition += token.Length;
			return true;
		}

		private bool IsUnaryOperatorPattern()
		{
			return !result.Any() || (result.TokenAt(result.Count - 1) is SymbolToken);
		}

		private bool IsTypeCastPattern(out TypeToken token) {
			token = (result.TokenAt(result.Count - 1) is TypeToken candidate
			    && result.TokenAt(result.Count - 2) is SymbolToken previousSymbol
			    && previousSymbol.Symbol == "(") ? candidate : null;
			return token != null;
		}

		private bool IsMethodtPattern(out NameToken token)
		{
			token = (result.TokenAt(result.Count - 1) is NameToken candidate) ? candidate : null;
			return token != null;
		}
	}
}