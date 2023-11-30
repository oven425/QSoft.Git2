using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QSoft.Git
{
    public class GitObjectBlob : GitObject
    {
        public GitObjectBlob(GitObject gitobject)
            :base(gitobject)
        {

        }
    }

    public class GitObjectTree: GitObject
    {

    }
    public class GitObject
    {
        public GitObject()
        {

        }

        public GitObject(GitObject gitobject)
        {
            this.FullName = gitobject.FullName;
            this.Type = gitobject.Type;
            this.Length = gitobject.Length;
        }
        public string Type { set; get; }
        public int Length { set; get; }
        public string FullName { private set; get; }
        public static Object Open(string fullname)
        {
            if(File.Exists(fullname)==false)
            {
                throw new FileNotFoundException();
            }
            var obj = new GitObject();
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

                var kk = new byte[4096];
                var len = zlib.Read(kk);
                List<byte[]> lls = new List<byte[]>();
                var stratindex = 0;
                
                while(true)
                {
                    var endindex = Array.FindIndex(kk, stratindex, x => x==0);
                    lls.Add(kk.Skip(stratindex).Take(endindex-stratindex).ToArray());
                    if(endindex == len)
                    {
                        break;
                    }
                    stratindex = endindex+1;
                }
                //5ee6f40cf477ee5bc16227d4dd71b529cc8d765d3130303634342051536f66742e4769742e637370726f6a
                //  e6f40cf477ee5bc16227d4dd71b529cc8d765d
                var tt = Encoding.ASCII.GetString(lls[0]);
                var hhs = BitConverter.ToString(lls[1]).Replace("-", "").ToLowerInvariant();
                var hhs1 = BitConverter.ToString(lls[2]).Replace("-", "").ToLowerInvariant();
            }
            
            return obj;
        }

        
    }
}
