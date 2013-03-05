using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyglotDemo.Model
{
    public class RootDirectory
    {
        public string _id { get; set; }
        public string un { get; set; }
        public List<Folder> folders { get; set; }
        public List<String> files { get; set; }

        public string GetPath(string fileName) //something like this anyway
        {
            if (files.Contains(fileName))
            {
                return "./"+fileName;
            }
            else
            {
                return null;
            }
        }
    }
}
