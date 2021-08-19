using Mimp.SeeSharper.IO.Provide.Abstraction;
using System;
using System.IO;

namespace Mimp.SeeSharper.IO.Provide
{
    public class PhysicalStreamProvider : IStreamSystemProvider
    {


        public Uri BaseUri { get; }


        public PhysicalStreamProvider(Uri baseUri)
        {
            BaseUri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
            if (baseUri.Scheme != "file")
                throw ProvideIOException.GetInvalidSchemeException(baseUri);
        }

        public PhysicalStreamProvider(string directory)
            : this(new Uri("file://" + Path.GetFullPath(directory))) { }


        public IStreamInfo ProvideStream(Uri uri)
        {
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));
            if (uri.Scheme != "file")
                throw ProvideIOException.GetInvalidSchemeException(uri);

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
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));
            if (uri.Scheme != "file")
                throw ProvideIOException.GetInvalidSchemeException(uri);

            try
            {
                return new PhysicalStreamParentInfo(uri, new DirectoryInfo(uri.LocalPath));
            }
            catch (Exception ex)
            {
                throw new ProvideIOException(ex.Message, ex);
            }
        }


        public Uri ResolveUri(string uri, Uri? parentUri)
        {
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));
            if (parentUri is not null && parentUri.Scheme != "file")
                throw ProvideIOException.GetInvalidSchemeException(parentUri);

            uri = uri.Trim();
            if (uri.ToLower().StartsWith("file://"))
                uri = uri.Substring("file://".Length);

            if (Path.IsPathRooted(uri))
                return new Uri($"file://{uri}");

            return new Uri(Path.Combine((parentUri ?? BaseUri).LocalPath, uri));
        }

    }
}
