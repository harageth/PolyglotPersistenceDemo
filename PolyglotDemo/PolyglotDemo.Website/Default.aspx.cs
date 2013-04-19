using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PolyglotDemo.Data;
using PolyglotDemo.Data.Test;
using PolyglotDemo.Model;


/*
 * Still needs doing:
 * Need to refactor the ChangeCWD_Event because it is not allowing going down more than one directory
 * Need to refactor the Page_Load or just rethink how I am displaying the data to the users. Right now it is not working correctly.
 * 
 * */


namespace PolyglotDemo.Website
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
             * grab the username and grab the file structure for that username
             * Bind the filestructure to the page
             * */
            //DatabaseInitialize.DatabaseInitializeFactory("Mongo");
            //DatabaseInitialize.DatabaseInitializeFactory("Redis");
            
            if (!IsPostBack)//so pretty much everything is a postback. So I need to figure out a way to simply traverse my file tree. I need to go back to storing the CWD in the session as well as the whole document. 
            {
                MongoDataContext mongoContext = new MongoDataContext();

                RootDirectory directory = mongoContext.GetFileStructure("harageth").FirstOrDefault();

                Session["directory"] = directory;
                folders.DataSource = directory.folders;
                folders.DataBind();

                IEnumerable<string> dataBindFiles = directory.files;
                files.DataSource = dataBindFiles;
                files.DataBind();
            }
            else
            {
                Response.Write("Not a postback");
            }
        }

        protected void UploadFile_Click(object sender, EventArgs e)
        {
            if (uploadFileToDatabase.HasFile)
            {

                string contentType = uploadFileToDatabase.PostedFile.ContentType;

                string fileName = uploadFileToDatabase.PostedFile.FileName;
                
                byte[] byteArray = uploadFileToDatabase.FileBytes;

                MongoDataContext mongoContext = new MongoDataContext();
                
                RedisDataContext redisContext = new RedisDataContext();
                RootDirectory directory = (RootDirectory) Session["directory"];

                if (directory.files == null)
                {
                    directory.files = new List<string>();
                }

                string virPath = virtualPath.Text;
                if (virPath.Split('/')[1].Equals(""))
                {
                    //virPath;
                }
                else
                {
                    virPath = virPath + "/";
                }
                
                if (directory.AddFileToCWD(fileName, virPath))
                {
                    mongoContext.UpdateFileStructure(directory);


                    redisContext.InsertFile(directory.un + virPath + fileName, byteArray);
                    /*if (virtualPath.Text.Equals("/"))
                    {
                        
                    }
                    else
                    {
                        redisContext.InsertFile(directory.un + virtualPath.Text + "/" + fileName, byteArray);
                    }*/
                    //Response.Redirect("Default.aspx");   
                }
                else
                {
                    Response.Write("Upload failed due to file already existing... in path");//should probably check redis as well... 
                }
            }
        }

        protected void ChangeCWD_Click(object sender, EventArgs e)
        {
            /*
             * What do we need to do here?
             * We have our CWD and we need to change to a new CWD... Whether that is going back a directory or if we are moving forward a directory
             * 
             * It might be prudent to change the dataModel to hold a parent.. Not sure how that would look in the DB though. Let me think about it.
             * It might also be prudent to store the CWD in Session for easy access.
             * 
             * 
             * */



            LinkButton val = (LinkButton) sender;
            string newCWDName = val.Text;
            RootDirectory directory = (RootDirectory)Session["directory"];
            
            int index = directory.folders.FindIndex(x => x.folderName == newCWDName);
            Folder newCWD = directory.folders[index];

            folders.DataSource = newCWD.folders;
            folders.DataBind();

            IEnumerable<string> dataBindFiles = newCWD.files;

            files.DataSource = dataBindFiles;
            files.DataBind();

            virtualPath.Text = virtualPath.Text+newCWDName;


        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            RedisDataContext redisContext = new RedisDataContext();
            LinkButton value = (LinkButton) sender;
            string fileName = "";
            string ext = "";
            int val = value.Text.LastIndexOf(".");
            
            if (val >= 0)
            {
                fileName = value.Text.Substring(0, val);
                
                ext = value.Text.Substring(val, value.Text.Length-val);
                
            }

            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + value.Text);
            RootDirectory directory = (RootDirectory)Session["directory"];
            Byte[] buffer;
            if (virtualPath.Text.Equals("/"))
            {
                buffer = redisContext.ReadFile(directory.un + virtualPath.Text + "/" + value.Text);
            }
            else
            {
                buffer = redisContext.ReadFile(directory.un + virtualPath.Text + "/" + value.Text);
            }
            //string fileContents = System.Text.Encoding.Default.GetString(buffer);
            //Response.Write(fileContents);
            
            Response.BinaryWrite(buffer);
            //Response.Write(directory.un+virtualPath.Text+value.Text);
            Response.End();
        }

        protected void CreateFolder(object sender, EventArgs e)
        {
            RootDirectory directory = (RootDirectory)Session["directory"];
            
            //have to make sure that the list of folders was initialized. If not then it needs to be newed.
            if (directory.folders == null)
            {
                directory.folders = new List<Folder>();
            }

            string virPath = virtualPath.Text;
            if (virPath.Split('/')[1].Equals(""))
            {
                //virPath;
            }
            else
            {
                virPath = virPath + "/";
            }
            Boolean val = directory.AddFolderToCWD(newFolder.Text, virPath);
            if (val)
            {
                //directory.folders.Add(new Folder() { folderName = newFolder.Text });

                MongoDataContext mongoContext = new MongoDataContext();

                mongoContext.UpdateFileStructure(directory);
            }
            else
            {
                Response.Write("Didn't add the new folder");
                //was unable to add the folder to the CWD
            }
//            Response.Redirect("Default.aspx");
        }
    }
}