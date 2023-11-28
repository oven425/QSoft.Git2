// See https://aka.ms/new-console-template for more information
using System.Buffers.Binary;
using System.IO.Compression;
using System.Text;



Console.WriteLine("Hello, World!");
var objectzip = @"C:\\Users\\oven4\\source\\repos\\QSoft.Git2\\.git\\index";
using(var file = File.OpenRead(objectzip))
{
    BinaryReader br = new BinaryReader(file);
    var dirc = br.ReadBytes(4);
    var version = BitConverter.ToInt32(br.ReadBytes(4).Reverse().ToArray());
    var entry = BitConverter.ToInt32(br.ReadBytes(4).Reverse().ToArray());

    //BinaryPrimitives
}
