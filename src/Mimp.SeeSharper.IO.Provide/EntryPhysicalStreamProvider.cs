using System;
using System.IO;
using System.Reflection;

namespace Mimp.SeeSharper.IO.Provide
{
    public class EntryPhysicalStreamProvider : PhysicalStreamProvider
    {


        public EntryPhysicalStreamProvider()
            : base(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!) { }


    }
}
