using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSoft.Git
{
    public class Repository
    {
        public Repository(string folder)
        {
            //if (Directory.Exists(folder))
            //{
            //    var fullpath = System.IO.Path.GetFullPath(folder);
            //    var dir = System.IO.Path.GetFileName(fullpath);
            //    if (dir == ".git")
            //    {
            //        return Repository.Open(fullpath);
            //    }
            //    else
            //    {

            //        return Repository.Open($"{fullpath}\\.git");
            //    }
            //}
        }


        public IEnumerable<(DateTime time, string message)> Commits { set; get; }
    }
}
