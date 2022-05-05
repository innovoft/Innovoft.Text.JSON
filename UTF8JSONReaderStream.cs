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
			return new Utf8JsonReader();
		}

		public bool Read(ref Utf8JsonReader reader)
		{
			if (reader.Read())
			{
				return true;
			}

			var state = reader.CurrentState;

			//TODO: Read more data

			reader = new Utf8JsonReader();
			return reader.Read();
		}
		#endregion //Methods
	}
}
