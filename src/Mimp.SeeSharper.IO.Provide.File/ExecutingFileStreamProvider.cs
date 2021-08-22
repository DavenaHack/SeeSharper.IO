using System.IO;
using System.Reflection;

namespace Mimp.SeeSharper.IO.Provide.File
{
    public class ExecutingFileStreamProvider : FileStreamProvider
    {


        public ExecutingFileStreamProvider()
            : base(Path.GetDirectoryName(Assembly.GetExecutingAssembly()!.Location)!) { }


    }
}
