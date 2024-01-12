using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QSoft.Git.Object;

namespace QSoft.Git
{
    public class Repository
    {
        string m_GitFolder;
        public Repository(string folder)
        {
            if (Directory.Exists(folder))
            {
                var fullpath = System.IO.Path.GetFullPath(folder);
                var dir = System.IO.Path.GetFileName(fullpath);
                if (dir == ".git")
                {
                    m_GitFolder = fullpath;
                }
                else
                {
                    m_GitFolder = $"{fullpath}\\.git";
                }
                var gitfolderobject = System.IO.Path.Join(m_GitFolder, "objects");
                var gitobjs = gitfolderobject.EnumbleObject().GroupBy(x=>x.type);
                foreach (var item in gitobjs)
                {
                    switch(item.Key)
                    {
                        case "commit":
                            {
                                var commits = item.Select(x => x.ReadCommit()).ToList();
                                this.Commits = item.Select(x => x.ReadCommit())
                                    .Select(x=>(x.author.utc, x.tree));
                            }
                            break;
                    }
                }


            }
        }


        public IEnumerable<(DateTime time, string message)> Commits { set; get; }
    }
}
