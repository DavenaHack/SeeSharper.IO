using Mimp.SeeSharper.Async.Abstraction;
using System.Threading;
using System.Threading.Tasks;

namespace Mimp.SeeSharper.IO.Provide.Abstraction
{
    public interface IAsyncCreateStreamParentInfo : IStreamParentInfo
    {


        public IAwaitable<bool> CreateAsync(CancellationToken cancellationToken);


    }
}
