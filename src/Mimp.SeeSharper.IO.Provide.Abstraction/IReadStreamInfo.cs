using System.IO;

namespace Mimp.SeeSharper.IO.Provide.Abstraction
{
    public interface IReadStreamInfo : IStreamInfo
    {


        public Stream OpenRead();


    }
}
