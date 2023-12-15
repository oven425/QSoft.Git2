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

                while (true)
                {
                    var begin_pos = file.Position;
                    readbuf = new byte[4];
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
                    readbuf = new byte[2];
                    readlen = file.Read(readbuf);
                    var size1 = BitConverter.ToInt16(readbuf.Reverse().ToArray(), 0) & 0xfff;
                    //if (size < 0xfff)
                    //{

                    //}
                    //else
                    {
                        var lls = new List<byte>();
                        while (true)
                        {
                            var b1 = file.ReadByte();
                            if (b1 == 0 | b1 == -1)
                            {
                                break;
                            }
                            lls.Add((byte)b1);
                        }
                        var h = Encoding.UTF8.GetString(lls.ToArray());
                        System.Diagnostics.Trace.WriteLine(h);
                    }
                    readlen = file.Read(readbuf);
                    var field = BitConverter.ToInt16(readbuf.Reverse().ToArray(), 0);
                    var len1 = file.Position - begin_pos;
                    int nullsize = 8;
                    var aa = len1 % nullsize;
                    if(aa != 0)
                    {
                        var bb = len1 / nullsize;
                        bb++;
                        var cc = bb * nullsize - len1;
                        readbuf = new byte[cc];
                        readlen = file.Read(readbuf);
                    }
                }
                
            }
        }
    }
}
