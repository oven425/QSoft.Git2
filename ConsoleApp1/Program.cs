// See https://aka.ms/new-console-template for more information
using System.IO.Compression;
using System.Text;



Console.WriteLine("Hello, World!");
var objectzip = "C:\\Users\\oven4\\source\\repos\\QSoft.Git2\\.git\\objects\\4a\\03b592d35655a7f629dbf3671235497a7b0d13";
//var oo = QSoft.Git.Object.Open(objectzip);
using(var f=File.OpenRead(objectzip))
{
    var zz = new ZLibStream(f, CompressionMode.Decompress);
    

    var readbuf = new byte[4096];
    var read_len = zz.Read(readbuf, 0, readbuf.Length);
    var type = Encoding.ASCII.GetString(readbuf);
    readbuf = new byte[4];
    read_len = zz.Read(readbuf, 0, readbuf.Length);
    var size = BitConverter.ToInt32(readbuf);
    readbuf = new byte[size];
    read_len = zz.Read(readbuf, 0, readbuf.Length);
    File.WriteAllBytes("123.txt", readbuf.Take(read_len).ToArray());
    var type1 = Encoding.ASCII.GetString(readbuf);

}




var bbs = File.ReadAllBytes(@"C:\Users\oven4\source\repos\QSoft.Git2\.git\objects\4a\c67b24bfd89af7dbcfb89ea2ab3195ba59df64");
var str = Encoding.UTF8.GetString(bbs, 4, 4);
Console.WriteLine(str);