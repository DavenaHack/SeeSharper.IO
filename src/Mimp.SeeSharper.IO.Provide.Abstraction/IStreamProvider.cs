using System;

namespace Mimp.SeeSharper.IO.Provide.Abstraction
{
    public interface IStreamProvider
    {


        public IStreamInfo ProvideStream(Uri uri);


    }
}
