using System;
using System.IO;

namespace Mimp.SeeSharper.IO
{
    public static class FileSystemInfoExtensions
    {


        public static Uri GetUri(this FileSystemInfo file)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));

            return new Uri($"file://{file.FullName}");
        }


    }
}
