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
                _ => provider.ProvideStreamParentAsync(uri).Await()
            };
        }

        public static IAwaitable<IStreamParentInfo> ProvideStreamParentAsync(this IAsyncStreamParentProvider provider, Uri uri) =>
            provider.ProvideStreamParentAsync(uri, CancellationToken.None);


        public static IStreamParentInfo ProvideStreamParent(this IAsyncStreamParentProvider provider, string uri) =>
            provider.ProvideStreamParent(new Uri(uri, UriKind.RelativeOrAbsolute));


        public static IAwaitable<IStreamParentInfo> ProvideStreamParentAsync(this IAsyncStreamParentProvider provider, string uri, CancellationToken cancellationToken) =>
            provider.ProvideStreamParentAsync(new Uri(uri, UriKind.RelativeOrAbsolute), cancellationToken);

        public static IAwaitable<IStreamParentInfo> ProvideStreamParentAsync(this IAsyncStreamParentProvider provider, string uri) =>
            provider.ProvideStreamParentAsync(uri, CancellationToken.None);


        #endregion ProvideStreamParent


        #region ResolveUri

        public static Uri ResolveUri(this IAsyncStreamParentProvider provider, Uri uri, Uri? baseUri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider switch
            {
                IStreamParentProvider sync => sync.Resolve(uri, baseUri),
                _ => provider.ResolveUriAsync(uri, baseUri).Await()
            };
        }

        public static Uri ResolveUri(this IAsyncStreamParentProvider provider, Uri uri) =>
            provider.ResolveUri(uri, null);

        public static IAwaitable<Uri> ResolveUriAsync(this IAsyncStreamParentProvider provider, Uri uri, Uri? baseUri) =>
            provider.ResolveUriAsync(uri, baseUri, CancellationToken.None);

        public static IAwaitable<Uri> ResolveUriAsync(this IAsyncStreamParentProvider provider, Uri uri, CancellationToken cancellationToken) =>
            provider.ResolveUriAsync(uri, null, cancellationToken);

        public static IAwaitable<Uri> ResolveUriAsync(this IAsyncStreamParentProvider provider, Uri uri) =>
            provider.ResolveUriAsync(uri, CancellationToken.None);


        public static Uri ResolveUri(this IAsyncStreamParentProvider provider, string uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.ResolveUri(new Uri(uri, UriKind.RelativeOrAbsolute), null);
        }

        public static IAwaitable<Uri> ResolveUriAsync(this IAsyncStreamParentProvider provider, string uri, CancellationToken cancellationToken) =>
            provider.ResolveUriAsync(new Uri(uri, UriKind.RelativeOrAbsolute), null, cancellationToken);

        public static IAwaitable<Uri> ResolveUriAsync(this IAsyncStreamParentProvider provider, string uri) =>
            provider.ResolveUriAsync(uri, CancellationToken.None);



        #endregion ResolveUri


        #region CreateParent


        public static bool CreateParent(this IAsyncStreamParentProvider provider, Uri uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.ProvideStreamParent(uri).Create();
        }

        public static async IAwaitable<bool> CreateParentAsync(this IAsyncStreamParentProvider provider, Uri uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return await (await provider.ProvideStreamParentAsync(uri, cancellationToken)).CreateAsync(cancellationToken);
        }

        public static IAwaitable<bool> CreateParentAsync(this IAsyncStreamParentProvider provider, Uri uri) =>
            provider.CreateParentAsync(uri, CancellationToken.None);


        public static bool CreateParent(this IAsyncStreamParentProvider provider, string uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.CreateParent(new Uri(uri, UriKind.RelativeOrAbsolute));
        }

        public static IAwaitable<bool> CreateParentAsync(this IAsyncStreamParentProvider provider, string uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.CreateParentAsync(new Uri(uri, UriKind.RelativeOrAbsolute), cancellationToken);
        }

        public static IAwaitable<bool> CreateParentAsync(this IAsyncStreamParentProvider provider, string uri) =>
            provider.CreateParentAsync(uri, CancellationToken.None);



        #endregion CreateParent


        #region DeleteParent


        public static bool DeleteParent(this IAsyncStreamParentProvider provider, Uri uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.ProvideStreamParent(uri).Delete();
        }

        public static async IAwaitable<bool> DeleteParentAsync(this IAsyncStreamParentProvider provider, Uri uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return await (await provider.ProvideStreamParentAsync(uri, cancellationToken)).DeleteAsync(cancellationToken);
        }

        public static IAwaitable<bool> DeleteParentAsync(this IAsyncStreamParentProvider provider, Uri uri) =>
            provider.DeleteParentAsync(uri, CancellationToken.None);


        public static bool DeleteParent(this IAsyncStreamParentProvider provider, string uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.DeleteParent(new Uri(uri, UriKind.RelativeOrAbsolute));
        }

        public static IAwaitable<bool> DeleteParentAsync(this IAsyncStreamParentProvider provider, string uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.DeleteParentAsync(new Uri(uri, UriKind.RelativeOrAbsolute), cancellationToken);
        }

        public static IAwaitable<bool> DeleteParentAsync(this IAsyncStreamParentProvider provider, string uri) =>
            provider.DeleteParentAsync(uri, CancellationToken.None);



        #endregion DeleteParent


    }
}
