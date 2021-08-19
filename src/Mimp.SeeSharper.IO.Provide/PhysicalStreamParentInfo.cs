using Mimp.SeeSharper.IO.Provide.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mimp.SeeSharper.IO.Provide
{
    public class PhysicalStreamParentInfo : IStreamParentInfo
    {


        public Uri Uri { get; }

        public Uri? ParentUri { get; }


        public IEnumerable<Uri> Streams { get; }

        public IEnumerable<Uri> Children { get; }


        public string Path { get; }

        public string Name { get; }

        public bool Exists { get; }

        public DateTime CreationTime { get; }

        public DateTime LastWriteTime { get; }


        public PhysicalStreamParentInfo(Uri uri, DirectoryInfo directoryInfo)
        {
            if (directoryInfo is null)
                throw new ArgumentNullException(nameof(directoryInfo));
            if (File.Exists(directoryInfo.FullName))
                throw new ArgumentException($"{directoryInfo.FullName} isn't a directory.", nameof(directoryInfo));

            Uri = uri ?? throw new ArgumentNullException(nameof(uri));
            if (uri.Scheme != "file")
                throw ProvideIOException.GetInvalidSchemeException(uri);

            ParentUri = directoryInfo.Parent?.GetUri();

            Streams = directoryInfo.GetFiles().Select(f => f.GetUri());
            Children = directoryInfo.GetDirectories().Select(d => d.GetUri());

            Path = directoryInfo.FullName;
            Name = directoryInfo.Name;
            Exists = directoryInfo.Exists;
            CreationTime = directoryInfo.CreationTimeUtc;
            LastWriteTime = directoryInfo.LastWriteTimeUtc;
        }


        public bool Create()
        {
            ThrowIfNoFile();
            try
            {
                if (Directory.Exists(Path))
                    return false;

                Directory.CreateDirectory(Path);
                return true;
            }
            catch (Exception ex)
            {
                throw new ProvideIOException(ex.Message, ex);
            }
        }


        public bool Delete()
        {
            ThrowIfNoFile();
            try
            {
                if (!Directory.Exists(Path))
                    return false;

                Directory.Delete(Path);
                return true;
            }
            catch (Exception ex)
            {
                throw new ProvideIOException(ex.Message, ex);
            }
        }


        protected void ThrowIfNoFile()
        {
            if (File.Exists(Path))
                throw ProvideIOException.GetIsNoLongerStreamParentException(this);
        }


        public override string ToString()
        {
            return $"{GetType()} - {Path}";
        }


    }
}
