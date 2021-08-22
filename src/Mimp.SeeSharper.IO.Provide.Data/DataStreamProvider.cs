using Mimp.SeeSharper.IO.Provide.Abstraction;
using System;

namespace Mimp.SeeSharper.IO.Provide.Data
{
    public class DataStreamProvider : IStreamProvider
    {


        public IStreamInfo ProvideStream(Uri uri)
        {
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));
            if (uri.Scheme != "data")
                throw ProvideIOException.GetInvalidSchemeException(uri);

            return new DataStreamInfo(uri);
        }


    }
}
