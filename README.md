# Innovoft.Text.JSON
Instead of only passing a Utf8JsonReader also pass a UTF8JSONReaderStream and call public bool Read(ref Utf8JsonReader reader) which handle all the issues with reading from a stream.
