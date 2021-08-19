using System;

namespace Mimp.SeeSharper.IO.Provide.Abstraction
{
    public interface IChildStreamInfo : IStreamInfo
    {


        public Uri ParentUri { get; }


    }
}
