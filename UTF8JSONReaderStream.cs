using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Innovoft.Text.JSON
{
	public sealed class UTF8JSONReaderStream : IDisposable
	{
		#region Fields
		private readonly Stream stream;
		private readonly byte[] buffer;
		private readonly bool dispose;

		private int count;
		#endregion //Fields

		#region Constructors
		public UTF8JSONReaderStream(Stream stream, byte[] buffer)
		{
			this.stream = stream;
			this.buffer = buffer;
			this.dispose = true;
		}

		public UTF8JSONReaderStream(Stream stream, byte[] buffer, bool dispose)
		{
			this.stream = stream;
			this.buffer= buffer;
			this.dispose = dispose;
		}
		#endregion //Constructors

		#region Finalizer
		~UTF8JSONReaderStream()
		{
			Dispose(disposing: false);
		}
		#endregion //Finalizer

		#region Properties
		public Stream Stream => stream;
		public byte[] Buffer => buffer;
		#endregion //Properties

		#region Methods
		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}

			if (dispose)
			{
				stream.Dispose();
			}
		}

		public Utf8JsonReader Create()
		{
			count = stream.Read(buffer, 0, buffer.Length);
			return new Utf8JsonReader(new ReadOnlySpan<byte>(buffer, 0, count), count <= 0, new JsonReaderState());
		}

		public bool Read(ref Utf8JsonReader reader)
		{
			if (reader.Read())
			{
				return true;
			}
			int offset;
			var consumed = (int)reader.BytesConsumed;
			if (consumed < count)
			{
				offset = count - consumed;
				System.Buffer.BlockCopy(buffer, consumed, buffer, 0, offset);
			}
			else
			{
				offset = 0;
			}
			var read = stream.Read(buffer, offset, buffer.Length - offset);
			if (read <= 0)
			{
				return false;
			}
			count = offset + read;
			reader = new Utf8JsonReader(new ReadOnlySpan<byte>(buffer, 0, count), read <= 0, reader.CurrentState);
			return reader.Read();
		}

		public void Skip(ref Utf8JsonReader reader)
		{
			if (reader.TokenType == JsonTokenType.PropertyName)
			{
				if (!Read(ref reader))
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
				if (!Read(ref reader))
				{
					throw new EndOfStreamException();
				}
				if (reader.CurrentDepth == depth)
				{
					return;
				}
			}
		}

		public bool TrySkip(ref Utf8JsonReader reader)
		{
			if (reader.TokenType == JsonTokenType.PropertyName)
			{
				if (!Read(ref reader))
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
				if (!Read(ref reader))
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
