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


        public Boolean AddFileToCWD(string fileName, string[] path, int index)
        {
            if(this.files == null)
            {
                this.files = new List<string>();
                this.files.Add(fileName);
                return true;
            }
            else if (index == path.Length - 1)
            {
                if(this.files.Contains(fileName))
                {
                    return false;
                }
                else
                {
                    //this.folders.Find(x => x.folderName.Equals(path[index])).files.Add(fileName);
                    this.files.Add(fileName);
                    return true;
                }
            }
            else
            {
                return this.folders.Find(x => x.folderName == path[index]).AddFileToCWD(fileName, path, index++);
            }
        }

        public void AddFolderToCWD()
        {

        }
    }
}
