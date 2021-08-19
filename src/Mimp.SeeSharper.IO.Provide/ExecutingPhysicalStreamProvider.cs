using System;
using System.IO;
using System.Reflection;

namespace Mimp.SeeSharper.IO.Provide
{
    public class ExecutingPhysicalStreamProvider : PhysicalStreamProvider
    {


        public ExecutingPhysicalStreamProvider()
            : base(Path.GetDirectoryName(Assembly.GetExecutingAssembly()!.Location)!) { }


    }
}
