using Mimp.SeeSharper.Async.Abstraction;
using System.Threading;

namespace Mimp.SeeSharper.IO.Provide.Abstraction
{
    public interface IAsyncDeleteStreamInfo : IStreamInfo
    {


        public IAwaitable<bool> DeleteAsync(CancellationToken cancellationToken);


    }
}
