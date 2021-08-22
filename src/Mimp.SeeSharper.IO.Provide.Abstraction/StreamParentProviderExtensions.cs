using Mimp.SeeSharper.Async;
using Mimp.SeeSharper.Async.Abstraction;
using System;
using System.Threading;

namespace Mimp.SeeSharper.IO.Provide.Abstraction
{
    public static class StreamParentProviderExtensions
    {


        #region ProvideStreamParent


        public static IAwaitable<IStreamParentInfo> ProvideStreamParentAsync(this IStreamParentProvider provider, Uri uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider switch
            {
                IAsyncStreamParentProvider async => async.ProvideStreamParentAsync(uri, cancellationToken),
                _ => Awaitables.Run(() => provider.ProvideStreamParent(uri), cancellationToken)
            };
        }

        public static IAwaitable<IStreamParentInfo> ProvideStreamParentAsync(this IStreamParentProvider provider, Uri uri) =>
            provider.ProvideStreamParentAsync(uri, CancellationToken.None);


        public static IStreamParentInfo ProvideStreamParent(this IStreamParentProvider provider, string uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.ProvideStreamParent(new Uri(uri, UriKind.RelativeOrAbsolute));
        }

        public static IAwaitable<IStreamParentInfo> ProvideStreamParentAsync(this IStreamParentProvider provider, string uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.ProvideStreamParentAsync(new Uri(uri, UriKind.RelativeOrAbsolute), cancellationToken);
        }

        public static IAwaitable<IStreamParentInfo> ProvideStreamParentAsync(this IStreamParentProvider provider, string uri) =>
            provider.ProvideStreamParentAsync(uri, CancellationToken.None);



        #endregion ProvideStreamParent


        #region ResolveUri


        public static Uri ResolveUri(this IStreamParentProvider provider, Uri uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.Resolve(uri, null);
        }

        public static IAwaitable<Uri> ResolveUriAsync(this IStreamParentProvider provider, Uri uri, Uri? baseUri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider switch
            {
                IAsyncStreamParentProvider async => async.ResolveUriAsync(uri, baseUri, cancellationToken),
                _ => Awaitables.Run(() => provider.Resolve(uri, baseUri), cancellationToken)
            };
        }

        public static IAwaitable<Uri> ResolveUriAsync(this IStreamParentProvider provider, Uri uri, Uri? baseUri) =>
            provider.ResolveUriAsync(uri, baseUri, CancellationToken.None);

        public static IAwaitable<Uri> ResolveUriAsync(this IStreamParentProvider provider, Uri uri, CancellationToken cancellationToken) =>
            provider.ResolveUriAsync(uri, null, cancellationToken);

        public static IAwaitable<Uri> ResolveUriAsync(this IStreamParentProvider provider, Uri uri) =>
            provider.ResolveUriAsync(uri, CancellationToken.None);


        public static Uri ResolveUri(this IStreamParentProvider provider, string uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.Resolve(new Uri(uri, UriKind.RelativeOrAbsolute), null);
        }

        public static IAwaitable<Uri> ResolveUriAsync(this IStreamParentProvider provider, string uri, CancellationToken cancellationToken) =>
            provider.ResolveUriAsync(new Uri(uri, UriKind.RelativeOrAbsolute), null, cancellationToken);

        public static IAwaitable<Uri> ResolveUriAsync(this IStreamParentProvider provider, string uri) =>
            provider.ResolveUriAsync(uri, CancellationToken.None);


        #endregion ResolveUri


        #region CreateParent


        public static bool CreateParent(this IStreamParentProvider provider, Uri uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.ProvideStreamParent(uri).Create();
        }

        public static async IAwaitable<bool> CreateParentAsync(this IStreamParentProvider provider, Uri uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return await (await provider.ProvideStreamParentAsync(uri, cancellationToken)).CreateAsync(cancellationToken);
        }

        public static IAwaitable<bool> CreateParentAsync(this IStreamParentProvider provider, Uri uri) =>
            provider.CreateParentAsync(uri, CancellationToken.None);


        public static bool CreateParent(this IStreamParentProvider provider, string uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.CreateParent(new Uri(uri, UriKind.RelativeOrAbsolute));
        }

        public static IAwaitable<bool> CreateParentAsync(this IStreamParentProvider provider, string uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.CreateParentAsync(new Uri(uri, UriKind.RelativeOrAbsolute), cancellationToken);
        }

        public static IAwaitable<bool> CreateParentAsync(this IStreamParentProvider provider, string uri) =>
            provider.CreateParentAsync(uri, CancellationToken.None);



        #endregion CreateParent


        #region DeleteParent


        public static bool DeleteParent(this IStreamParentProvider provider, Uri uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.ProvideStreamParent(uri).Delete();
        }

        public static async IAwaitable<bool> DeleteParentAsync(this IStreamParentProvider provider, Uri uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return await (await provider.ProvideStreamParentAsync(uri, cancellationToken)).DeleteAsync(cancellationToken);
        }

        public static IAwaitable<bool> DeleteParentAsync(this IStreamParentProvider provider, Uri uri) =>
            provider.DeleteParentAsync(uri, CancellationToken.None);


        public static bool DeleteParent(this IStreamParentProvider provider, string uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.DeleteParent(new Uri(uri, UriKind.RelativeOrAbsolute));
        }

        public static IAwaitable<bool> DeleteParentAsync(this IStreamParentProvider provider, string uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.DeleteParentAsync(new Uri(uri, UriKind.RelativeOrAbsolute), cancellationToken);
        }

        public static IAwaitable<bool> DeleteParentAsync(this IStreamParentProvider provider, string uri) =>
            provider.DeleteParentAsync(uri, CancellationToken.None);



        #endregion DeleteParent


    }
}
