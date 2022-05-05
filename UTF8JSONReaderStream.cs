using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Innovoft.Text.JSON
{
	public sealed class UTF8JSONReaderStream : IDisposable
	{
		#region Fields
		private readonly Stream stream;
		private readonly bool streamDispose;
		private readonly Action? dispose;
		#endregion //Fields

		#region Constructors
		public UTF8JSONReaderStream(Stream stream)
		{
			this.stream = stream;
			this.streamDispose = true;
			this.dispose = null;
		}

		public UTF8JSONReaderStream(Stream stream, bool streamDispose)
		{
			this.stream = stream;
			this.streamDispose = streamDispose;
			this.dispose = null;
		}

		public UTF8JSONReaderStream(Stream stream, bool streamDispose, Action dispose)
		{
			this.stream = stream;
			this.streamDispose = streamDispose;
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

			dispose?.Invoke();
		}
		#endregion //Methods
	}
}
