using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Innovoft.Text.JSON
{
	public static class Utf8JsonReaderConverter
	{
		#region Methods
		public static decimal GetDecimal(ref Utf8JsonReader reader)
		{
			switch (reader.TokenType)
			{
			case JsonTokenType.String:
				if (!Utf8Parser.TryParse(reader.ValueSpan, out decimal value, out var consumed))
				{
					throw new FormatException();
				}
				return value;

			case JsonTokenType.Number:
				return reader.GetDecimal();

			default:
				throw new FormatException();
			}
		}

		public static double GetDouble(ref Utf8JsonReader reader)
		{
			switch (reader.TokenType)
			{
			case JsonTokenType.String:
				if (!Utf8Parser.TryParse(reader.ValueSpan, out double value, out var consumed))
				{
					throw new FormatException();
				}
				return value;

			case JsonTokenType.Number:
				return reader.GetDouble();

			default:
				throw new FormatException();
			}
		}

		public static bool TryGetDecimal(ref Utf8JsonReader reader, out decimal value)
		{
			switch (reader.TokenType)
			{
			case JsonTokenType.String:
				return Utf8Parser.TryParse(reader.ValueSpan, out value, out var consumed);

			case JsonTokenType.Number:
				return reader.TryGetDecimal(out value);

			default:
				value = default;
				return false;
			}
		}

		public static bool TryGetDouble(ref Utf8JsonReader reader, out double value)
		{
			switch (reader.TokenType)
			{
			case JsonTokenType.String:
				return Utf8Parser.TryParse(reader.ValueSpan, out value, out var consumed);

			case JsonTokenType.Number:
				return reader.TryGetDouble(out value);

			default:
				value = default;
				return false;
			}
		}
		#endregion //Methods
	}
}
