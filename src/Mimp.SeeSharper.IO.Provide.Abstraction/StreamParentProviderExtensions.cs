using Mimp.SeeSharper.Async;
using Mimp.SeeSharper.Async.Abstraction;
using System;
using System.Collections.Generic;
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


        #region GetChildren


        public static IEnumerable<Uri> GetChildren(this IStreamParentProvider provider, Uri uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.ProvideStreamParent(uri).GetChildren();
        }

        public static async IAwaitable<IEnumerable<Uri>> GetChildrenAsync(this IStreamParentProvider provider, Uri uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return (await provider.ProvideStreamParentAsync(uri, cancellationToken)).GetChildren();
        }

        public static IAwaitable<IEnumerable<Uri>> GetChildrenAsync(this IStreamParentProvider provider, Uri uri) =>
            provider.GetChildrenAsync(uri, CancellationToken.None);


        public static IEnumerable<Uri> GetChildren(this IStreamParentProvider provider, string uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.GetChildren(new Uri(uri, UriKind.RelativeOrAbsolute));
        }

        public static IAwaitable<IEnumerable<Uri>> GetChildrenAsync(this IStreamParentProvider provider, string uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.GetChildrenAsync(new Uri(uri, UriKind.RelativeOrAbsolute), cancellationToken);
        }

        public static IAwaitable<IEnumerable<Uri>> GetChildrenAsync(this IStreamParentProvider provider, string uri) =>
            provider.GetChildrenAsync(uri, CancellationToken.None);



        #endregion GetChildren


        #region GetStreams


        public static IEnumerable<Uri> GetStreams(this IStreamParentProvider provider, Uri uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.ProvideStreamParent(uri).GetStreams();
        }

        public static async IAwaitable<IEnumerable<Uri>> GetStreamsAsync(this IStreamParentProvider provider, Uri uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return (await provider.ProvideStreamParentAsync(uri, cancellationToken)).GetStreams();
        }

        public static IAwaitable<IEnumerable<Uri>> GetStreamsAsync(this IStreamParentProvider provider, Uri uri) =>
            provider.GetStreamsAsync(uri, CancellationToken.None);


        public static IEnumerable<Uri> GetStreams(this IStreamParentProvider provider, string uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.GetStreams(new Uri(uri, UriKind.RelativeOrAbsolute));
        }

        public static IAwaitable<IEnumerable<Uri>> GetStreamsAsync(this IStreamParentProvider provider, string uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.GetStreamsAsync(new Uri(uri, UriKind.RelativeOrAbsolute), cancellationToken);
        }

        public static IAwaitable<IEnumerable<Uri>> GetStreamsAsync(this IStreamParentProvider provider, string uri) =>
            provider.GetStreamsAsync(uri, CancellationToken.None);



        #endregion GetStreams


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


        #region ProvideChildrenRecursive


        public static IEnumerable<IStreamParentInfo> ProvideChildrenRecursive(this IStreamParentProvider provider, Uri uri, bool root)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            var parent = provider.ProvideStreamParent(uri);
            if (root)
                yield return parent;
            foreach (var child in parent.GetChildren())
                foreach (var descendant in provider.ProvideChildrenRecursive(child, root))
                    yield return descendant;
        }

        public static IEnumerable<IStreamParentInfo> ProvideChildrenRecursive(this IStreamParentProvider provider, Uri uri) =>
            provider.ProvideChildrenRecursive(uri, true);

        public static IAwaitableEnumerable<IStreamParentInfo> ProvideChildrenRecursiveAsync(this IStreamParentProvider provider, Uri uri, bool root, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider switch
            {
                IAsyncStreamParentProvider async => async.ProvideChildrenRecursiveAsync(uri, cancellationToken),
                _ => provider.ProvideChildrenRecursive(uri, root).ToAwaitable()
            };
        }

        public static IAwaitableEnumerable<IStreamParentInfo> ProvideChildrenRecursiveAsync(this IStreamParentProvider provider, Uri uri, bool root) =>
            provider.ProvideChildrenRecursiveAsync(uri, root, CancellationToken.None);

        public static IAwaitableEnumerable<IStreamParentInfo> ProvideChildrenRecursiveAsync(this IStreamParentProvider provider, Uri uri, CancellationToken cancellationToken) =>
            provider.ProvideChildrenRecursiveAsync(uri, true, cancellationToken);

        public static IAwaitableEnumerable<IStreamParentInfo> ProvideChildrenRecursiveAsync(this IStreamParentProvider provider, Uri uri) =>
            provider.ProvideChildrenRecursiveAsync(uri, true);



        public static IEnumerable<IStreamParentInfo> ProvideChildrenRecursive(this IStreamParentProvider provider, string uri, bool root) =>
            provider.ProvideChildrenRecursive(new Uri(uri, UriKind.RelativeOrAbsolute), root);

        public static IEnumerable<IStreamParentInfo> ProvideChildrenRecursive(this IStreamParentProvider provider, string uri) =>
            provider.ProvideChildrenRecursive(uri, true);


        public static IAwaitableEnumerable<IStreamParentInfo> ProvideChildrenRecursiveAsync(this IStreamParentProvider provider, string uri, bool root, CancellationToken cancellationToken) =>
            provider.ProvideChildrenRecursiveAsync(new Uri(uri, UriKind.RelativeOrAbsolute), root, cancellationToken);

        public static IAwaitableEnumerable<IStreamParentInfo> ProvideChildrenRecursiveAsync(this IStreamParentProvider provider, string uri, bool root) =>
            provider.ProvideChildrenRecursiveAsync(uri, root, CancellationToken.None);

        public static IAwaitableEnumerable<IStreamParentInfo> ProvideChildrenRecursiveAsync(this IStreamParentProvider provider, string uri, CancellationToken cancellationToken) =>
            provider.ProvideChildrenRecursiveAsync(uri, true, cancellationToken);

        public static IAwaitableEnumerable<IStreamParentInfo> ProvideChildrenRecursiveAsync(this IStreamParentProvider provider, string uri) =>
            provider.ProvideChildrenRecursiveAsync(uri, true);


        #endregion ProvideChildrenRecursive


        #region GetStreamsRecursive


        public static IEnumerable<Uri> GetStreamsRecursive(this IStreamParentProvider provider, Uri uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            foreach (var parent in provider.ProvideChildrenRecursive(uri))
                foreach (var stream in parent.GetStreams())
                    yield return stream;
        }

        public static IAwaitableEnumerable<Uri> GetStreamsRecursiveAsync(this IStreamParentProvider provider, Uri uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider switch
            {
                IAsyncStreamParentProvider async => async.GetStreamsRecursiveAsync(uri, cancellationToken),
                _ => provider.GetStreamsRecursive(uri).ToAwaitable()
            };
        }

        public static IAwaitableEnumerable<Uri> GetStreamsRecursiveAsync(this IStreamParentProvider provider, Uri uri) =>
            provider.GetStreamsRecursiveAsync(uri, CancellationToken.None);



        public static IEnumerable<Uri> GetStreamsRecursive(this IStreamParentProvider provider, string uri) =>
            provider.GetStreamsRecursive(new Uri(uri, UriKind.RelativeOrAbsolute));


        public static IAwaitableEnumerable<Uri> GetStreamsRecursiveAsync(this IStreamParentProvider provider, string uri, CancellationToken cancellationToken) =>
            provider.GetStreamsRecursiveAsync(new Uri(uri, UriKind.RelativeOrAbsolute), cancellationToken);

        public static IAwaitableEnumerable<Uri> GetStreamsRecursiveAsync(this IStreamParentProvider provider, string uri) =>
            provider.GetStreamsRecursiveAsync(uri, CancellationToken.None);


        #endregion GetStreamsRecursive


    }
}
