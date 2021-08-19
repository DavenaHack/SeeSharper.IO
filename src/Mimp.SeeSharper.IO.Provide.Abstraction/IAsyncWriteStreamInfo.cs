using Mimp.SeeSharper.Async.Abstraction;
using System.IO;
using System.Threading;

namespace Mimp.SeeSharper.IO.Provide.Abstraction
{
    public interface IAsyncWriteStreamInfo : IStreamInfo
    {


        public IAwaitable<Stream> OpenWriteAsync(CancellationToken cancellationToken);


    }
}
