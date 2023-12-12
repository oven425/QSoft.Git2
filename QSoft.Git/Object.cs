using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QSoft.Git.Object
{
    public class Blob
    {
        public string Type { set; get; }
        public Range Length { set; get; } = new Range(10, 10);
        public string FullName { private set; get; }
    }

    public static class GitObjectExtension
    {
        public static IEnumerable<(string type, long offset, int size, string filename)> EnumbleObject(this string objectfolder)
        {
            DirectoryInfo d11d = new DirectoryInfo(objectfolder);
            var aaa = d11d.EnumerateDirectories();
            var dirs = Directory.EnumerateDirectories(objectfolder)
                .Where(x => x.LastIndexOf("pack") == -1)
                .Where(x => x.LastIndexOf("info") == -1);
            var aoi = dirs.ElementAt(0).LastIndexOf("pack");
            var files = Directory.EnumerateDirectories(objectfolder)
                .Where(x => x.LastIndexOf("pack") == -1)
                .Where(x => x.LastIndexOf("info") == -1)
                .SelectMany(x => Directory.EnumerateFiles(x));
            foreach(var oo in files)
            {
                System.Diagnostics.Trace.WriteLine(oo);
                var dd = oo.ParseObject();
                yield return (dd.type, dd.offset, dd.size, oo);
            }
        }

        public static string ReadBlob(this (string type, long offset, int size, string filename) src)
        {
            using( var stream = File.OpenRead(src.filename))
            using(var zlib = new ZLibStream(stream, CompressionMode.Decompress))
            {
                var buf = new byte[4096];
                zlib.Read(buf, 0, (int)src.offset);
                buf = new byte[src.size];
                zlib.Read(buf, 0, src.size);
                var str = Encoding.UTF8.GetString(buf);
                return str;
            }
        }

        public static List<(string tt, string id)> ReadTree(this (string type, long offset, int size, string filename) src)
        {
            List<(string tt, string id)> tree = new List<(string tt, string id)>();
            using (var stream = File.OpenRead(src.filename))
            using (var zlib = new ZLibStream(stream, CompressionMode.Decompress))
            using(var br = new BinaryReader(zlib))
            {
                var buf = new byte[4096];
                zlib.Read(buf, 0, (int)src.offset);
                var readlen = zlib.Read(buf, 0, buf.Length);
                
                bool pare1 = true;
                List<byte> temp = new List<byte>();
                for(int i=0; i< readlen; i++)
                {
                    if(pare1 == true)
                    {
                        if (buf[i] == 0)
                        {
                            var dd = Encoding.UTF8.GetString(temp.ToArray());
                            var id = BitConverter.ToString(buf, i, 20).Replace("-", "");
                            tree.Add((dd, id));
                            i = i + 20;
                            
                            temp = new List<byte>();
                        }
                        else
                        {
                            temp.Add(buf[i]);
                        }
                    }
                }

                return tree;
            }
        }

        static List<List<byte>> Split(this Span<byte> src, byte delet)
        {
            List<List<byte>> dst = new List<List<byte>>();
            List<byte> temp = new List<byte>();
            
            for (int i = 0;i<src.Length ; i++)
            {
                if (src[i] == delet)
                {
                    dst.Add(temp);
                    temp = new List<byte>();
                }
                else
                {
                    temp.Add(src[i]);
                }
            }
            if(temp.Count > 0)
            {
                dst.Add(temp);
            }
            
            return dst;
        }

        public static string ReadCommit(this (string type, long offset, int size, string filename) src)
        {
            using (var stream = File.OpenRead(src.filename))
            using (var zlib = new ZLibStream(stream, CompressionMode.Decompress))
            {
                var buf = new byte[4096];
                zlib.Read(buf, 0, (int)src.offset);
                zlib.Read(buf, 0, src.size);
                var str = Encoding.UTF8.GetString(buf);
//tree db2c57d040432f1cba0a44e9322e37a0262dfe79
//parent c35f5da934412efad46bfdd612f2891c1d06b9f3
//author oven425<oven425@yahoo.com.tw> 1701874466 + 0800
//committer oven425<oven425@yahoo.com.tw> 1701874466 + 0800

//update
//1.parse object
                var regex = new Regex(@"tree (?<tree>\w+)\nparent (?<parent>\w+)\nauthor (?<author>\w.+)\ncommitter (?<committer>\w.+)\n\n(?<cc>)");
                var hr = regex.Match(str);
                if (hr.Success)
                {
                    var tree = hr.Groups["tree"];
                    var parent = hr.Groups["parent"];
                    var author = hr.Groups["author"];
                    var committer = hr.Groups["committer"];
                    int sszie = str.Length - hr.Groups["cc"].Index;
                    var aa = new string(str.ToArray(), hr.Groups["cc"].Index, sszie);
                    var dd = str.Skip(hr.Groups["cc"].Index).Take(sszie);
                }
                return str;
            }
        }

        public static (string type, long offset, int size) ParseObject(this string fullname)
        {
            long zeroindex = 0;
            using (var stream = File.OpenRead(fullname))
            using (var zlib = new ZLibStream(stream, CompressionMode.Decompress))
            {
                var headers = new List<byte>();
                while (true)
                {
                    var bb = zlib.ReadByte();
                    if (bb == 0)
                    {
                        zeroindex = zlib.BaseStream.Position;
                        break;
                    }
                    else if (bb == -1)
                    {
                        throw new Exception("No find header");
                    }
                    headers.Add((byte)bb);
                }
                var headerstr = Encoding.ASCII.GetString(headers.ToArray());
                var regex = new Regex(@"(?<type>\w+) (?<length>\d+)");
                var match = regex.Match(headerstr);
                if (match.Success)
                {
                    //obj.Type = match.Groups["type"].Value;
                    //obj.Length = int.Parse(match.Groups["length"].Value);
                   var len= int.Parse(match.Groups["length"].Value);
                    //var range = new Range(new Index((int)zeroindex), new Index(len));
                    return (match.Groups["type"].Value, headers.Count+1, len);
                }
                else
                {
                    throw new Exception("parse Blob header fail");
                }
            }
        }
    }
    public class GitObject
    {
        public GitObject()
        {

        }

        public string Type { set; get; }
        public int Length { set; get; }
        public string FullName { private set; get; }
        public int Offset { set; get; }
        

        //public static GitObject Open(string fullname)
        //{
        //    if(File.Exists(fullname)==false)
        //    {
        //        throw new FileNotFoundException();
        //    }
        //    var obj = new GitObject();
        //    obj.FullName = fullname;
        //    var readbuf = new byte[4];
        //    using( var stream = File.OpenRead(fullname))
        //    using(var zlib = new ZLibStream(stream, CompressionMode.Decompress))
        //    {
        //        var headers = new List<byte>();
        //        while(true)
        //        {
        //            var bb = zlib.ReadByte();
        //            if(bb ==0)
        //            {
        //                break;
        //            }
        //            else if(bb == -1)
        //            {
        //                throw new Exception("No find header");
        //            }
        //            headers.Add((byte)bb);
        //        }
        //        var headerstr = Encoding.ASCII.GetString(headers.ToArray());
        //        var regex = new Regex(@"(?<type>\w+) (?<length>\d+)");
        //        var match = regex.Match(headerstr);
        //        if(match.Success)
        //        {
        //            obj.Type = match.Groups["type"].Value;
        //            obj.Length = int.Parse(match.Groups["length"].Value);
        //        }
        //        else
        //        {
        //            throw new Exception("parse Blob header fail");
        //        }

        //        var kk = new byte[4096];
        //        var len = zlib.Read(kk);
        //        List<byte[]> lls = new List<byte[]>();
        //        var stratindex = 0;
                
        //        while(true)
        //        {
        //            var endindex = Array.FindIndex(kk, stratindex, x => x==0);
        //            lls.Add(kk.Skip(stratindex).Take(endindex-stratindex).ToArray());
        //            if(endindex == len)
        //            {
        //                break;
        //            }
        //            stratindex = endindex+1;
        //        }
        //        //5ee6f40cf477ee5bc16227d4dd71b529cc8d765d3130303634342051536f66742e4769742e637370726f6a
        //        //  e6f40cf477ee5bc16227d4dd71b529cc8d765d
        //        var tt = Encoding.ASCII.GetString(lls[0]);
        //        var hhs = BitConverter.ToString(lls[1]).Replace("-", "").ToLowerInvariant();
        //        var hhs1 = BitConverter.ToString(lls[2]).Replace("-", "").ToLowerInvariant();
        //    }
            
        //    return obj;
        //}

        
    }
}
