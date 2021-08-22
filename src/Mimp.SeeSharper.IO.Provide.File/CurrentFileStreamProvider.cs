using System.IO;

namespace Mimp.SeeSharper.IO.Provide.File
{
    public class CurrentFileStreamProvider : FileStreamProvider
    {


        public CurrentFileStreamProvider()
            : base(Directory.GetCurrentDirectory()) { }


    }
}
