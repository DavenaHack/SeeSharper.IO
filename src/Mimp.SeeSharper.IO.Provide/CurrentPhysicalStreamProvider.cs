using System;
using System.IO;

namespace Mimp.SeeSharper.IO.Provide
{
    public class CurrentPhysicalStreamProvider : PhysicalStreamProvider
    {


        public CurrentPhysicalStreamProvider()
            : base(Directory.GetCurrentDirectory()) { }


    }
}
