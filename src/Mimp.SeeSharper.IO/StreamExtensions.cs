using Mimp.SeeSharper.Async;
using Mimp.SeeSharper.Async.Abstraction;
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Mimp.SeeSharper.IO
{
    public static class StreamExtensions
    {


        public static Stream EmptyRead { get; } = new MemoryStream(Array.Empty<byte>(), false);


        #region Write


        public static void Write(this Stream stream, byte[] buffer)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            stream.Write(buffer, 0, buffer.Length);
        }

        public static IAwaitable WriteAsync(this Stream stream, byte[] buffer, CancellationToken cancellationToken)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            return stream.WriteAsync(buffer, 0, buffer.Length, cancellationToken).ToAwaitable();
        }

        public static IAwaitable WriteAsync(this Stream stream, byte[] buffer) =>
            WriteAsync(stream, buffer, CancellationToken.None);


        public static void Write(this Stream stream, string value, Encoding encoding)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));
            if (value is null)
                throw new ArgumentNullException(nameof(value));
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            stream.Write(encoding.GetBytes(value));
        }

        public static void Write(this Stream stream, string value)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            stream.Write(value, Encoding.Default);
        }

        public static IAwaitable WriteAsync(this Stream stream, string value, Encoding encoding, CancellationToken cancellationToken)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));
            if (value is null)
                throw new ArgumentNullException(nameof(value));
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            return WriteAsync(stream, encoding.GetBytes(value), cancellationToken);
        }

        public static IAwaitable WriteAsync(this Stream stream, string value, Encoding encoding) =>
            stream.WriteAsync(value, encoding, CancellationToken.None);

        public static IAwaitable WriteAsync(this Stream stream, string value, CancellationToken cancellationToken)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            return stream.WriteAsync(value, Encoding.Default, cancellationToken);
        }

        public static IAwaitable WriteAsync(this Stream stream, string value) =>
            stream.WriteAsync(value, CancellationToken.None);


        #endregion Write


        #region Read


        public static int Read(this Stream stream, byte[] buffer)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            return stream.Read(buffer, 0, buffer.Length);
        }

        public static IAwaitable<int> ReadAsync(this Stream stream, byte[] buffer, CancellationToken cancellationToken)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            return stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ToAwaitable();
        }

        public static IAwaitable<int> ReadAsync(this Stream stream, byte[] buffer) =>
            ReadAsync(stream, buffer, CancellationToken.None);


        public static byte[] ReadBytes(this Stream stream, long length)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));

            var buffer = new byte[length];
            stream.Read(buffer);
            return buffer;
        }

        public static async IAwaitable<byte[]> ReadBytesAsync(this Stream stream, long length, CancellationToken cancellationToken)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));

            var buffer = new byte[length];
            await stream.ReadAsync(buffer, cancellationToken);
            return buffer;
        }

        public static IAwaitable<byte[]> ReadBytesAsync(this Stream stream, long length) =>
            stream.ReadBytesAsync(length, CancellationToken.None);


        public static byte[] ReadBytes(this Stream stream)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));

            return stream.ReadBytes(stream.Length - stream.Position);
        }

        public static IAwaitable<byte[]> ReadBytesAsync(this Stream stream, CancellationToken cancellationToken)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));

            return stream.ReadBytesAsync(stream.Length - stream.Position, cancellationToken);
        }

        public static IAwaitable<byte[]> ReadBytesAsync(this Stream stream) =>
            stream.ReadBytesAsync(CancellationToken.None);


        public static string ReadString(this Stream stream, Encoding encoding)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            return encoding.GetString(stream.ReadBytes());
        }

        public static string ReadString(this Stream stream)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));

            return stream.ReadString(Encoding.Default);
        }

        public static async IAwaitable<string> ReadStringAsync(this Stream stream, Encoding encoding, CancellationToken cancellationToken)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            return encoding.GetString(await stream.ReadBytesAsync(cancellationToken));
        }

        public static IAwaitable<string> ReadStringAsync(this Stream stream, Encoding encoding) =>
            stream.ReadStringAsync(encoding, CancellationToken.None);

        public static IAwaitable<string> ReadStringAsync(this Stream stream, CancellationToken cancellationToken)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));

            return stream.ReadStringAsync(Encoding.Default, cancellationToken);
        }

        public static IAwaitable<string> ReadStringAsync(this Stream stream) =>
            stream.ReadStringAsync(CancellationToken.None);


        public static string ReadString(this Stream stream, int length, Encoding encoding)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            return ReadString(stream, stream.ReadBytes(GetReadLength(stream, length, encoding)), length, encoding);
        }

        public static string ReadString(this Stream stream, int length)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));

            return stream.ReadString(length, Encoding.Default);
        }

        public static async IAwaitable<string> ReadStringAsync(this Stream stream, int length, Encoding encoding, CancellationToken cancellationToken)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            return ReadString(stream, await stream.ReadBytesAsync(GetReadLength(stream, length, encoding), cancellationToken), length, encoding);
        }

        public static IAwaitable<string> ReadStringAsync(this Stream stream, int length, Encoding encoding) =>
            stream.ReadStringAsync(length, encoding, CancellationToken.None);

        public static IAwaitable<string> ReadStringAsync(this Stream stream, int length, CancellationToken cancellationToken)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));

            return stream.ReadStringAsync(length, Encoding.Default, cancellationToken);
        }

        public static IAwaitable<string> ReadStringAsync(this Stream stream, int length) =>
            stream.ReadStringAsync(length, CancellationToken.None);

        private static long GetReadLength(Stream stream, int length, Encoding encoding) =>
            Math.Min(encoding.GetMaxByteCount(length), stream.Length - stream.Position);

        private static string ReadString(Stream stream, byte[] buffer, int length, Encoding encoding)
        {
            var result = encoding.GetString(buffer).Substring(0, length);
            var usedBuffer = encoding.GetBytes(result);
            if (usedBuffer.Length != buffer.Length)
                stream.Seek(usedBuffer.Length - buffer.Length, SeekOrigin.Current);
            return result;
        }


        #endregion Read


        #region Seek


        public static void SeekTo(this Stream stream, long offset)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));

            stream.Seek(offset, SeekOrigin.Begin);
        }

        public static void SeekStart(this Stream stream) =>
            stream.SeekTo(0);


        public static void SeekOffset(this Stream stream, long offset)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));

            stream.Seek(offset, SeekOrigin.Current);
        }


        public static void SeekEnd(this Stream stream)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));

            stream.Seek(0, SeekOrigin.End);
        }


        #endregion Seek


    }

}
