using System;
using System.IO;

namespace Mimp.SeeSharper.IO.Provide.File
{
    public static class StringExtensions
    {


        public static Uri FilePath2AbsoluteUri(string path, string? relativeTo)
        {
            if (path is null)
                throw new ArgumentNullException(nameof(path));

            path = path.Trim();
            path = Path.IsPathRooted(path) ? Path.GetFullPath(path)
                : Path.GetFullPath(Path.Combine(relativeTo ?? string.Empty, path));

            if (path.StartsWith("/"))
                path = path.Substring(1);

            return new Uri($"file:///{path}");
        }

        public static Uri FilePath2AbsoluteUri(string path) =>
            FilePath2AbsoluteUri(path, null);


    }
}
