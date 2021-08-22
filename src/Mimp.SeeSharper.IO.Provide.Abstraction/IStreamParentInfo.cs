using System;
using System.Collections.Generic;

namespace Mimp.SeeSharper.IO.Provide.Abstraction
{
    public interface IStreamParentInfo
    {


        public Uri Uri { get; }

        public Uri? ParentUri { get; }


        public IEnumerable<Uri> GetStreams();

        public IEnumerable<Uri> GetChildren();


    }
}
