﻿using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
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

		public static float GetSingle(ref Utf8JsonReader reader)
		{
			switch (reader.TokenType)
			{
			case JsonTokenType.String:
				if (!Utf8Parser.TryParse(reader.ValueSpan, out float value, out var consumed))
				{
					throw new FormatException();
				}
				return value;

			case JsonTokenType.Number:
				return reader.GetSingle();

			default:
				throw new FormatException();
			}
		}

		public static short GetInt16(ref Utf8JsonReader reader)
		{
			switch (reader.TokenType)
			{
			case JsonTokenType.String:
				if (!Utf8Parser.TryParse(reader.ValueSpan, out short value, out var consumed))
				{
					throw new FormatException();
				}
				return value;

			case JsonTokenType.Number:
				return reader.GetInt16();

			default:
				throw new FormatException();
			}
		}

		public static int GetInt32(ref Utf8JsonReader reader)
		{
			switch (reader.TokenType)
			{
			case JsonTokenType.String:
				if (!Utf8Parser.TryParse(reader.ValueSpan, out int value, out var consumed))
				{
					throw new FormatException();
				}
				return value;

			case JsonTokenType.Number:
				return reader.GetInt32();

			default:
				throw new FormatException();
			}
		}

		public static long GetInt64(ref Utf8JsonReader reader)
		{
			switch (reader.TokenType)
			{
			case JsonTokenType.String:
				if (!Utf8Parser.TryParse(reader.ValueSpan, out long value, out var consumed))
				{
					throw new FormatException();
				}
				return value;

			case JsonTokenType.Number:
				return reader.GetInt64();

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

		public static bool TryGetSingle(ref Utf8JsonReader reader, out float value)
		{
			switch (reader.TokenType)
			{
			case JsonTokenType.String:
				return Utf8Parser.TryParse(reader.ValueSpan, out value, out var consumed);

			case JsonTokenType.Number:
				return reader.TryGetSingle(out value);

			default:
				value = default;
				return false;
			}
		}

		public static bool TryGetInt16(ref Utf8JsonReader reader, out short value)
		{
			switch (reader.TokenType)
			{
			case JsonTokenType.String:
				return Utf8Parser.TryParse(reader.ValueSpan, out value, out var consumed);

			case JsonTokenType.Number:
				return reader.TryGetInt16(out value);

			default:
				value = default;
				return false;
			}
		}

		public static bool TryGetInt32(ref Utf8JsonReader reader, out int value)
		{
			switch (reader.TokenType)
			{
			case JsonTokenType.String:
				return Utf8Parser.TryParse(reader.ValueSpan, out value, out var consumed);

			case JsonTokenType.Number:
				return reader.TryGetInt32(out value);

			default:
				value = default;
				return false;
			}
		}

		public static bool TryGetInt64(ref Utf8JsonReader reader, out long value)
		{
			switch (reader.TokenType)
			{
			case JsonTokenType.String:
				return Utf8Parser.TryParse(reader.ValueSpan, out value, out var consumed);

			case JsonTokenType.Number:
				return reader.TryGetInt64(out value);

			default:
				value = default;
				return false;
			}
		}

		public static void Skip(ref Utf8JsonReader reader)
		{
			if (reader.TokenType == JsonTokenType.PropertyName)
			{
				if (!reader.Read())
				{
					throw new EndOfStreamException();
				}
			}

			if (reader.TokenType != JsonTokenType.StartObject &&
				reader.TokenType != JsonTokenType.EndObject)
			{
				return;
			}

			var depth = reader.CurrentDepth;
			while (true)
			{
				if (!reader.Read())
				{
					throw new EndOfStreamException();
				}
				if (reader.CurrentDepth == depth)
				{
					return;
				}
			}
		}

		public static bool TrySkip(ref Utf8JsonReader reader)
		{
			if (reader.TokenType == JsonTokenType.PropertyName)
			{
				if (!reader.Read())
				{
					return false;
				}
			}

			if (reader.TokenType != JsonTokenType.StartObject &&
				reader.TokenType != JsonTokenType.EndObject)
			{
				return true;
			}

			var depth = reader.CurrentDepth;
			while (true)
			{
				if (!reader.Read())
				{
					return false;
				}
				if (reader.CurrentDepth == depth)
				{
					return true;
				}
			}
		}
		#endregion //Methods
	}
}
