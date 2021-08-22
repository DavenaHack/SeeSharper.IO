using Mimp.SeeSharper.Async;
using Mimp.SeeSharper.Async.Abstraction;
using System;
using System.IO;
using System.Threading;

namespace Mimp.SeeSharper.IO.Provide.Abstraction
{
    public static class AsyncStreamProviderExtensions
    {


        #region ProvideStream


        public static IStreamInfo ProvideStream(this IAsyncStreamProvider provider, Uri uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider switch
            {
                IStreamProvider sync => sync.ProvideStream(uri),
                _ => provider.ProvideStreamAsync(uri, CancellationToken.None).Await()
            };
        }

        public static IAwaitable<IStreamInfo> ProvideStreamAsync(this IAsyncStreamProvider provider, Uri uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.ProvideStreamAsync(uri, CancellationToken.None);
        }


        public static IStreamInfo ProvideStream(this IAsyncStreamProvider provider, string uri) =>
            provider.ProvideStream(new Uri(uri, UriKind.RelativeOrAbsolute));

        public static IAwaitable<IStreamInfo> ProvideStreamAsync(this IAsyncStreamProvider provider, string uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.ProvideStreamAsync(new Uri(uri, UriKind.RelativeOrAbsolute), cancellationToken);
        }

        public static IAwaitable<IStreamInfo> ProvideStreamAsync(this IAsyncStreamProvider provider, string uri) =>
            provider.ProvideStreamAsync(uri, CancellationToken.None);


        #endregion ProvideStream


        #region Read


        public static Stream OpenRead(this IAsyncStreamProvider provider, Uri uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.ProvideStream(uri).OpenRead();
        }

        public static async IAwaitable<Stream> OpenReadAsync(this IAsyncStreamProvider provider, Uri uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return await (await provider.ProvideStreamAsync(uri, cancellationToken)).OpenReadAsync(cancellationToken);
        }

        public static IAwaitable<Stream> OpenReadAsync(this IAsyncStreamProvider provider, Uri uri) =>
            provider.OpenReadAsync(uri, CancellationToken.None);


        public static Stream OpenRead(this IAsyncStreamProvider provider, string uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.OpenRead(new Uri(uri, UriKind.RelativeOrAbsolute));
        }

        public static IAwaitable<Stream> OpenReadAsync(this IAsyncStreamProvider provider, string uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.OpenReadAsync(new Uri(uri, UriKind.RelativeOrAbsolute), cancellationToken);
        }

        public static IAwaitable<Stream> OpenReadAsync(this IAsyncStreamProvider provider, string uri) =>
            provider.OpenReadAsync(uri, CancellationToken.None);


        #endregion Read


        #region Write


        public static Stream OpenWrite(this IAsyncStreamProvider provider, Uri uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.ProvideStream(uri).OpenWrite();
        }

        public static async IAwaitable<Stream> OpenWriteAsync(this IAsyncStreamProvider provider, Uri uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return await (await provider.ProvideStreamAsync(uri, cancellationToken)).OpenWriteAsync(cancellationToken);
        }

        public static IAwaitable<Stream> OpenWriteAsync(this IAsyncStreamProvider provider, Uri uri) =>
            provider.OpenWriteAsync(uri, CancellationToken.None);


        public static Stream OpenWrite(this IAsyncStreamProvider provider, string uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.OpenWrite(new Uri(uri, UriKind.RelativeOrAbsolute));
        }

        public static IAwaitable<Stream> OpenWriteAsync(this IAsyncStreamProvider provider, string uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.OpenWriteAsync(new Uri(uri, UriKind.RelativeOrAbsolute), cancellationToken);
        }

        public static IAwaitable<Stream> OpenWriteAsync(this IAsyncStreamProvider provider, string uri) =>
            provider.OpenWriteAsync(uri, CancellationToken.None);


        #endregion Write


        #region Delete


        public static bool Delete(this IAsyncStreamProvider provider, Uri uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.ProvideStream(uri).Delete();
        }

        public static async IAwaitable<bool> DeleteAsync(this IAsyncStreamProvider provider, Uri uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return await (await provider.ProvideStreamAsync(uri, cancellationToken)).DeleteAsync(cancellationToken);
        }

        public static IAwaitable<bool> DeleteAsync(this IAsyncStreamProvider provider, Uri uri) =>
            provider.DeleteAsync(uri, CancellationToken.None);


        public static bool Delete(this IAsyncStreamProvider provider, string uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.Delete(new Uri(uri, UriKind.RelativeOrAbsolute));
        }

        public static IAwaitable<bool> DeleteAsync(this IAsyncStreamProvider provider, string uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.DeleteAsync(new Uri(uri, UriKind.RelativeOrAbsolute), cancellationToken);
        }

        public static IAwaitable<bool> DeleteAsync(this IAsyncStreamProvider provider, string uri) =>
            provider.DeleteAsync(uri, CancellationToken.None);


        #endregion Delete


    }
}
