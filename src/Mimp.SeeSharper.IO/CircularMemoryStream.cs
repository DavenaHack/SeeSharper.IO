using System;
using System.IO;

namespace Mimp.SeeSharper.IO
{
    public class CircularMemoryStream : Stream
    {


        public override bool CanRead => true;

        public override bool CanSeek => true;


        public override bool CanWrite { get; }


        private long _length;
        public override long Length => _length;


        private long _circlePosition;
        private long _position;
        public override long Position
        {
            get => _position;
            set
            {
                IfDisposeThrow();
                if (value >= Length)
                    throw new ArgumentOutOfRangeException(nameof(Position), $"The position is set to a value greater than {nameof(Length)}.");
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Position), $"The position is set to a negative value.");
                _position = value;
            }
        }


        private long _capacity;
        /// <summary>
        /// The Capacity of the stream
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The capacity is set to less than 1 or less than the current <see cref="Length"/>.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="NotSupportedException">Stream is not resizeable.</exception>
        public virtual long Capacity
        {
            get => _capacity;
            set
            {
                IfDisposeThrow();
                if (!_resizeable)
                    throw new NotSupportedException("Stream is not resizable.");
                if (value < 1)
                    throw new ArgumentOutOfRangeException(nameof(Capacity), $"The capacity is set to less than 1.");
                if (value < Length)
                    throw new ArgumentOutOfRangeException(nameof(Capacity), $"The capacity is set to less than the current length of the stream.");
                var buffer = new byte[value];
                if (Length > 0)
                {
                    var i = _circlePosition % _capacity;
                    if (i + _length > _capacity)
                    {
                        var sl = _capacity - i;
                        Array.Copy(_buffer, _index + i, buffer, 0, sl);
                        Array.Copy(_buffer, _index, buffer, 0 + sl, _length - sl);
                    }
                    else
                        Array.Copy(_buffer, _index + i, buffer, 0, _length);
                }
                _buffer = buffer;
                _capacity = value;
            }
        }


        private byte[] _buffer;

        private long _index;

        private readonly bool _resizeable;


        /// <summary>
        /// Initializes a new instance with a capacity of 1024
        /// </summary>
        public CircularMemoryStream()
            : this(1024) { }

        /// <summary>
        /// Initailizes a new instance with a capacity of <paramref name="capacity"/>
        /// </summary>
        /// <param name="capacity"></param>
        /// <exception cref="ArgumentOutOfRangeException">If capacity is less than 1.</exception>
        public CircularMemoryStream(long capacity)
            : this(capacity, true) { }

        /// <summary>
        /// Initailizes a new instance with a capacity of <paramref name="capacity"/>
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="resizable"></param>
        /// <exception cref="ArgumentOutOfRangeException">If capacity is less than 1.</exception>
        public CircularMemoryStream(long capacity, bool resizable)
            : this(new byte[capacity], true, resizable)
        {
            _length = 0;
        }


        /// Initializes a new non-resizeable instance based on the specified <paramref name="buffer"/>
        /// </summary>
        /// <param name="buffer"></param>
        /// <exception cref="ArgumentNullException">If buffer is null.</exception>
        public CircularMemoryStream(byte[] buffer)
            : this(buffer, true) { }

        /// <summary>
        /// Initializes a new non-resizeable instance based on the specified <paramref name="buffer"/>
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="writable"></param>
        /// <exception cref="ArgumentNullException">If buffer is null.</exception>
        public CircularMemoryStream(byte[] buffer, bool writable)
            : this(buffer, writable, false) { }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="buffer"/>
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="writable"></param>
        /// <exception cref="ArgumentNullException">If buffer is null.</exception>
        public CircularMemoryStream(byte[] buffer, bool writable, bool resizable)
            : this(buffer, 0, buffer.Length, writable, resizable) { }

        /// <summary>
        /// Initializes a new non-resizable instance based on the specified <paramref name="buffer"/> with the start of <paramref name="index"/> and a length of <paramref name="length"/>
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <exception cref="ArgumentNullException">If <paramref name="buffer"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="index"/> of <paramref name="length"/> is less than zero</exception>
        /// <exception cref="ArgumentException">If the <paramref name="buffer"/> length minus <paramref name="index"/> is less than <paramref name="length"/></exception>
        public CircularMemoryStream(byte[] buffer, long index, long length)
            : this(buffer, index, length, true) { }

        /// <summary>
        /// Initializes a new non-resizable instance based on the specified <paramref name="buffer"/> with the start of <paramref name="index"/> and a length of <paramref name="length"/>
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="writable"></param>
        /// <exception cref="ArgumentNullException">If <paramref name="buffer"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="index"/> of <paramref name="length"/> is less than zero</exception>
        /// <exception cref="ArgumentException">If the <paramref name="buffer"/> length minus <paramref name="index"/> is less than <paramref name="length"/></exception>
        public CircularMemoryStream(byte[] buffer, long index, long length, bool writable)
            : this(buffer, index, length, writable, false) { }

        /// <summary>
        /// Initialize a new instance based on the specified <paramref name="buffer"/> with the start of <paramref name="index"/> and a length of <paramref name="length"/>
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="writable"></param>
        /// <param name="resizable"></param>
        /// <exception cref="ArgumentNullException">If <paramref name="buffer"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="index"/> of <paramref name="length"/> is less than zero</exception>
        /// <exception cref="ArgumentException">If the <paramref name="buffer"/> length minus <paramref name="index"/> is less than <paramref name="length"/></exception>
        public CircularMemoryStream(byte[] buffer, long index, long length, bool writable, bool resizable)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer), "Buffer is null.");
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is negative");
            if (length < 1)
                throw new ArgumentOutOfRangeException(nameof(length), "Length is less than 1");
            if (buffer.Length - index < length)
                throw new ArgumentException("The buffer length minus index is less than length");

            _buffer = buffer;
            _index = index;
            _capacity = _length = length;
            CanWrite = writable;
            _resizeable = resizable;
        }


        public override long Seek(long offset, SeekOrigin origin)
        {
            IfDisposeThrow();
            switch (origin)
            {
                case SeekOrigin.Begin:
                    _position = offset;
                    break;
                case SeekOrigin.Current:
                    _position += offset;
                    break;
                case SeekOrigin.End:
                    _position = Length - offset;
                    break;
                default:
                    throw new NotSupportedException();
            }
            return _position;
        }


        public override int Read(byte[] buffer, int offset, int count)
        {
            IfDisposeThrow();
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer), "Buffer is null");
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), "Offset is negative");
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Count is negative");
            if (count + offset > buffer.Length)
                throw new ArgumentException("The sum of offset and count is larger than the buffer length");

            var l = Math.Min(Length - Position, count);
            if (l == 0)
                return 0;

            var i = (_circlePosition + _position) % _capacity;
            if (i + l > _capacity)
            {
                var sl = _capacity - i;
                Array.Copy(_buffer, _index + i, buffer, offset, sl);
                Array.Copy(_buffer, _index, buffer, offset + sl, l - sl);
            }
            else
                Array.Copy(_buffer, _index + i, buffer, offset, l);

            _position += l;
            return (int)l;
        }


        public override void Write(byte[] buffer, int offset, int count)
        {
            IfDisposeThrow();
            if (!CanWrite)
                throw new NotSupportedException("Stream is readonly.");
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer), "Buffer is null");
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), "Offset is negative");
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Count is negative");
            if (count + offset > buffer.Length)
                throw new ArgumentException("The sum of offset and count is larger than the buffer length");

            if (count == 0)
                return;

            if (_position + count > _capacity)
            {
                _circlePosition = (_circlePosition + count - (_capacity - _position)) % _capacity;
                _length = _position = _capacity;
            }
            else
            {
                _position += count;
                if (_position > _length)
                    _length = _position;
            }

            var l = Math.Min(count, _capacity);
            var i = (_circlePosition + _position) % _capacity;
            var o = offset + count - l;
            if (l > i)
            {
                var sl = l - i;
                Array.Copy(buffer, o, _buffer, _index + _capacity - sl, sl);
                Array.Copy(buffer, o + sl, _buffer, _index, l - sl);
            }
            else
                Array.Copy(buffer, o, _buffer, _index + i - l, l);
        }


        public override void SetLength(long value)
        {
            IfDisposeThrow();
            if (!CanWrite)
                throw new NotSupportedException("Stream is readonly.");
            if (Length > Capacity)
                throw new ArgumentOutOfRangeException(nameof(value), "Length is greater than capacity");

            if (value > _length)
                for (var i = _length; i < value; i++)
                    _buffer[(_index + i) % Capacity] = 0;
            else if (value < _length)
                if (_position > value)
                    _position = value;
            _length = value;
        }


        public override void Flush()
        {
            IfDisposeThrow();
        }


        #region Dispose


        private bool _disposed;

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _disposed = true;
        }

        protected void IfDisposeThrow()
        {
            if (_disposed)
                throw new ObjectDisposedException("The current stream is closed.");
        }


        #endregion


    }
}
