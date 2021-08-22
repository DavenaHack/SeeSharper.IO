using System;

namespace Mimp.SeeSharper.IO.Provide.Abstraction
{
    public interface IStreamParentProvider
    {


        public IStreamParentInfo ProvideStreamParent(Uri uri);

        public Uri Resolve(Uri uri, Uri? baseUri);


    }
}
