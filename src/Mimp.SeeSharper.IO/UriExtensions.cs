using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mimp.SeeSharper.IO
{
    public static class UriExtensions
    {


        public static string GetFilePath(this Uri uri)
        {
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));

            string path;
            if (uri.IsAbsoluteUri)
            {
                if (uri.Scheme != "file")
                    throw new ArgumentException($"{uri} isn't a file uri", nameof(uri));
                path = uri.LocalPath;
            } else
                path = uri.OriginalString;

            path = path.Trim();
            if (Path.DirectorySeparatorChar == '\\')
                path = path.Replace('/', Path.DirectorySeparatorChar);
            else
                path = path.Replace('\\', Path.DirectorySeparatorChar);

            return path;
        }


    }
}
