using System;
using System.IO;

namespace Mimp.SeeSharper.IO.Provide.Abstraction
{
    [Serializable]
    public class ProvideIOException : IOException
    {


        public ProvideIOException() { }

        public ProvideIOException(string? message)
            : base(message) { }

        public ProvideIOException(string? message, Exception? inner)
            : base(message, inner) { }

        protected ProvideIOException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context
        ) : base(info, context) { }


        public static ProvideIOException GetInvalidSchemeException(Uri uri) =>
            new ProvideIOException($"{uri} has invalid scheme \"{uri.Scheme}\".");

        public static ProvideIOException GetInvalidSchemeException(string scheme) =>
            new ProvideIOException($"Invalid scheme \"{scheme}\".");


        public static ProvideIOException GetNoReadStreamException(IStreamInfo streamInfo) =>
            new ProvideIOException($"{streamInfo} isn't readable.");

        public static ProvideIOException GetNoWriteStreamException(IStreamInfo streamInfo) =>
            new ProvideIOException($"{streamInfo} isn't writable.");

        public static ProvideIOException GetNoDeleteStreamException(IStreamInfo streamInfo) =>
            new ProvideIOException($"{streamInfo} isn't deletable.");

        public static ProvideIOException GetNoCreateStreamParentException(IStreamParentInfo parentInfo) =>
            new ProvideIOException($"{parentInfo} isn't creatable.");

        public static ProvideIOException GetNoDeleteStreamParentException(IStreamParentInfo parentInfo) =>
            new ProvideIOException($"{parentInfo} isn't deletable.");


        public static ProvideIOException GetIsNoLongerStreamException(IStreamInfo streamInfo) =>
            new ProvideIOException($"{streamInfo} is no longer a stream.");

        public static ProvideIOException GetIsNoLongerStreamParentException(IStreamParentInfo parentInfo) =>
            new ProvideIOException($"{parentInfo} is no longer a stream parent.");


    }
}
