using Mimp.SeeSharper.Async.Abstraction;
using System;
using System.IO;
using System.Threading;

namespace Mimp.SeeSharper.IO.Provide.Abstraction
{
    public static class StreamSystemProviderExtensions
    {


        #region ProvideStream


        public static IStreamInfo ProvideStream(this IStreamSystemProvider provider, string uri) =>
            provider.ProvideStream(provider.ResolveUri(uri));


        public static async IAwaitable<IStreamInfo> ProvideStreamAsync(this IStreamSystemProvider provider, string uri, CancellationToken cancellationToken) =>
            await provider.ProvideStreamAsync(await provider.ResolveUriAsync(uri, cancellationToken), cancellationToken);

        public static IAwaitable<IStreamInfo> ProvideStreamAsync(this IStreamSystemProvider provider, string uri) =>
            provider.ProvideStreamAsync(uri, CancellationToken.None);


        #endregion ProvideStream


        #region Read


        public static Stream OpenRead(this IStreamSystemProvider provider, string uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.ProvideStream(uri).OpenRead();
        }

        public static async IAwaitable<Stream> OpenReadAsync(this IStreamSystemProvider provider, string uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return await (await provider.ProvideStreamAsync(uri, cancellationToken)).OpenReadAsync(cancellationToken);
        }

        public static IAwaitable<Stream> OpenReadAsync(this IStreamSystemProvider provider, string uri) =>
            provider.OpenReadAsync(uri, CancellationToken.None);


        #endregion Read


        #region Write


        public static Stream OpenWrite(this IStreamSystemProvider provider, string uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.ProvideStream(uri).OpenWrite();
        }

        public static async IAwaitable<Stream> OpenWriteAsync(this IStreamSystemProvider provider, string uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return await (await provider.ProvideStreamAsync(uri, cancellationToken)).OpenWriteAsync(cancellationToken);
        }

        public static IAwaitable<Stream> OpenWriteAsync(this IStreamSystemProvider provider, string uri) =>
            provider.OpenWriteAsync(uri, CancellationToken.None);


        #endregion Write


        #region Delete


        public static bool Delete(this IStreamSystemProvider provider, string uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.ProvideStream(uri).Delete();
        }

        public static async IAwaitable<bool> DeleteAsync(this IStreamSystemProvider provider, string uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return await (await provider.ProvideStreamAsync(uri, cancellationToken)).DeleteAsync(cancellationToken);
        }

        public static IAwaitable<bool> DeleteAsync(this IStreamSystemProvider provider, string uri) =>
            provider.DeleteAsync(uri, CancellationToken.None);


        #endregion Delete


    }
}
