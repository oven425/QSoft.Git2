// See https://aka.ms/new-console-template for more information
using QSoft.Git;
using QSoft.Git.Object;
using System.Buffers.Binary;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
//var brs = "../../../../test/.git/refs/heads".EnumableBranch();


//https://stackoverflow.com/questions/6804586/how-to-conditional-regex
//tree 65a457425a679cbe9adf0d2741785d3ceabb44a7
//author oven425<oven425@yahoo.com.tw> 1704203885 +0800
//committer oven425 <oven425 @yahoo.com.tw> 1704203885 +0800
//
//add a.txt

var parsecommit = (string src) =>
{
    var regex1 = new Regex(@"(?<edit>\w+) [<](?<mail>\w.+)[>] (?<timestamp>\d+) (?<offset1>\+|-)(?<offset2>\w+)");
    var mm = regex1.Match(src);
    if (mm.Success)
    {
        var editor = mm.Groups["edit"].Value;
        var mail = mm.Groups["mail"].Value;
        var timestamp = mm.Groups["timestamp"].Value;
        var offset1 = mm.Groups["offset1"].Value;
        var offset2 = mm.Groups["offset2"].Value;
        var sec = int.Parse(timestamp);
        var utc = new DateTime(1970, 1, 1).AddSeconds(sec);
        var zone = TimeSpan.Parse(offset1 switch
        {
            "+" => $"{offset2.Insert(2, ":")}",
            "-" => $"-{offset2.Insert(2, ":")}",
            _=>"00:00"
        });
        return (editor, mail, utc, zone);
    }
    return ("", "", DateTime.MinValue, TimeSpan.Zero);
};
DateTime basetime = new DateTime(1970, 1, 1);
var tt = basetime.AddSeconds(1701874466).AddHours(8);
var tsbb = TimeSpan.TryParse("+08:00", out var ts);
var regex1 = new Regex(@"(?<edit>\w+) [<](?<mail>\w.+)[>] (?<timestamp>\d+) (?<offset1>\+|-)(?<offset2>\w+)");
var str = "qoo <lkk@yahoo.com.tw> 1663316929 +0800";
var pppo = parsecommit(str);
var mm = regex1.Match(str);
if(mm.Success)
{
    var editor = mm.Groups["edit"].Value;
    var mail = mm.Groups["mail"].Value;
    var timestamp = mm.Groups["timestamp"].Value;
    var offset1 = mm.Groups["offset1"].Value;
    var offset2 = mm.Groups["offset2"].Value;
    var sec = int.Parse(timestamp);
    var utc = new DateTime(1970, 1, 1).AddSeconds(sec);
    var poo = offset2.Insert(2, ":");
    var tt1 = $"{offset1}";
    var strtt = offset1 switch
    {
        "+" => $"{offset2.Insert(2, ":")}",
        "-" => $"-{offset2.Insert(2, ":")}"
    };
    var zone = TimeSpan.Parse(strtt);
}



//var index = @"C:\Users\oven4\source\repos\QSoft.Git2\.git\index";
//index.ReadIndex();


var objectfolder = @"C:\Users\oven4\source\repos\QSoft.Registry\.git\objects";
var objs = objectfolder.EnumbleObject();
foreach (var oo in objs)
{
    System.Diagnostics.Trace.WriteLine($"{oo.type} {oo.size}");
    //if(oo.type == "blob")
    //{
    //    oo.ReadBlob();
    //}
    //else if (oo.type == "tree")
    //{
    //    oo.ReadTree();
    //}
    //else if (oo.type == "commit")
    //{
    //    oo.ReadCommit();
    //}
}
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
