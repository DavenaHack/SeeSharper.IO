//using Mimp.SeeSharper.IO.Provide.Abstraction;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Mimp.SeeSharper.IO.Provide
//{
//    public class SchemeStreamProvider
//        : IStreamProvider, IAsyncStreamProvider,
//            IStreamParentProvider, IAsyncStreamParentProvider
//    {


//        public IEnumerable<KeyValuePair<string, IEnumerable<object>>> Providers => _providers;

//        private IDictionary<string, IEnumerable<object>> _providers;

//        public SchemeStreamProvider(IEnumerable<KeyValuePair<string, IEnumerable<object>>> providers)
//        {
//            _providers = providers
//                ?.GroupBy(
//                    p => p.Key is null ? throw new ArgumentNullException(nameof(providers), "At least one scheme is null.")
//                        : Uri.CheckSchemeName(p.Key) ? throw ProvideIOException.GetInvalidSchemeException(p.Key)
//                        : p.Key.ToLower(),
//                    (scheme, pairs) => new KeyValuePair<string, IEnumerable<object>>(scheme,
//                        pairs.SelectMany(p => p.Value?.Select(provider =>
//                            provider is null ? throw new ArgumentNullException(nameof(providers), "At least one provider is null.")
//                            : provider is not IStreamProvider
//                                && provider is not IAsyncStreamProvider
//                                && provider is not IStreamParentProvider
//                                && provider is not IAsyncStreamParentProvider
//                            ? throw new ArgumentException($"{provider} is neither a {nameof(IStreamProvider)}," +
//                                $" {nameof(IAsyncStreamProvider)}, {nameof(IStreamParentProvider)}" +
//                                $" nor {nameof(IAsyncStreamParentProvider)}", nameof(providers))
//                            : provider
//                        ).ToArray() ?? throw new ArgumentNullException(nameof(providers), "At least one provider is null."))
//                    )
//                ).ToDictionary(p => p.Key, p => p.Value) ?? throw new ArgumentNullException(nameof(providers));
//        }

//        public SchemeStreamProvider(IEnumerable<KeyValuePair<string, object>> providers)
//            : this(providers?.Select(p => new KeyValuePair<string, IEnumerable<object>>(p.Key, new[] { p.Value }))
//                  ?? throw new ArgumentNullException(nameof(providers)))
//        { }

//        public SchemeStreamProvider(IEnumerable<KeyValuePair<object, IEnumerable<string>>> providers)
//            : this(providers?.SelectMany(p =>
//                    p.Value is null || !p.Value.Any() ? throw new ArgumentNullException(nameof(providers), "At least one provider has no schemes")
//                    : p.Value.Select(s => new KeyValuePair<string, object>(s, p.Key)))
//                ?? throw new ArgumentNullException(nameof(providers)))
//        { }

//        public IStreamInfo ProvideStream(Uri uri)
//        {
//            if (uri is null)
//                throw new ArgumentNullException(nameof(uri));

//            if (!_providers.TryGetValue(uri.Scheme, out var providers))
//                throw ProvideIOException.GetInvalidSchemeException(uri);


//            throw new ProvideIOException($"Can't provide stream.", new AggregateException(exceptions));
//        }

//        public async Task<IStreamInfo> ProvideStreamAsync(Uri uri, CancellationToken cancellationToken)
//        {
//            if (uri is null)
//                throw new ArgumentNullException(nameof(uri));

//            if (!_providers.TryGetValue(uri.Scheme, out var providers))
//                throw ProvideIOException.GetInvalidSchemeException(uri);

//        }


//        public IStreamParentInfo ProvideStreamParent(Uri uri)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IStreamParentInfo> ProvideStreamParentAsync(Uri uri, CancellationToken cancellationToken)
//        {
//            throw new NotImplementedException();
//        }

//        public Uri ResolveUri(Uri uri, Uri? baseUri)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Uri> ResolveUriAsync(Uri uri, Uri? baseUri, CancellationToken cancellationToken)
//        {
//            throw new NotImplementedException();
//        }


//    }
//}
