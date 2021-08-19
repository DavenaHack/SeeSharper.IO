using Mimp.SeeSharper.Async.Abstraction;
using System.Threading;
using System.Threading.Tasks;

namespace Mimp.SeeSharper.IO.Provide.Abstraction
{
    public interface IAsyncDeleteStreamParentInfo : IStreamParentInfo
    {


        public IAwaitable<bool> DeleteAsync(CancellationToken cancellationToken);


    }
}
