using System.IO;

namespace Mimp.SeeSharper.IO.Provide.Abstraction
{
    public interface IWriteStreamInfo : IStreamInfo
    {


        public Stream OpenWrite();


    }
}
