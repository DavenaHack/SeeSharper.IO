using Mimp.SeeSharper.IO.Provide.Abstraction;
using System;
using System.IO;

namespace Mimp.SeeSharper.IO.Provide.File
{
    public class FileStreamProvider : IStreamProvider, IStreamParentProvider
    {


        public Uri BaseUri { get; }


        public FileStreamProvider(Uri baseUri)
        {
            if (baseUri is null)
                throw new ArgumentNullException(nameof(baseUri));

            if (baseUri.IsAbsoluteUri && baseUri.Scheme != "file")
                throw ProvideIOException.GetInvalidSchemeException(baseUri);
            else
                baseUri = new Uri($"file:///{Path.GetFullPath(baseUri.GetFilePath())}");

            BaseUri = baseUri;
        }

        public FileStreamProvider(string directory)
            : this(new Uri(directory, UriKind.RelativeOrAbsolute)) { }


        public IStreamInfo ProvideStream(Uri uri)
        {
            uri = Resolve(uri, null);

            try
            {
                return new FileStreamInfo(uri, new FileInfo(uri.GetFilePath()));
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
                return new FileStreamParentInfo(uri, new DirectoryInfo(uri.GetFilePath()));
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

            return StringExtensions.FilePath2AbsoluteUri(uri.OriginalString, (parentUri ?? BaseUri).GetFilePath());
        }


    }
}
