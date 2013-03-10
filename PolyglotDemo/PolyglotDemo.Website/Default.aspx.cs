using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PolyglotDemo.Data;
using PolyglotDemo.Data.Test;
using PolyglotDemo.Model;

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

            MongoDataContext mongoContext = new MongoDataContext();
            
            RootDirectory directory = mongoContext.GetFileStructure("harageth").FirstOrDefault();
            
            Session["directory"] = directory;
            folders.DataSource = directory.folders;
            folders.DataBind();

            IEnumerable<string> dataBindFiles = directory.files;
            files.DataSource = dataBindFiles;
            files.DataBind();
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
                if (directory.AddFileToCWD(fileName, virtualPath.Text))
                {
                    mongoContext.UpdateFileStructure(directory);

                    if (virtualPath.Text.Equals("/"))
                    {
                        redisContext.InsertFile(directory.un + virtualPath.Text + fileName, byteArray);
                    }
                    else
                    {
                        redisContext.InsertFile(directory.un + virtualPath.Text + "/" + fileName, byteArray);
                    }
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
                buffer = redisContext.ReadFile(directory.un + virtualPath.Text + value.Text);
            }
            else
            {
                buffer = redisContext.ReadFile(directory.un + virtualPath.Text + "/" + value.Text);
            }
            //string fileContents = System.Text.Encoding.Default.GetString(buffer);
            //Response.Write(fileContents);
            
            Response.BinaryWrite(buffer);
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

            Boolean val = directory.AddFolderToCWD("testFolder", virtualPath.Text);
            directory.folders.Add(new Folder() { folderName="testFolder" });

            MongoDataContext mongoContext = new MongoDataContext();

            mongoContext.UpdateFileStructure(directory);
            Response.Redirect("Default.aspx");
        }
    }
}