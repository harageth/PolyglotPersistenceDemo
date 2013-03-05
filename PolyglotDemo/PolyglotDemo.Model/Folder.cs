using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyglotDemo.Model
{
    public class Folder
    {
        public string folderName { get; set; }
        public List<Folder> folders { get; set; }
        public List<String> files { get; set; }
    }
}
