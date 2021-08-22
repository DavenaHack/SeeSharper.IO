using Mimp.SeeSharper.IO.Provide.Abstraction;
using System;
using System.IO;

namespace Mimp.SeeSharper.IO.Provide.File
{
    public class FileStreamInfo
        : IStreamInfo, IChildStreamInfo, IReadStreamInfo, IWriteStreamInfo, IDeleteStreamInfo
    {


        public Uri Uri { get; }

        public Uri ParentUri { get; }


        public string Path { get; }

        public string Name { get; }

        public bool Exists { get; }

        public DateTime CreationTime { get; }

        public DateTime LastWriteTime { get; }


        public FileStreamInfo(Uri uri, FileInfo fileInfo)
        {
            if (fileInfo is null)
                throw new ArgumentNullException(nameof(fileInfo));
            Console.WriteLine($"-{fileInfo.FullName}-");
            if (Directory.Exists(fileInfo.FullName))
                throw new ArgumentException($"{fileInfo.FullName} isn't a file.", nameof(fileInfo));

            Uri = uri ?? throw new ArgumentNullException(nameof(uri));
            if (uri.Scheme != "file")
                throw ProvideIOException.GetInvalidSchemeException(uri);

            ParentUri = fileInfo.Directory!.GetUri();

            Path = fileInfo.FullName;
            Name = fileInfo.Name;
            Exists = fileInfo.Exists;
            CreationTime = fileInfo.CreationTimeUtc;
            LastWriteTime = fileInfo.LastWriteTimeUtc;
        }


        public Stream OpenRead()
        {
            ThrowIfNoFile();
            try
            {
                if (!System.IO.File.Exists(Path))
                    return StreamExtensions.EmptyRead;

                return System.IO.File.OpenRead(Path);
            }
            catch (Exception ex)
            {
                throw new ProvideIOException(ex.Message, ex);
            }
        }


        public Stream OpenWrite()
        {
            ThrowIfNoFile();
            try
            {
                if (!System.IO.File.Exists(Path))
                {
                    var directory = System.IO.Path.GetDirectoryName(Path)!;
                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);
                }

                return System.IO.File.OpenWrite(Path);
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
                if (!System.IO.File.Exists(Path))
                    return false;

                System.IO.File.Delete(Path);
                return true;

            }
            catch (Exception ex)
            {
                throw new ProvideIOException(ex.Message, ex);
            }
        }


        protected void ThrowIfNoFile()
        {
            if (Directory.Exists(Path))
                throw ProvideIOException.GetIsNoLongerStreamException(this);
        }


        public override string ToString()
        {
            return $"{GetType()} - {Path}";
        }


    }
}
