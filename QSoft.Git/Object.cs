using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QSoft.Git
{
    public class Object
    {
        public string Type { set; get; }
        public int Length { set; get; }
        public string FullName { private set; get; }
        public static Object Open(string fullname)
        {
            if(File.Exists(fullname)==false)
            {
                throw new FileNotFoundException();
            }
            Object obj = new Object();
            obj.FullName = fullname;
            var readbuf = new byte[4];
            using( var stream = File.OpenRead(fullname))
            using(var zlib = new ZLibStream(stream, CompressionMode.Decompress))
            {
                var headers = new List<byte>();
                while(true)
                {
                    var bb = zlib.ReadByte();
                    if(bb ==0)
                    {
                        break;
                    }
                    else if(bb == -1)
                    {
                        throw new Exception("No find header");
                    }
                    headers.Add((byte)bb);
                }
                var headerstr = Encoding.ASCII.GetString(headers.ToArray());
                var regex = new Regex(@"(?<type>\w+) (?<length>\d+)");
                var match = regex.Match(headerstr);
                if(match.Success)
                {
                    obj.Type = match.Groups["type"].Value;
                    obj.Length = int.Parse(match.Groups["length"].Value);
                }
                else
                {
                    throw new Exception("parse Blob header fail");
                }
            }
            
            return obj;
        }

        
    }
}
