using Mimp.SeeSharper.IO.Provide.Abstraction;
using System;
using System.IO;

namespace Mimp.SeeSharper.IO.Provide
{
    public class PhysicalStreamProvider : IStreamProvider, IStreamParentProvider
    {


        public Uri BaseUri { get; }


        public PhysicalStreamProvider(Uri baseUri)
        {
            if (baseUri is null)
                throw new ArgumentNullException(nameof(baseUri));
            if (baseUri.IsAbsoluteUri && baseUri.Scheme != "file")
                throw ProvideIOException.GetInvalidSchemeException(baseUri);
            else
                baseUri = new Uri($"file:///{Path.GetFullPath(baseUri.OriginalString)}");
            BaseUri = baseUri;
        }

        public PhysicalStreamProvider(string directory)
            : this(new Uri(directory, UriKind.RelativeOrAbsolute)) { }


        public IStreamInfo ProvideStream(Uri uri)
        {
            uri = Resolve(uri, null);

            try
            {
                return new PhysicalStreamInfo(uri, new FileInfo(uri.LocalPath));
            }
            catch (Exception ex)
            {
                throw new ProvideIOException(ex.Message, ex);
            }
        }


        public IStreamParentInfo ProvideStreamParent(Uri uri)
        {
            uri = Resolve(uri, null);

            try
            {
                return new PhysicalStreamParentInfo(uri, new DirectoryInfo(uri.LocalPath));
            }
            catch (Exception ex)
            {
                throw new ProvideIOException(ex.Message, ex);
            }
        }


        public Uri Resolve(Uri uri, Uri? parentUri)
        {
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));
            if (uri.IsAbsoluteUri && uri.Scheme != "file")
                throw ProvideIOException.GetInvalidSchemeException(uri);
            if (parentUri is not null && parentUri.Scheme != "file")
                throw ProvideIOException.GetInvalidSchemeException(parentUri);

            if (uri.IsAbsoluteUri)
                return uri;

            var path = uri.OriginalString;
            if (Path.IsPathRooted(path))
                return new Uri($"file:///{Path.GetFullPath(path)}");

            return new Uri($"file:///{Path.GetFullPath(Path.Combine((parentUri ?? BaseUri).LocalPath, path))}");
        }


    }
}
