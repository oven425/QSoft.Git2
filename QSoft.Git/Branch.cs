using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QSoft.Git
{
    public static class BranchExtension
    {
        public static IEnumerable<(string name, string gitobject)> EnumableBranch(this string dir)
        {
            var fullpath = System.IO.Path.GetFullPath(dir);
            var branchs = Directory.EnumerateFiles(fullpath)
                .Select(x => new { name = System.IO.Path.GetFileName(x), gitobj = File.ReadAllText(x) });

            return Enumerable.Range(0, 1).Select(x => (x.ToString(), x.ToString()));
        }
    }

}
