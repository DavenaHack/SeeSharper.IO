using Mimp.SeeSharper.Async;
using Mimp.SeeSharper.Async.Abstraction;
using System;
using System.Collections.Generic;
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


        #region GetChildren


        public static IEnumerable<Uri> GetChildren(this IAsyncStreamParentProvider provider, Uri uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.ProvideStreamParent(uri).GetChildren();
        }

        public static async IAwaitable<IEnumerable<Uri>> GetChildrenAsync(this IAsyncStreamParentProvider provider, Uri uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return (await provider.ProvideStreamParentAsync(uri, cancellationToken)).GetChildren();
        }

        public static IAwaitable<IEnumerable<Uri>> GetChildrenAsync(this IAsyncStreamParentProvider provider, Uri uri) =>
            provider.GetChildrenAsync(uri, CancellationToken.None);


        public static IEnumerable<Uri> GetChildren(this IAsyncStreamParentProvider provider, string uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.GetChildren(new Uri(uri, UriKind.RelativeOrAbsolute));
        }

        public static IAwaitable<IEnumerable<Uri>> GetChildrenAsync(this IAsyncStreamParentProvider provider, string uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.GetChildrenAsync(new Uri(uri, UriKind.RelativeOrAbsolute), cancellationToken);
        }

        public static IAwaitable<IEnumerable<Uri>> GetChildrenAsync(this IAsyncStreamParentProvider provider, string uri) =>
            provider.GetChildrenAsync(uri, CancellationToken.None);



        #endregion GetChildren


        #region GetStreams


        public static IEnumerable<Uri> GetStreams(this IAsyncStreamParentProvider provider, Uri uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.ProvideStreamParent(uri).GetStreams();
        }

        public static async IAwaitable<IEnumerable<Uri>> GetStreamsAsync(this IAsyncStreamParentProvider provider, Uri uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return (await provider.ProvideStreamParentAsync(uri, cancellationToken)).GetStreams();
        }

        public static IAwaitable<IEnumerable<Uri>> GetStreamsAsync(this IAsyncStreamParentProvider provider, Uri uri) =>
            provider.GetStreamsAsync(uri, CancellationToken.None);


        public static IEnumerable<Uri> GetStreams(this IAsyncStreamParentProvider provider, string uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.GetStreams(new Uri(uri, UriKind.RelativeOrAbsolute));
        }

        public static IAwaitable<IEnumerable<Uri>> GetStreamsAsync(this IAsyncStreamParentProvider provider, string uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider.GetStreamsAsync(new Uri(uri, UriKind.RelativeOrAbsolute), cancellationToken);
        }

        public static IAwaitable<IEnumerable<Uri>> GetStreamsAsync(this IAsyncStreamParentProvider provider, string uri) =>
            provider.GetStreamsAsync(uri, CancellationToken.None);



        #endregion GetStreams


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


        #region ProivdeStreamParentRecursive


        public static IEnumerable<IStreamParentInfo> ProvideChildrenRecursive(this IAsyncStreamParentProvider provider, Uri uri, bool root)
        {

            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider switch
            {
                IStreamParentProvider sync => sync.ProvideChildrenRecursive(uri, root),
                _ => provider.ProvideChildrenRecursiveAsync(uri, root).Await()
            };
        }

        public static IEnumerable<IStreamParentInfo> ProvideChildrenRecursive(this IAsyncStreamParentProvider provider, Uri uri) =>
            provider.ProvideChildrenRecursive(uri, true);


        public static IAwaitableEnumerable<IStreamParentInfo> ProvideChildrenRecursiveAsync(this IAsyncStreamParentProvider provider, Uri uri, bool root, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return Awaitables.Yield(async (Func<IAwaitable<IStreamParentInfo>, IAwaitable> yield, CancellationToken token) =>
            {
                cancellationToken = CancellationTokenSource.CreateLinkedTokenSource(token, cancellationToken).Token;

                var parentAwait = provider.ProvideStreamParentAsync(uri, cancellationToken);
                if (root)
                    await yield(parentAwait);
                var parent = await parentAwait;

                foreach (var child in parent.GetChildren())
                {
                    var descendants = provider.ProvideChildrenRecursiveAsync(child, root, cancellationToken).GetAwaiter(cancellationToken);
                    while (await descendants.AwaitNextAsync(cancellationToken))
                        await yield(descendants.GetNextAsync(cancellationToken));
                }
            });
        }

        public static IAwaitableEnumerable<IStreamParentInfo> ProvideChildrenRecursiveAsync(this IAsyncStreamParentProvider provider, Uri uri, bool root) =>
            provider.ProvideChildrenRecursiveAsync(uri, root, CancellationToken.None);

        public static IAwaitableEnumerable<IStreamParentInfo> ProvideChildrenRecursiveAsync(this IAsyncStreamParentProvider provider, Uri uri, CancellationToken cancellationToken) =>
            provider.ProvideChildrenRecursiveAsync(uri, true, cancellationToken);

        public static IAwaitableEnumerable<IStreamParentInfo> ProvideChildrenRecursiveAsync(this IAsyncStreamParentProvider provider, Uri uri) =>
            provider.ProvideChildrenRecursiveAsync(uri, true);



        public static IEnumerable<IStreamParentInfo> ProvideChildrenRecursive(this IAsyncStreamParentProvider provider, string uri, bool root) =>
            provider.ProvideChildrenRecursive(new Uri(uri, UriKind.RelativeOrAbsolute), root);

        public static IEnumerable<IStreamParentInfo> ProvideChildrenRecursive(this IAsyncStreamParentProvider provider, string uri) =>
            provider.ProvideChildrenRecursive(uri, true);


        public static IAwaitableEnumerable<IStreamParentInfo> ProvideChildrenRecursiveAsync(this IAsyncStreamParentProvider provider, string uri, bool root, CancellationToken cancellationToken) =>
            provider.ProvideChildrenRecursiveAsync(new Uri(uri, UriKind.RelativeOrAbsolute), root, cancellationToken);

        public static IAwaitableEnumerable<IStreamParentInfo> ProvideChildrenRecursiveAsync(this IAsyncStreamParentProvider provider, string uri, bool root) =>
            provider.ProvideChildrenRecursiveAsync(uri, root, CancellationToken.None);

        public static IAwaitableEnumerable<IStreamParentInfo> ProvideChildrenRecursiveAsync(this IAsyncStreamParentProvider provider, string uri, CancellationToken cancellationToken) =>
            provider.ProvideChildrenRecursiveAsync(uri, true, cancellationToken);

        public static IAwaitableEnumerable<IStreamParentInfo> ProvideChildrenRecursiveAsync(this IAsyncStreamParentProvider provider, string uri) =>
            provider.ProvideChildrenRecursiveAsync(uri, true);


        #endregion ProivdeStreamParentRecursive


        #region GetStreamsRecursive


        public static IEnumerable<Uri> GetStreamsRecursive(this IAsyncStreamParentProvider provider, Uri uri)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return provider switch
            {
                IStreamParentProvider sync => sync.GetStreamsRecursive(uri),
                _ => provider.GetStreamsRecursiveAsync(uri).Await()
            };
        }

        public static IAwaitableEnumerable<Uri> GetStreamsRecursiveAsync(this IAsyncStreamParentProvider provider, Uri uri, CancellationToken cancellationToken)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            return Awaitables.Yield(async (Func<Uri, IAwaitable> yield, CancellationToken token) =>
            {
                cancellationToken = CancellationTokenSource.CreateLinkedTokenSource(token, cancellationToken).Token;


                await foreach (var parent in provider.ProvideChildrenRecursiveAsync(uri, cancellationToken))
                    foreach (var stream in parent.GetStreams())
                        await yield(stream);
            });
        }

        public static IAwaitableEnumerable<Uri> GetStreamsRecursiveAsync(this IAsyncStreamParentProvider provider, Uri uri) =>
            provider.GetStreamsRecursiveAsync(uri, CancellationToken.None);



        public static IEnumerable<Uri> GetStreamsRecursive(this IAsyncStreamParentProvider provider, string uri) =>
            provider.GetStreamsRecursive(new Uri(uri, UriKind.RelativeOrAbsolute));


        public static IAwaitableEnumerable<Uri> GetStreamsRecursiveAsync(this IAsyncStreamParentProvider provider, string uri, CancellationToken cancellationToken) =>
            provider.GetStreamsRecursiveAsync(new Uri(uri, UriKind.RelativeOrAbsolute), cancellationToken);

        public static IAwaitableEnumerable<Uri> GetStreamsRecursiveAsync(this IAsyncStreamParentProvider provider, string uri) =>
            provider.GetStreamsRecursiveAsync(uri, CancellationToken.None);


        #endregion GetStreamsRecursive

    }
}
