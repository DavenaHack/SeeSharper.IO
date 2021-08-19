using Mimp.SeeSharper.Async;
using Mimp.SeeSharper.Async.Abstraction;
using System;
using System.Threading;

namespace Mimp.SeeSharper.IO.Provide.Abstraction
{
    public static class AsyncStreamParentProviderExtensions
    {


        #region ProvideStreamParent


        public static IStreamParentInfo ProvideStreamParent(this IAsyncStreamParentProvider provider, Uri uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider switch
            {
                IStreamParentProvider sync => sync.ProvideStreamParent(uri),
                _ => provider.ProvideStreamParentAsync(uri, CancellationToken.None).Await()
            };
        }

        public static IAwaitable<IStreamParentInfo> ProvideStreamParentAsync(this IAsyncStreamParentProvider provider, Uri uri) =>
            provider.ProvideStreamParentAsync(uri, CancellationToken.None);


        public static IStreamParentInfo ProvideStreamParent(this IAsyncStreamParentProvider provider, string uri) =>
            provider.ProvideStreamParent(provider.ResolveUri(uri));


        public static async IAwaitable<IStreamParentInfo> ProvideStreamParentAsync(this IAsyncStreamParentProvider provider, string uri, CancellationToken cancellationToken) =>
            await provider.ProvideStreamParentAsync(await provider.ResolveUriAsync(uri, cancellationToken), cancellationToken);

        public static IAwaitable<IStreamParentInfo> ProvideStreamParentAsync(this IAsyncStreamParentProvider provider, string uri) =>
            provider.ProvideStreamParentAsync(uri, CancellationToken.None);


        #endregion ProvideStreamParent


        #region ResolveUri


        public static IAwaitable<Uri> ResolveUriAsync(this IAsyncStreamParentProvider provider, string uri, Uri? baseUri) =>
            provider.ResolveUriAsync(uri, baseUri, CancellationToken.None);


        public static IAwaitable<Uri> ResolveUriAsync(this IAsyncStreamParentProvider provider, string uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.ResolveUriAsync(uri, null, cancellationToken);
        }

        public static IAwaitable<Uri> ResolveUriAsync(this IAsyncStreamParentProvider provider, string uri) =>
            provider.ResolveUriAsync(uri, CancellationToken.None);


        public static Uri ResolveUri(this IAsyncStreamParentProvider provider, string uri, Uri? baseUri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider switch
            {
                IStreamParentProvider sync => sync.ResolveUri(uri, baseUri),
                _ => provider.ResolveUriAsync(uri, baseUri, CancellationToken.None).Await()
            };
        }

        public static Uri ResolveUri(this IAsyncStreamParentProvider provider, string uri) =>
            provider.ResolveUri(uri, null);


        #endregion ResolveUri


        #region CreateParent


        public static bool CreateParent(this IAsyncStreamParentProvider provider, Uri uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            return provider.ProvideStreamParent(uri).Create();
        }

        public static async IAwaitable<bool> CreateParentAsync(this IAsyncStreamParentProvider provider, Uri uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            return await (await provider.ProvideStreamParentAsync(uri, cancellationToken)).CreateAsync(cancellationToken);
        }


        #endregion CreateParent


        #region DeleteParent


        public static bool DeleteParent(this IAsyncStreamParentProvider provider, Uri uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            return provider.ProvideStreamParent(uri).Delete();
        }

        public static async IAwaitable<bool> DeleteParentAsync(this IAsyncStreamParentProvider provider, Uri uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            return await (await provider.ProvideStreamParentAsync(uri, cancellationToken)).DeleteAsync(cancellationToken);
        }


        #endregion DeleteParent


    }
}
