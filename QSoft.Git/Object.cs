using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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
                int realen = zlib.Read(readbuf);
                obj.Type = Encoding.ASCII.GetString(readbuf);
                realen = zlib.Read(readbuf);
                obj.Length = BitConverter.ToInt32(readbuf, 0);
            }
            
            return obj;
        }

        
    }
}
