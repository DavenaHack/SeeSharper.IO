using System.IO;
using System.Reflection;

namespace Mimp.SeeSharper.IO.Provide.File
{
    public class EntryFileStreamProvider : FileStreamProvider
    {


        public EntryFileStreamProvider()
            : base(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!) { }


    }
}
