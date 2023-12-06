// See https://aka.ms/new-console-template for more information
using QSoft.Git.Object;
using System.Buffers.Binary;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

//var objfile = @"C:\Users\oven4\source\repos\QSoft.Git2\.git\objects\4e\f4278d2c878ce29a361cabc8f1fea04efdf6f5";

//using(var file = File.OpenRead(objfile))
//using(var zlib = new ZLibStream(file, CompressionMode.Decompress))
//{

//}
//要計算的內容
var content = "Hello, 5xRuby";

//計算公式
var input = $"blob {content.Length}\0{content}";
string source = "Hello World!";
using (SHA256 sha256Hash = SHA256.Create())
{
    string hash = GetHash(sha256Hash, input);
    //0f69639c88aff6d81e8ffb172172f55b720ae91f5cdd0bcabaa949b7cc245614
    //4135fc4add3332e25ab3cd5acabe1bd9ea0450fb
    Console.WriteLine($"The SHA256 hash of {input} is: {hash}.");

    Console.WriteLine("Verifying the hash...");

    //if (VerifyHash(sha256Hash, source, hash))
    //{
    //    Console.WriteLine("The hashes are the same.");
    //}
    //else
    //{
    //    Console.WriteLine("The hashes are not same.");
    //}
}

string GetHash(HashAlgorithm hashAlgorithm, string input)
{

    // Convert the input string to a byte array and compute the hash.
    byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

    // Create a new Stringbuilder to collect the bytes
    // and create a string.
    var sBuilder = new StringBuilder();

    // Loop through each byte of the hashed data
    // and format each one as a hexadecimal string.
    for (int i = 0; i < data.Length; i++)
    {
        sBuilder.Append(data[i].ToString("x2"));
    }

    // Return the hexadecimal string.
    return sBuilder.ToString();
}
var range=new Range(new Index(10), new Index(20));

var objectfolder = @"C:\Users\oven4\source\repos\QSoft.Git2\.git\objects";
var objs= objectfolder.EnumbleObject().ToList();
var dirs = Directory.EnumerateDirectories(objectfolder)
    .SelectMany(x => Directory.EnumerateFiles(x));
//foreach(var file in dirs)
//{
//    GitObject.Open(file);
//}
//QSoft.Git.Object.GitObject.Open(@"C:\Users\oven4\source\repos\QSoft.Git2\.git\objects\ee\df73ee7bb34736f36685803de2fdb8eb1124a9");


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
