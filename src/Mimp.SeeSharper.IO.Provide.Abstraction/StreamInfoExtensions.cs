using Mimp.SeeSharper.Async;
using Mimp.SeeSharper.Async.Abstraction;
using System;
using System.IO;
using System.Threading;

namespace Mimp.SeeSharper.IO.Provide.Abstraction
{
    public static class StreamInfoExtensions
    {


        #region Read


        public static bool CanRead(this IStreamInfo streamInfo)
        {
            if (streamInfo is null)
                throw new ArgumentNullException(nameof(streamInfo));

            return streamInfo is IReadStreamInfo || streamInfo is IAsyncReadStreamInfo;
        }


        public static Stream OpenRead(this IStreamInfo streamInfo)
        {
            if (streamInfo is null)
                throw new ArgumentNullException(nameof(streamInfo));

            return streamInfo switch
            {
                IReadStreamInfo read => read.OpenRead(),
                IAsyncReadStreamInfo read => read.OpenReadAsync().Await(),
                _ => throw ProvideIOException.GetNoReadStreamException(streamInfo)
            };
        }

        public static async IAwaitable<Stream> OpenReadAsync(this IStreamInfo streamInfo, CancellationToken cancellationToken)
        {
            if (streamInfo is null)
                throw new ArgumentNullException(nameof(streamInfo));

            return streamInfo switch
            {
                IAsyncReadStreamInfo read => await read.OpenReadAsync(cancellationToken),
                IReadStreamInfo read => read.OpenRead(),
                _ => throw ProvideIOException.GetNoReadStreamException(streamInfo)
            };
        }

        public static IAwaitable<Stream> OpenReadAsync(this IStreamInfo streamInfo) =>
            streamInfo.OpenReadAsync(CancellationToken.None);



        #endregion Read


        #region Write


        public static bool CanWrite(this IStreamInfo streamInfo)
        {
            if (streamInfo is null)
                throw new ArgumentNullException(nameof(streamInfo));

            return streamInfo is IWriteStreamInfo || streamInfo is IAsyncWriteStreamInfo;
        }


        public static Stream OpenWrite(this IStreamInfo streamInfo)
        {
            if (streamInfo is null)
                throw new ArgumentNullException(nameof(streamInfo));

            return streamInfo switch
            {
                IWriteStreamInfo write => write.OpenWrite(),
                IAsyncWriteStreamInfo write => write.OpenWriteAsync().Await(),
                _ => throw ProvideIOException.GetNoWriteStreamException(streamInfo)
            };
        }

        public static async IAwaitable<Stream> OpenWriteAsync(this IStreamInfo streamInfo, CancellationToken cancellationToken)
        {
            if (streamInfo is null)
                throw new ArgumentNullException(nameof(streamInfo));

            return streamInfo switch
            {
                IAsyncWriteStreamInfo write => await write.OpenWriteAsync(cancellationToken),
                IWriteStreamInfo write => write.OpenWrite(),
                _ => throw ProvideIOException.GetNoWriteStreamException(streamInfo)
            };
        }

        public static IAwaitable<Stream> OpenWriteAsync(this IStreamInfo streamInfo) =>
            streamInfo.OpenWriteAsync(CancellationToken.None);


        #endregion Write


        #region Delete


        public static bool CanDelete(this IStreamInfo streamInfo)
        {
            if (streamInfo is null)
                throw new ArgumentNullException(nameof(streamInfo));

            return streamInfo is IDeleteStreamInfo || streamInfo is IAsyncDeleteStreamInfo;
        }


        public static bool Delete(this IStreamInfo streamInfo)
        {
            if (streamInfo is null)
                throw new ArgumentNullException(nameof(streamInfo));

            return streamInfo switch
            {
                IDeleteStreamInfo delete => delete.Delete(),
                IAsyncDeleteStreamInfo delete => delete.DeleteAsync().Await(),
                _ => throw ProvideIOException.GetNoDeleteStreamException(streamInfo)
            };
        }

        public static async IAwaitable<bool> DeleteAsync(this IStreamInfo streamInfo, CancellationToken cancellationToken)
        {
            if (streamInfo is null)
                throw new ArgumentNullException(nameof(streamInfo));

            return streamInfo switch
            {
                IAsyncDeleteStreamInfo delete => await delete.DeleteAsync(cancellationToken),
                IDeleteStreamInfo delete => delete.Delete(),
                _ => throw ProvideIOException.GetNoDeleteStreamException(streamInfo)
            };
        }

        public static IAwaitable<bool> DeleteAsync(this IStreamInfo streamInfo) =>
            streamInfo.DeleteAsync(CancellationToken.None);



        #endregion Delete


    }
}
