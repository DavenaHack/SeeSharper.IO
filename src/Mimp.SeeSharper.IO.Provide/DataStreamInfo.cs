using Mimp.SeeSharper.IO.Provide.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mimp.SeeSharper.IO.Provide
{
    public class DataStreamInfo : IReadStreamInfo
    {


        public Uri Uri { get; }


        public string MediaType { get; }

        public IEnumerable<string> Parameters { get; }

        public Encoding? Charset { get; }

        public bool IsBase64 { get; }


        public string Data { get; }


        public DataStreamInfo(Uri uri)
        {
            Uri = uri ?? throw new ArgumentNullException(nameof(uri));
            if (uri.Scheme != "data")
                throw ProvideIOException.GetInvalidSchemeException(uri);

            MediaType = "text/plain";
            Charset = null;
            IsBase64 = false;
            Parameters = Array.Empty<string>();

            var parts = uri.OriginalString.Split(new[] { ':' }, 2)[1].Split(new[] { ',' }, 2);
            if (parts.Length == 1)
                Data = parts[0];
            else
            {
                if (!string.IsNullOrWhiteSpace(parts[0]))
                {
                    var ps = parts[0].Split(';');
                    MediaType = ps[0];
                    Parameters = ps.Skip(1).ToArray();
                    foreach (var p in Parameters)
                    {
                        var v = p.Trim();
                        if (string.Equals(v, "base64", StringComparison.InvariantCultureIgnoreCase))
                            IsBase64 = true;
                        else if (v.StartsWith("charset"))
                        {
                            var cs = v.Split(new[] { '=' }, 2);
                            if (cs.Length == 2)
                                Charset = Encoding.GetEncoding(cs[1].Trim());
                        }
                    }
                }
                Data = parts[1];
            }

            if (!IsBase64)
                Data = Uri.UnescapeDataString(Data);
        }


        public Stream OpenRead()
        {
            byte[] buffer;
            if (IsBase64)
                buffer = Convert.FromBase64String(Data);
            else
            {
                var encoding = Charset ?? Encoding.Default;
                buffer = encoding.GetBytes(Data);
            }
            return new MemoryStream(buffer);
        }


        public override string ToString()
        {
            return $"{GetType()} - {Uri}";
        }


    }
}