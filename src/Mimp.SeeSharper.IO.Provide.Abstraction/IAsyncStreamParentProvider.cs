using Mimp.SeeSharper.Async.Abstraction;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mimp.SeeSharper.IO.Provide.Abstraction
{
    public interface IAsyncStreamParentProvider
    {


        public IAwaitable<IStreamParentInfo> ProvideStreamParentAsync(Uri uri, CancellationToken cancellationToken);

        public IAwaitable<Uri> ResolveUriAsync(Uri uri, Uri? baseUri, CancellationToken cancellationToken);


    }
}
