using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSoft.Git
{
    public class Index
    {
        public void ReadAll()
        {

        }
    }

    static public class IndexExtension
    {
        public static void ReadIndex(this string src)
        {
            using(var file = File.OpenRead(src))
            {
                var readbuf = new byte[4];
                var readlen = file.Read(readbuf);
                var dirc_str = Encoding.ASCII.GetString(readbuf);

                readlen = file.Read(readbuf);
                var version = BitConverter.ToInt32(readbuf.Reverse().ToArray(), 0);

                readlen = file.Read(readbuf);
                var entries = BitConverter.ToInt32(readbuf.Reverse().ToArray(), 0);

                readlen = file.Read(readbuf);
                var ctime_seconds = BitConverter.ToInt32(readbuf.Reverse().ToArray(), 0);
                readlen = file.Read(readbuf);
                var ctime_nanoseconds = BitConverter.ToInt32(readbuf.Reverse().ToArray(), 0);

                readlen = file.Read(readbuf);
                var mtime_seconds = BitConverter.ToInt32(readbuf.Reverse().ToArray(), 0);
                readlen = file.Read(readbuf);
                var mtime_nanoseconds = BitConverter.ToInt32(readbuf.Reverse().ToArray(), 0);

                readlen = file.Read(readbuf);
                var dev = BitConverter.ToInt32(readbuf.Reverse().ToArray(), 0);
                readlen = file.Read(readbuf);
                var ino = BitConverter.ToInt32(readbuf.Reverse().ToArray(), 0);
                readlen = file.Read(readbuf);
                var mode = BitConverter.ToInt32(readbuf.Reverse().ToArray(), 0);

                readlen = file.Read(readbuf);
                var uid = BitConverter.ToInt32(readbuf.Reverse().ToArray(), 0);
                readlen = file.Read(readbuf);
                var gid = BitConverter.ToInt32(readbuf.Reverse().ToArray(), 0);
                readlen = file.Read(readbuf);
                var size = BitConverter.ToInt32(readbuf.Reverse().ToArray(), 0);

                readbuf = new byte[20];
                readlen = file.Read(readbuf);
                var sha1 = BitConverter.ToString(readbuf);
                readbuf = new byte[4];
                readlen = file.Read(readbuf);
                var size1 = BitConverter.ToInt32(readbuf.Reverse().ToArray(), 0)&0xfff;

                var lls = new List<byte>();
                while(true)
                {
                    var b1 = file.ReadByte();
                    if(b1 == 0|b1==-1)
                    {
                        break;
                    }
                    lls.Add((byte)b1);
                }
                var h = Encoding.UTF8.GetString(lls.ToArray());
            }
        }
    }
}
