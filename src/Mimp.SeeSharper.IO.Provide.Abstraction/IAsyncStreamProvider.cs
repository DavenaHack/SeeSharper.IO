using Mimp.SeeSharper.Async.Abstraction;
using System;
using System.Threading;

namespace Mimp.SeeSharper.IO.Provide.Abstraction
{
    public interface IAsyncStreamProvider
    {


        public IAwaitable<IStreamInfo> ProvideStreamAsync(Uri uri, CancellationToken cancellationToken);


    }
}
