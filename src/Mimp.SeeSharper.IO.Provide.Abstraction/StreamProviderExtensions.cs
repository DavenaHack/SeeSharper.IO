using Mimp.SeeSharper.Async.Abstraction;
using System;
using System.IO;
using System.Threading;

namespace Mimp.SeeSharper.IO.Provide.Abstraction
{
    public static class StreamProviderExtensions
    {


        #region ProvideStream


        public static async IAwaitable<IStreamInfo> ProvideStreamAsync(this IStreamProvider provider, Uri uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider switch
            {
                IAsyncStreamProvider async => await async.ProvideStreamAsync(uri, cancellationToken),
                _ => provider.ProvideStream(uri)
            };
        }

        public static IAwaitable<IStreamInfo> ProvideStreamAsync(this IStreamProvider provider, Uri uri) =>
            provider.ProvideStreamAsync(uri, CancellationToken.None);


        #endregion ProvideStream


        #region Read


        public static Stream OpenRead(this IStreamProvider provider, Uri uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.ProvideStream(uri).OpenRead();
        }

        public static async IAwaitable<Stream> OpenReadAsync(this IStreamProvider provider, Uri uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return await (await provider.ProvideStreamAsync(uri, cancellationToken)).OpenReadAsync(cancellationToken);
        }

        public static IAwaitable<Stream> OpenReadAsync(this IStreamProvider provider, Uri uri) =>
            provider.OpenReadAsync(uri, CancellationToken.None);


        #endregion Read


        #region Write


        public static Stream OpenWrite(this IStreamProvider provider, Uri uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.ProvideStream(uri).OpenWrite();
        }

        public static async IAwaitable<Stream> OpenWriteAsync(this IStreamProvider provider, Uri uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return await (await provider.ProvideStreamAsync(uri, cancellationToken)).OpenWriteAsync(cancellationToken);
        }

        public static IAwaitable<Stream> OpenWriteAsync(this IStreamProvider provider, Uri uri) =>
            provider.OpenWriteAsync(uri, CancellationToken.None);


        #endregion Write


        #region Delete


        public static bool Delete(this IStreamProvider provider, Uri uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.ProvideStream(uri).Delete();
        }

        public static async IAwaitable<bool> DeleteAsync(this IStreamProvider provider, Uri uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return await (await provider.ProvideStreamAsync(uri, cancellationToken)).DeleteAsync(cancellationToken);
        }

        public static IAwaitable<bool> DeleteAsync(this IStreamProvider provider, Uri uri) =>
            provider.DeleteAsync(uri, CancellationToken.None);


        #endregion Delete


    }
}
