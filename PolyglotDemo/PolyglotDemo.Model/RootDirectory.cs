using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PolyglotDemo.Model
{
    public class RootDirectory
    {
        public string _id { get; set; }
        public string un { get; set; }
        public string pw { get; set; }
        public List<Folder> folders { get; set; }
        public List<String> files { get; set; }

        
        public Boolean AddFileToCWD(string fileName, string cwd)
        {
            //char[] delimeter = new char[] {'/'};
            //cwd = cwd + "/";
            string [] path = cwd.Split('/');
            
            if (path.Length == 2)
            {
                if (this.files.Count == 0)
                {
                    this.files = new List<string>();
                    return true;
                }

                if(this.files.Contains(fileName))
                {
                    return false;
                }
                else
                {
                    this.files.Add(fileName);
                    return true;
                }
            }
            else
            {
                //return false;
                return this.folders.Find(x => x.folderName == path[1]).AddFileToCWD(fileName, path, 2);
            }
        }

        public Boolean AddFolderToCWD(string folderName, string cwd)
        {
            //char[] delimeter = new char[] {'/'};
            //cwd = cwd + "/";
            string[] path = cwd.Split('/');

            if (path.Length == 2)
            {
                if (this.folders.Count == 0)
                {
                    this.folders = new List<Folder>();
                    return true;
                }

                if (this.folders.Exists(x => x.folderName == folderName))
                {
                    return true;
                    //return false;
                }
                else
                {
                    //return false;
                    this.folders.Add(new Folder() { folderName = folderName });
                    return true;
                }
            }
            else
            {
                //return false;
                
                return this.folders.Find(x => x.folderName == path[1]).AddFolderToCWD(folderName, path, 2);
            }
        }

        //stubbed out for now
        protected Folder GetCWD(string folderName, string CWD)
        {
            return null;
        }
    }
}
