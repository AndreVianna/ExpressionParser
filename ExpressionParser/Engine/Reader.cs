using System;
using System.Text.RegularExpressions;
using ExpressionParser.Model;
using ExpressionParser.Model.Tokens;

namespace ExpressionParser.Engine
{
	internal class Reader
	{
		private readonly TokenList result = new TokenList();
		private int characterPosition;

		private Reader() { }

		internal static TokenList ReadFrom(string input)
		{
			var reader = new Reader();
			reader.ReadAll(input);
			return reader.result;
		}

		private void ReadAll(string input)
		{
			for (characterPosition = 0; characterPosition < input.Length;)
				ProcessCharacter(input);
		}

		private void ProcessCharacter(string line)
		{
			if (FindValidToken(line)) return;
			throw new Exception($"Invalid token at position {characterPosition + 1}.");
		}

		private bool FindValidToken(string line)
		{
			return FindWhiteSpace(line) || FindChar(line) || FindString(line) || FindDecimal(line) || FindInteger(line) || FindToken(line) || FindCandidate(line);
		}

		private bool FindWhiteSpace(string line)
		{
			return TryCreateToken(line.Substring(characterPosition), @"^\s+", a => null);
		}

		private bool FindChar(string line)
		{
			return TryCreateToken(line.Substring(characterPosition), @"^('[^\\']'|'\\[\\'trn]')", a => new LiteralToken<char>(ConvertToChar(a.Substring(1, a.Length - 2))));
		}

		private char ConvertToChar(string source)
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

		private bool FindString(string line)
		{
			return TryCreateToken(line.Substring(characterPosition), @"^""[^""]*""", a => new LiteralToken<string>(a.Trim('"')));
		}

		private bool FindDecimal(string line)
		{
			return TryCreateToken(line.Substring(characterPosition), @"^((\d*\.\d+)|(\d+\.\d*))", a => new LiteralToken<decimal>(Convert.ToDecimal(a)));
		}
		private bool FindInteger(string line)
		{
			return TryCreateToken(line.Substring(characterPosition), @"^\d+", a => new LiteralToken<int>(Convert.ToInt32(a)));
		}

		private bool FindCandidate(string line)
		{
			var match = Regex.Match(line.Substring(characterPosition), @"^[\w]*", RegexOptions.IgnoreCase);
			var candidate = match.Value;
			return FindNull(candidate) || FindBoolean(candidate) || FindName(candidate);
		}

		private bool FindNull(string candidate)
		{
			return TryCreateToken(candidate, @"^null$", a => new LiteralToken<object>(null));
		}

		private bool FindBoolean(string candidate)
		{
			return TryCreateToken(candidate, @"^(true|false)$", a => new LiteralToken<bool>(Convert.ToBoolean(a)));
		}

		private bool FindName(string candidate)
		{
			return TryCreateToken(candidate, @"^[a-zA-Z_][\w]*$", a => new NameToken(a));
		}

		private bool TryCreateToken(string source, string regex, Func<string, Token> creator)
		{
			var match = Regex.Match(source, regex, RegexOptions.IgnoreCase);
			if (!match.Success) return false;
			var token = creator(match.Value);
			if (token != null) result.Add(creator(match.Value));
			characterPosition += match.Length;
			return true;
		}

		private bool FindToken(string line)
		{
			var current = line[characterPosition];
			var next = characterPosition < (line.Length - 1) ? line[characterPosition + 1] : (char?)null;
			var token = $"{current}{next}";
			switch (token) {
				case "!=":
				case "==":
				case "=>":
				case ">=":
				case "<=":
				case "&&":
				case "||":
				case "??":
				case "?.":
					result.Add(new SymbolToken(token));
					characterPosition++;
					characterPosition++;
					return true;
				default:
					token = $"{current}";
					break;
			}
			switch (token)
			{
				case "!":
				case ">":
				case "<":
				case "+":
				case "-":
				case "*":
				case "/":
				case "%":
				case ".":
				case "[":
				case "]":
				case "(":
				case ")":
				case ",":
				case "?":
				case ":":
					result.Add(new SymbolToken(token));
					characterPosition++;
					return true;
				default:
					return false;
			}
		}
	}
}