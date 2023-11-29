// See https://aka.ms/new-console-template for more information
using System.Buffers.Binary;
using System.IO.Compression;
using System.Text;

var objfile = @"C:\Users\oven4\source\repos\QSoft.Git2\.git\objects\4e\f4278d2c878ce29a361cabc8f1fea04efdf6f5";

using(var file = File.OpenRead(objfile))
using(var zlib = new ZLibStream(file, CompressionMode.Decompress))
{
    
}
QSoft.Git.Object.Open(@"C:\Users\oven4\source\repos\QSoft.Git2\.git\objects\4e\f4278d2c878ce29a361cabc8f1fea04efdf6f5");


Console.WriteLine("Hello, World!");
var objectzip = @"C:\\Users\\oven4\\source\\repos\\QSoft.Git2\\.git\\index";
using(var file = File.OpenRead(objectzip))
{
    BinaryReader br = new BinaryReader(file);
    var dirc = br.ReadBytes(4);
    var aa = BinaryPrimitives.ReadInt32BigEndian(br.ReadBytes(4));
    var entry = BinaryPrimitives.ReadInt32BigEndian(br.ReadBytes(4));

    var second = BinaryPrimitives.ReadInt32BigEndian(br.ReadBytes(4));
    var nanosecond_fractions = BinaryPrimitives.ReadInt32BigEndian(br.ReadBytes(4));
    var change_time = new DateTime(1970,1,1).AddSeconds(second);

    second = BinaryPrimitives.ReadInt32BigEndian(br.ReadBytes(4));
    nanosecond_fractions = BinaryPrimitives.ReadInt32BigEndian(br.ReadBytes(4));
    var modify_time = new DateTime(1970, 1, 1).AddSeconds(second);
}
