using System;
using System.Collections.Generic;

namespace Mimp.SeeSharper.IO.Provide.Abstraction
{
    public interface IStreamParentInfo
    {


        public Uri Uri { get; }

        public Uri? ParentUri { get; }


        public IEnumerable<Uri> Streams { get; }

        public IEnumerable<Uri> Children { get; }


    }
}
