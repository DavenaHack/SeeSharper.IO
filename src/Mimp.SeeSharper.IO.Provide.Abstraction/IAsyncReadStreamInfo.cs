using Mimp.SeeSharper.Async.Abstraction;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mimp.SeeSharper.IO.Provide.Abstraction
{
    public interface IAsyncReadStreamInfo : IStreamInfo
    {


        public IAwaitable<Stream> OpenReadAsync(CancellationToken cancellationToken);


    }
}
