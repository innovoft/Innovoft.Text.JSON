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
		private readonly bool streamDispose;
		#endregion //Fields

		#region Constructors
		public UTF8JSONReaderStream(Stream stream)
		{
			this.stream = stream;
			this.streamDispose = true;
		}

		public UTF8JSONReaderStream(Stream stream, bool streamDispose)
		{
			this.stream = stream;
			this.streamDispose = streamDispose;
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

			if (streamDispose)
			{
				stream.Dispose();
			}
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
